﻿namespace KMS.Core.Aggregates.Trip.Requests;

public class GetTripRequest
{
    public Guid? DriverId { get; set; }

    public DateTime? DepartedAt { get; set; }

    public DateTime? ArrivedAt { get; set; }

    public int? Status { get; set; }
}
