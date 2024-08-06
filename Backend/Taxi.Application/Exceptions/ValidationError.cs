namespace Taxi.Application.Exceptions
{
    public sealed class ValidationError(string PropertyName, string ErrorMessage);

}