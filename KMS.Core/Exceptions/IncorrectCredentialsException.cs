using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[Serializable]
[UtilsException(StatusCodes.Status400BadRequest, "ERROR_INCORRECT_CREDENTIALS")]
public class IncorrectCredentialsException : Exception
{
    public IncorrectCredentialsException()
    {
    }

    public IncorrectCredentialsException(string message) : base(message)
    {
    }

    public IncorrectCredentialsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected IncorrectCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
