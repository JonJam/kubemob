﻿using System.Collections.Generic;
using System.Linq;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Validation
{
    [Preserve(AllMembers = true)]
    public class ValidatableObject<T> : ExtendedBindableObject
    {
        private readonly IList<IValidationRule<T>> validationRules;

        private IList<string> errors;
        private T value;
        private bool isValid;

        public ValidatableObject(IList<IValidationRule<T>> validationRules)
        {
            this.validationRules = validationRules;

            this.Errors = new List<string>();
            this.IsValid = true;
        }

        public IList<string> Errors
        {
            get => this.errors;
            private set
            {
                if (this.SetProperty(ref this.errors, value))
                {
                    this.NotifyPropertyChanged(() => this.FirstError);
                }
            }
        }

        public string FirstError => this.Errors.FirstOrDefault();

        public T Value
        {
            get => this.value;
            set => this.SetProperty(ref this.value, value);
        }

        public bool IsValid
        {
            get => this.isValid;
            private set => this.SetProperty(ref this.isValid, value);
        }

        public bool Validate()
        {
            this.Errors.Clear();

            this.Errors = this.validationRules.Where(v => !v.Check(this.Value)).Select(v => v.ValidationMessage).ToList();
            this.IsValid = !this.Errors.Any();

            return this.IsValid;
        }
    }
}
