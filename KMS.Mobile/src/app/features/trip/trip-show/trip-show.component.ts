import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, zip } from 'rxjs';
import { PermissionId } from '../../../core/enums/permission.enums';
import { AccountService } from '../../../core/services/account.service';
import { TripService } from '../../../core/services/api/trip.service';
import { UserService } from '../../../core/services/api/user.service';
import { CreateTripRequest, Trip, TripHelpers } from '../../../core/types/trip.types';
import { User } from '../../../core/types/user.types';

@Component({
	selector: 'app-trip-show',
	templateUrl: './trip-show.component.html',
})
export class TripShowComponent implements OnInit {
	constructor(
		private tripService: TripService,
		private activatedRoute: ActivatedRoute,
		private userService: UserService,
		private accountService: AccountService,
		private router: Router,
	) {
		// Nothing
	}

	public trip?: Trip;
	public form?: UntypedFormGroup;
	public users: User[] = [];
	public drivers: User[] = [];
	public permissions: string[] = [];
	public isNew = false;

	public statuses = [
		{ value: 10, name: 'Pending' },
		{ value: 20, name: 'InProgress' },
		{ value: 30, name: 'Delivered' },
	];

	public ngOnInit(): void {
		zip([this.activatedRoute.params, this.accountService.currentPermissions]).subscribe({
			next: (response) => {
				const id = response[0]['id'] as string;
				this.permissions = response[1];

				if (this.permissions.includes(PermissionId.UserView)) {
					zip([this.userService.getUsers(), this.userService.getUsers({ isDriver: true })]).subscribe({
						next: (response2) => {
							this.users = response2[0];
							this.drivers = response2[1];
						},
					});
				}

				if (id === 'create') {
					this.isNew = true;
				}

				this.getTrip(id);
			},
		});
	}

	private getTrip(id: string): void {
		const sub = this.isNew ? of(TripHelpers.createNew()) : this.tripService.getTrip(id);

		sub.subscribe({
			next: (trip) => {
				this.trip = trip;

				this.form = TripHelpers.createForm(this.trip);
			},
		});
	}

	public get canModifyTrip(): boolean {
		return this.permissions.includes(PermissionId.TripModify);
	}

	public get canDeleteTrip(): boolean {
		return this.canModifyTrip && this.trip?.status === 10;
	}

	public onSave(): void {
		if (!this.trip) {
			return;
		}
		const trip = this.form?.getRawValue() as Trip;
		if (this.isNew) {
			this.createTrip(trip);
		} else {
			this.updateTrip(this.trip.id ?? '', trip);
		}
	}

	private createTrip(trip: Trip): void {
		const request: CreateTripRequest = {
			departedAt: trip.departedAt,
			arrivedAt: trip.arrivedAt,
			status: trip.status,
			driverId: trip.driver?.id ?? '',
		};

		this.tripService.createTrip(request).subscribe({
			next: (response) => {
				this.trip = response;
				this.isNew = false;
				this.form = TripHelpers.createForm(this.trip);
				this.router.navigate(['/trips', response.id]);
			},
		});
	}

	private updateTrip(tripId: string, trip: Trip): void {
		this.tripService.updateTrip(tripId, trip).subscribe({
			next: (response) => {
				this.trip = response;
				this.form = TripHelpers.createForm(this.trip);
			},
		});
	}

	public deleteTrip(): void {
		if (!this.trip) {
			return;
		}
		this.tripService.deleteTrip(this.trip.id).subscribe({
			next: () => {
				this.router.navigate(['/trips']);
			},
		});
	}
}
