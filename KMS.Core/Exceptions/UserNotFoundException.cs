using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[UtilsException(StatusCodes.Status404NotFound, "ERROR_USER_NOT_FOUND")]
public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(Guid id) : base($"User {id} not found.")
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
