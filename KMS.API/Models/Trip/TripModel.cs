﻿using Ardalis.SmartEnum.SystemTextJson;
using KMS.API.Models.User;
using KMS.Core.Aggregates.Trip;
using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Aggregates.User.Models;
using System.Text.Json.Serialization;

namespace KMS.API.Models.Trip;

public class TripModel
{
    public Guid Id { get; init; }

    public BaseUserModel? Driver { get; init; }

    public DateTime? DepartedAt { get; init; }

    public DateTime? ArrivedAt { get; init; }

    public List<UserModel> Responsible { get; init; } = new();

    [JsonConverter(typeof(SmartEnumValueConverter<TripStatus, int>))]
    public TripStatus Status { get; init; } = TripStatus.Pending;

    public TripEntity ToEntity(Guid? tripId) => new()
    {
        Id = tripId ?? Id,
        DriverId = Driver?.Id ?? Guid.Empty,
        DepartedAt = DepartedAt,
        ArrivedAt = ArrivedAt,
        Status = Status,
        Responsible = new List<UserTripEntity>(Responsible.Select(x => new UserTripEntity
            {
                UserId = x.Id,
                TripId = tripId ?? Id
            }))
    };
    
}
