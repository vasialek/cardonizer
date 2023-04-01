namespace CardonizerServer.Api.Exceptions;

public class InternalFlowException : Exception
{
    public InternalFlowException(string message)
        : base(message)
    {
    }
}