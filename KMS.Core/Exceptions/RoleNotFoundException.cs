using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[UtilsException(StatusCodes.Status404NotFound, "ERROR_ROLE_NOT_FOUND")]
public class RoleNotFoundException : Exception
{
    public RoleNotFoundException()
    {
    }

    public RoleNotFoundException(string message) : base(message)
    {
    }

    public RoleNotFoundException(Guid id) : base($"Role {id} not found.")
    {
    }

    public RoleNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected RoleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
