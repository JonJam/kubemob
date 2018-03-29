using System.Collections.Generic;
using System.Linq;
using KubeMob.Common.ViewModels.Base;

namespace KubeMob.Common.Validation
{
    // TODO what can be made private, get only etc
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private readonly IList<IValidationRule<T>> validationRules;

        private IList<string> errors;
        private T value;
        private bool isValid;

        public ValidatableObject(IList<IValidationRule<T>> validationRules)
        {
            this.validationRules = validationRules;

            this.errors = new List<string>();
            this.isValid = true;
        }

        public IList<string> Errors
        {
            get => this.errors;
            private set => this.SetProperty(ref this.errors, value);
        }

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
