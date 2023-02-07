using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[UtilsException(StatusCodes.Status404NotFound, "ERROR_TRIP_CANNOT_BE_DELETED")]
public class TripCannotBeDeletedException : Exception
{
    public TripCannotBeDeletedException()
    {
    }

    public TripCannotBeDeletedException(string message) : base(message)
    {
    }

    public TripCannotBeDeletedException(Guid id) : base($"Trip {id} can not be deleted.")
    {
    }

    public TripCannotBeDeletedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TripCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
