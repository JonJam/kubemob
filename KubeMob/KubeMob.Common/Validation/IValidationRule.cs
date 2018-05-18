namespace KubeMob.Common.Validation
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/validation#specifying-validation-rules"/>
    /// </summary>
    /// <typeparam name="T">The type to validate.</typeparam>
    public interface IValidationRule<in T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
}
