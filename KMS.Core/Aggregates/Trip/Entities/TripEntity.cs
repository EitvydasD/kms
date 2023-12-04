public static TripEntity Create(Guid callerId, CreateTripRequest request)
    {
        var tripId = Guid.NewGuid();

        var trip = new TripEntity
        {
            Id = tripId,
            DriverId = request.DriverId ?? callerId,
            DepartedAt = request.DepartedAt,
            ArrivedAt = request.ArrivedAt,
            Status = request.Status,
            Responsible = new List<UserTripEntity>
                {
                    new UserTripEntity{ UserId = callerId, TripId = tripId }
                },
        };

        return trip;
    }

    public void Update(TripEntity request)
    {
        DriverId = request.DriverId;
        DepartedAt = request.DepartedAt;
        ArrivedAt = request.ArrivedAt;
        Status = request.Status;
        Responsible = request.Responsible;
    }
