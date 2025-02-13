namespace Swarm.Exceptions.ExceptionBase;

public class ErrorOnValidationException : SwarmException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }
}
