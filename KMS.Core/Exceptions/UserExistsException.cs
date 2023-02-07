using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[UtilsException(StatusCodes.Status409Conflict, "ERROR_USER_EXISTS")]
public class UserExistsException : Exception
{
    public UserExistsException()
    {
    }

    public UserExistsException(string message) : base(message)
    {
    }

    public UserExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
