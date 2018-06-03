using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Views
{
    /// <summary>
    /// Simple ItemsControl to render a list of things in a stacked view using
    /// either text labels, or an inflated data template. It also includes the ability
    /// to display a text placeholder if no items are present in the data bound collection.
    ///
    /// Based off <see cref="https://github.com/xamarinhq/xamu-infrastructure/blob/master/src/XamU.Infrastructure/Controls/ItemsControl.cs"/>
    /// https://docs.microsoft.com/en-us/xamarin/cross-platform/desktop/controls/wpf#itemscontrol
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ItemsControl : ContentView
    {
        /// <summary>
        /// Bindable property for the placeholder text.
        /// </summary>
        public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(
            nameof(PlaceholderText),
            typeof(string),
            typeof(ItemsControl));

        /// <summary>
        /// Bindable property for the orientation of the default layout panel
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            nameof(Orientation),
            typeof(StackOrientation),
            typeof(ItemsControl),
            StackOrientation.Vertical,
            propertyChanged: (bindable, oldValue, newValue) => ((ItemsControl)bindable).OnOrientationPropertyChanged((StackOrientation)oldValue, (StackOrientation)newValue));

        /// <summary>
        /// Bindable property for the Spacing of the default layout panel
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(ItemsControl),
            10.0,
            propertyChanged: (bindable, oldValue, newValue) => ((ItemsControl)bindable).OnSpacingPropertyChanged((double)oldValue, (double)newValue));

        /// <summary>
        /// Bindable property for the Label style used for each item when there
        /// is no data template assigned.
        /// </summary>
        public static readonly BindableProperty ItemStyleProperty = BindableProperty.Create(
            nameof(ItemStyle),
            typeof(Style),
            typeof(ItemsControl),
            propertyChanged: (bindable, oldValue, newValue) => ((ItemsControl)bindable).OnItemStylePropertyChanged(newValue as Style));

        /// <summary>
        /// Bindable property for the panel type
        /// </summary>
        public static readonly BindableProperty ItemsPanelProperty = BindableProperty.Create(
            nameof(ItemsPanel),
            typeof(Layout<View>),
            typeof(ItemsControl),
            propertyChanged: (bindable, oldValue, newValue) => ((ItemsControl)bindable).OnItemsPanelPropertyChanged((Xamarin.Forms.Layout<View>)oldValue, (Xamarin.Forms.Layout<View>)newValue));

        /// <summary>
        /// Bindable property for the data source
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(ItemsControl),
            propertyChanging: (bindable, oldValue, newValue) => ((ItemsControl)bindable).OnItemsSourceChanged((IList)oldValue, (IList)newValue));

        /// <summary>
        /// Bindable property for the data template to visually represent each item.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ItemsControl),
            propertyChanging: (bindable, oldValue, newValue) => ((ItemsControl)bindable).OnItemTemplateChanged((DataTemplate)oldValue, (DataTemplate)newValue));

        private readonly Label noItemsLabel;
        private StackLayout stack;

        public ItemsControl()
        {
            this.Padding = new Thickness(5, 0, 5, 5);

            this.noItemsLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            this.noItemsLabel.SetBinding(Label.StyleProperty, new Binding(nameof(this.ItemStyle), source: this));
            this.noItemsLabel.SetBinding(Label.TextProperty, new Binding(nameof(this.PlaceholderText), source: this));

            this.Content = this.noItemsLabel;
        }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>The placeholder text.</value>
        public string PlaceholderText
        {
            get => (string)this.GetValue(PlaceholderTextProperty);
            set => this.SetValue(PlaceholderTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the Orientation for the default layout panel
        /// This is not used if you replace the panel!
        /// </summary>
        /// <value>Orientation value</value>
        public StackOrientation Orientation
        {
            get => (StackOrientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the Spacing for the default layout panel
        /// This is not used if you replace the panel!
        /// </summary>
        /// <value>Spacing value</value>
        public double Spacing
        {
            get => (double)this.GetValue(SpacingProperty);
            set => this.SetValue(SpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the item style used for dynamically generated labels.
        /// This is not used if you apply a DataTemplate
        /// </summary>
        /// <value>The item style.</value>
        public Style ItemStyle
        {
            get => (Style)this.GetValue(ItemStyleProperty);
            set => this.SetValue(ItemStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the container used for the layout panel
        /// If not set, a StackLayout is used.
        /// </summary>
        /// <value>Orientation value</value>
        public Layout<View> ItemsPanel
        {
            get => (Layout<View>)this.GetValue(ItemsPanelProperty);
            set => this.SetValue(ItemsPanelProperty, value);
        }

        /// <summary>
        /// Gets or sets the items source - can be any collection of elements.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource
        {
            get => (IList)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the item template used to generate the visuals for a single item.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Retrieve or create the container for children
        /// </summary>
        /// <param name="createDefaultContainer">True to create default container if ItemsPanel is not set</param>
        /// <returns>Layout container</returns>
        private Layout<View> GetOrCreateLayoutContainer(bool createDefaultContainer)
        {
            if (this.ItemsPanel != null)
            {
                return this.ItemsPanel;
            }

            if (createDefaultContainer && this.stack == null)
            {
                this.stack = new StackLayout
                {
                    Orientation = this.Orientation,
                    Spacing = this.Spacing,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
            }

            return this.stack;
        }

        /// <summary>
        /// Instance method used to change the current orientation of the layout panel.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void OnOrientationPropertyChanged(StackOrientation oldValue, StackOrientation newValue)
        {
            if (this.stack != null && oldValue != newValue)
            {
                this.stack.Orientation = newValue;
            }
        }

        /// <summary>
        /// Instance method used to change the current spacing of the layout panel.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void OnSpacingPropertyChanged(double oldValue, double newValue)
        {
            if (this.stack != null && oldValue != newValue)
            {
                this.stack.Spacing = newValue;
            }
        }

        /// <summary>
        /// This is called when the ItemsPanel property is changed. We need to clear the old object and
        /// fill our data into the new one.
        /// </summary>
        /// <param name="oldValue">Old panel</param>
        /// <param name="newValue">New panel</param>
        private void OnItemsPanelPropertyChanged(Layout<View> oldValue, Layout<View> newValue)
        {
            this.Content = null; // temporarily show nothing.

            if (this.stack != null)
            {
                this.stack.Children.Clear();
                if (newValue != null)
                {
                    this.stack = null;
                }
            }

            oldValue?.Children.Clear();

            // Will recreate the container if necessary
            this.FillContainer(newValue, this.ItemsSource, this.ItemTemplate);
        }

        /// <summary>
        /// Instance method used to change the ItemTemplate for each rendered item.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void OnItemTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            if (oldValue == newValue)
            {
                return;
            }

            IList data = this.ItemsSource;
            if (data?.Count > 0)
            {
                // Remove all the generated visuals
                Layout<View> container = this.GetOrCreateLayoutContainer(true);
                container.Children.Clear();

                // Regenerate
                this.FillContainer(container, data, newValue);
            }
        }

        /// <summary>
        /// Instance method called when the underlying data source is changed through the
        /// <see cref="ItemsSource"/> property. This re-generates the list based on the new collection.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void OnItemsSourceChanged(IList oldValue, IList newValue)
        {
            // Unsubscribe from the old collection
            if (oldValue != null && oldValue is INotifyCollectionChanged oldValueNcc)
            {
                oldValueNcc.CollectionChanged -= this.OnCollectionChanged;
            }

            if (newValue == null)
            {
                Layout<View> container = this.GetOrCreateLayoutContainer(false);
                if (container != null)
                {
                    container.Children.Clear();
                    this.stack = null;
                }

                this.Content = this.noItemsLabel;
            }
            else
            {
                this.FillContainer(null, newValue, this.ItemTemplate);
                if (newValue is INotifyCollectionChanged ncc)
                {
                    ncc.CollectionChanged += this.OnCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Instance method called when the label style is changed through the
        /// <see cref="ItemStyle"/> property. This applies the new style to all the labels.
        /// </summary>
        /// <param name="style">Style.</param>
        private void OnItemStylePropertyChanged(Style style)
        {
            // Ignore if we have a data template.
            if (this.ItemTemplate != null)
            {
                return;
            }

            Layout<View> container = this.GetOrCreateLayoutContainer(false);
            if (container == null)
            {
                return;
            }

            foreach (View view in container.Children)
            {
                if (view is Label label)
                {
                    if (style == null)
                    {
                        label.ClearValue(Label.StyleProperty);
                    }
                    else
                    {
                        label.Style = style;
                    }
                }
            }
        }

        /// <summary>
        /// This method takes our items source and generates visuals for
        /// each item in the collection; it can reuse visuals which were created
        /// previously and simply changes the binding context.
        /// </summary>
        /// <param name="container">Visual container to add items to</param>
        /// <param name="newValue">New items to display</param>
        /// <param name="itemTemplate">ItemTemplate to use (null for Label)</param>
        private void FillContainer(Xamarin.Forms.Layout<View> container, IList newValue, DataTemplate itemTemplate)
        {
            if (container == null)
            {
                container = this.GetOrCreateLayoutContainer(true);
            }

            // No items? Show the "no content" label.
            if (newValue == null || newValue.Count == 0)
            {
                this.Content = this.noItemsLabel;
                container.Children.Clear();
                this.stack = null;
                return;
            }

            // Add items
            Style itemStyle = this.ItemStyle;
            IList<View> visuals = container.Children;

            for (int i = 0; i < visuals.Count; i++)
            {
                visuals[i].IsVisible = i < newValue.Count;
            }

            for (int i = 0; i < newValue.Count; i++)
            {
                object dataItem = newValue[i];

                if (visuals.Count > i)
                {
                    if (itemTemplate != null)
                    {
                        View visualItem = visuals[i];
                        visualItem.BindingContext = dataItem;
                    }
                    else
                    {
                        Label visualItem = (Label)visuals[i];
                        visualItem.Text = dataItem.ToString();
                        if (itemStyle != null)
                        {
                            visualItem.Style = itemStyle;
                        }
                        else
                        {
                            visualItem.ClearValue(Label.StyleProperty);
                        }
                    }
                }
                else
                {
                    if (itemTemplate != null)
                    {
                        View view = this.InflateTemplate(itemTemplate, dataItem);
                        container.Children.Add(view);
                    }
                    else
                    {
                        Label label = new Label { Text = dataItem.ToString() };
                        if (itemStyle != null)
                        {
                            label.Style = itemStyle;
                        }

                        container.Children.Add(label);
                    }
                }
            }

            this.Content = container;
        }

        /// <summary>
        /// Inflates the visuals for a data template or template selector
        /// and adds it to our StackLayout.
        /// </summary>
        /// <param name="template">Template.</param>
        /// <param name="item">Item.</param>
        /// <returns>The view.</returns>
        private View InflateTemplate(DataTemplate template, object item)
        {
            // Pull real template from selector if necessary.
            if (template is DataTemplateSelector dSelector)
            {
                template = dSelector.SelectTemplate(item, this);
            }

            View view = template.CreateContent() as View;
            if (view != null)
            {
                view.BindingContext = item;
            }

            return view;
        }

        /// <summary>
        /// This is called when the data source collection implements
        /// collection change notifications and the data has changed.
        /// This is not optimized - it simply replaces all the data.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => this.FillContainer(null, (IList)sender, this.ItemTemplate);
    }
}