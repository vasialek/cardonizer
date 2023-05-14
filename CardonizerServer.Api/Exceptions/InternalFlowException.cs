using CardonizerServer.Api.Models;

namespace CardonizerServer.Api.Exceptions;

public class InternalFlowException : Exception
{
    public ErrorCodes ErrorCode { get; }

    public InternalFlowException(ErrorCodes errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}
