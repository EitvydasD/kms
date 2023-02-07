import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { User } from './user.types';

export type Trip = {
	id: string;
	driver?: User;
	departedAt?: Date;
	arrivedAt?: Date;
	status: number;
	responsible: User[];
};

export type GetTripRequest = {
	departedAt: Date | null;
	arrivedAt: Date | null;
	status: number | null;
	driverId: string | null;
};

export type CreateTripRequest = {
	departedAt?: Date;
	arrivedAt?: Date;
	status: number;
	driverId: string;
};

export class TripHelpers {
	public static createNew(): Trip {
		return {
			id: '',
			status: 10,
			responsible: [],
		};
	}

	public static createForm(trip: Trip): UntypedFormGroup {
		return new FormGroup({
			driver: new FormControl<User | undefined>(trip.driver, [Validators.required]),
			departedAt: new FormControl<Date | undefined>(trip.departedAt ? new Date(trip.departedAt) : undefined),
			arrivedAt: new FormControl<Date | undefined>(trip.arrivedAt ? new Date(trip.arrivedAt) : undefined),
			status: new FormControl<number>(trip.status, [Validators.required]),
			responsible: new FormControl<User[]>(trip.responsible),
		});
	}
}
