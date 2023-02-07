using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[Serializable]
[UtilsException(StatusCodes.Status400BadRequest, "ERROR_INVALID_PASSWORD")]
public class InvalidCurrentPasswordException : Exception
{
    public InvalidCurrentPasswordException()
    {
    }

    public InvalidCurrentPasswordException(string? message) : base(message)
    {
    }

    public InvalidCurrentPasswordException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidCurrentPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
