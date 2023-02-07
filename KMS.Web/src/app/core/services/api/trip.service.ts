import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTripRequest, GetTripRequest, Trip } from '../../types/trip.types';

@Injectable({
	providedIn: 'root',
})
export class TripService {
	constructor(private http: HttpClient) {
		// Nothing
	}

	public getTrips(request: GetTripRequest): Observable<Trip[]> {
		return this.http.get<Trip[]>(this.getEndpoint(), {
			params: {
				arrivedAt: request.arrivedAt?.toISOString() ?? '',
				departedAt: request.departedAt?.toISOString() ?? '',
				status: request.status?.toString() ?? '',
				driverId: request.driverId ?? '',
			},
		});
	}

	public getTrip(tripId: string): Observable<Trip> {
		return this.http.get<Trip>(this.getEndpoint(tripId));
	}

	public createTrip(request: CreateTripRequest): Observable<Trip> {
		return this.http.post<Trip>(this.getEndpoint(), request);
	}

	public updateTrip(tripId: string, request: Trip): Observable<Trip> {
		return this.http.put<Trip>(this.getEndpoint(tripId), request);
	}

	public deleteTrip(tripId: string): Observable<void> {
		return this.http.delete<void>(this.getEndpoint(tripId));
	}

	private getEndpoint(tripId?: string) {
		if (tripId) {
			return `api/trip/${tripId}`;
		}
		return 'api/trip';
	}
}
