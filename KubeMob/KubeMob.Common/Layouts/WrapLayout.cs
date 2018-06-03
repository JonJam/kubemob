using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Layouts
{
    /// <summary>
    /// This class provides a wrapping layout container for Xamarin.Forms
    ///
    /// Based off <see cref="https://github.com/xamarinhq/xamu-infrastructure/blob/master/src/XamU.Infrastructure/Layout/WrapLayout.cs"/>
    /// </summary>
    [Preserve(AllMembers = true)]
    public class WrapLayout : Layout<View>
    {
        /// <summary>
        /// Bindable property definition for the ColumnSpacing property
        /// </summary>
        public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(
            nameof(ColumnSpacing),
            typeof(double),
            typeof(WrapLayout),
            5.0,
            propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).InvalidateLayout());

        /// <summary>
        /// Bindable property definition for the ColumnSpacing property
        /// </summary>
        public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(
            nameof(RowSpacing),
            typeof(double),
            typeof(WrapLayout),
            5.0,
            propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).InvalidateLayout());

        private readonly Dictionary<Size, LayoutData> layoutDataCache = new Dictionary<Size, LayoutData>();

        public double ColumnSpacing
        {
            get => (double)this.GetValue(ColumnSpacingProperty);
            set => this.SetValue(ColumnSpacingProperty, value);
        }

        public double RowSpacing
        {
            get => (double)this.GetValue(RowSpacingProperty);
            set => this.SetValue(RowSpacingProperty, value);
        }

        /// <summary>
        /// Initial size calculation performed to determine how much space is required to display the child elements
        /// </summary>
        /// <param name="widthConstraint">The width constraint to request.</param>
        /// <param name="heightConstraint">The height constraint to request.</param>
        /// <summary>Method that is called when a layout measurement happens.</summary>
        /// <returns>To be added.</returns>
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            LayoutData layoutData = this.GetLayoutData(widthConstraint, heightConstraint);

            if (layoutData.VisibleChildCount == 0)
            {
                return default(SizeRequest);
            }

            Size totalSize = new Size(
                (layoutData.CellSize.Width * layoutData.Columns) + (this.ColumnSpacing * (layoutData.Columns - 1)),
                (layoutData.CellSize.Height * layoutData.Rows) + (this.RowSpacing * (layoutData.Rows - 1)));

            return new SizeRequest(totalSize);
        }

        /// <summary>
        /// Second pass which performs the actual layout of children based on the available
        /// size given to the layout panel.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width available</param>
        /// <param name="height">Height available</param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            LayoutData layoutData = this.GetLayoutData(width, height);

            if (layoutData.VisibleChildCount == 0)
            {
                return;
            }

            double xChild = x;
            double yChild = y;
            int column = 0;

            foreach (View child in this.Children)
            {
                if (!child.IsVisible)
                {
                    continue;
                }

                LayoutChildIntoBoundingRegion(child, new Rectangle(new Point(xChild, yChild), layoutData.CellSize));

                if (++column == layoutData.Columns)
                {
                    column = 0;
                    xChild = x;
                    yChild += this.RowSpacing + layoutData.CellSize.Height;
                }
                else
                {
                    xChild += this.ColumnSpacing + layoutData.CellSize.Width;
                }
            }
        }

        /// <summary>
        /// Invalidates the current layout.
        /// </summary>
        /// <remarks>
        /// Calling this method will invalidate the measure and triggers a new layout cycle.
        /// </remarks>
        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();

            // Discard all layout information for children added or removed.
            this.layoutDataCache.Clear();
        }

        /// <summary>
        /// Invoked whenever a child of the layout has emitted <see cref="E:Xamarin.Forms.VisualElement.MeaureInvalidated" />.
        /// Implement this method to add class handling for this event.
        /// </summary>
        protected override void OnChildMeasureInvalidated()
        {
            base.OnChildMeasureInvalidated();

            // Discard all layout information for child size changed.
            this.layoutDataCache.Clear();
        }

        /// <summary>
        /// Calculate the rows/columns to use for the given width/height and cache it off.
        /// Ideally, we will only calculate this twice for most cases (orientations), however
        /// desktop apps can resize at will.
        /// </summary>
        /// <param name="width">Available width</param>
        /// <param name="height">Available height</param>
        /// <returns>The rows/columns to use for the given width/height.</returns>
        private LayoutData GetLayoutData(double width, double height)
        {
            Size size = new Size(width, height);

            // Check if cached information is available.
            if (this.layoutDataCache.ContainsKey(size))
            {
                return this.layoutDataCache[size];
            }

            int visibleChildCount = 0;
            Size maxChildSize = default(Size);
            LayoutData layoutData = default(LayoutData);

            // Enumerate through all the children.
            foreach (View child in this.Children)
            {
                // Skip invisible children.
                if (!child.IsVisible)
                {
                    continue;
                }

                // Count the visible children.
                visibleChildCount++;

                // Get the child's requested size.
                SizeRequest childSizeRequest = child.Measure(
                    double.PositiveInfinity,
                    double.PositiveInfinity);

                // Accumulate the maximum child size.
                maxChildSize.Width = Math.Max(maxChildSize.Width, childSizeRequest.Request.Width);
                maxChildSize.Height = Math.Max(maxChildSize.Height, childSizeRequest.Request.Height);
            }

            if (visibleChildCount != 0)
            {
                // Calculate the number of rows and columns.
                int columns = 0;
                int rows = 0;

                if (double.IsPositiveInfinity(width))
                {
                    columns = visibleChildCount;
                    rows = 1;
                }
                else
                {
                    columns = (int)((width + this.ColumnSpacing) / (maxChildSize.Width + this.ColumnSpacing));
                    columns = Math.Max(1, columns);
                    rows = (visibleChildCount + columns - 1) / columns;
                }

                // Now maximize the cell size based on the layout size.
                Size cellSize = default(Size);

                if (double.IsPositiveInfinity(width))
                {
                    cellSize.Width = maxChildSize.Width;
                }
                else
                {
                    cellSize.Width = (width - (this.ColumnSpacing * (columns - 1))) / columns;
                }

                if (double.IsPositiveInfinity(height))
                {
                    cellSize.Height = maxChildSize.Height;
                }
                else
                {
                    cellSize.Height = (height - (this.RowSpacing * (rows - 1))) / rows;
                }

                layoutData = new LayoutData(visibleChildCount, cellSize, rows, columns);
            }

            // We don't save off the layout data for desktop because it can have too
            // many variations.
            if (Device.Idiom != TargetIdiom.Desktop)
            {
                this.layoutDataCache.Add(size, layoutData);
            }

            return layoutData;
        }

        /// <summary>
        /// Internal structure to hold data about the structure of the panel.
        /// </summary>
        private struct LayoutData
        {
            public readonly int VisibleChildCount;
            public readonly int Rows;
            public readonly int Columns;

            public Size CellSize;

            public LayoutData(
                int visibleChildCount,
                Size cellSize,
                int rows,
                int columns)
            {
                this.VisibleChildCount = visibleChildCount;
                this.CellSize = cellSize;
                this.Rows = rows;
                this.Columns = columns;
            }
        }
    }
}