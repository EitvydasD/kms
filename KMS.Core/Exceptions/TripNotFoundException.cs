using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using Utils.Library.Exceptions;

namespace KMS.Core.Exceptions;

[UtilsException(StatusCodes.Status404NotFound, "ERROR_TRIP_NOT_FOUND")]
public class TripNotFoundException : Exception
{
    public TripNotFoundException()
    {
    }

    public TripNotFoundException(string message) : base(message)
    {
    }

    public TripNotFoundException(Guid id) : base($"Trip {id} not found.")
    {
    }

    public TripNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TripNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
