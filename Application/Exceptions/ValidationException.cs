using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation errors have occurred")
        {
            Errors = new();
        }

        public List<string> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (ValidationFailure failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
