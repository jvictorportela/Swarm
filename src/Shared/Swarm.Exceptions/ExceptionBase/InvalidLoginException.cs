namespace Swarm.Exceptions.ExceptionBase;

public class InvalidLoginException : SwarmException
{
    public InvalidLoginException() : base(ResourceMessagesExceptions.EMAIL_OR_PASSWORD_INVALID)
    {

    }
}
