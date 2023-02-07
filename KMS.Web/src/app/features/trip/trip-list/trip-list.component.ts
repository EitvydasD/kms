import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { debounceTime, switchMap } from 'rxjs';
import { PermissionId } from '../../../core/enums/permission.enums';
import { AccountService } from '../../../core/services/account.service';
import { TripService } from '../../../core/services/api/trip.service';
import { UserService } from '../../../core/services/api/user.service';
import { GetTripRequest, Trip } from '../../../core/types/trip.types';
import { User } from '../../../core/types/user.types';

@Component({
	selector: 'app-trip-list',
	templateUrl: './trip-list.component.html',
})
export class TripListComponent implements OnInit {
	constructor(
		private tripService: TripService,
		private route: ActivatedRoute,
		private router: Router,
		private userService: UserService,
		private accountService: AccountService,
	) {
		// Nothing
	}

	public request?: GetTripRequest;
	public hasPermissionViewAll: boolean = false;
	public trips: Trip[] = [];
	public statuses = [
		{ value: 10, name: 'Pending' },
		{ value: 20, name: 'InProgress' },
		{ value: 30, name: 'Delivered' },
	];

	public drivers: User[] = [];

	public ngOnInit(): void {
		this.accountService.currentPermissions.subscribe({
			next: (permissions) => {
				this.hasPermissionViewAll = permissions.includes(PermissionId.TripViewAll);

				if (permissions.includes(PermissionId.UserView)) {
					this.userService.getUsers({ isDriver: true }).subscribe({
						next: (response) => {
							this.drivers = response;
							this.initParameterMap();
						},
					});
				} else {
					this.initParameterMap();
				}
			},
		});
	}

	public getStatusName(status: number): string {
		const statusItem = this.statuses.find((item) => item.value === status);
		return statusItem ? statusItem.name : '';
	}

	private initParameterMap(): void {
		this.route.queryParamMap
			.pipe(
				debounceTime(250),

				switchMap((params) => {
					this.request = {
						driverId: params.get('driverId'),
						arrivedAt: params.get('arrivedAt') ? new Date(params.get('arrivedAt') ?? '') : null,
						departedAt: params.get('departedAt') ? new Date(params.get('departedAt') ?? '') : null,
						status: params.get('status') ? parseInt(params.get('status') ?? '') : null,
					};
					return this.tripService.getTrips(this.request);
				}),
			)
			.subscribe({
				next: (response) => {
					this.trips = response;
				},
			});
	}

	public onFilterChange(params: Params): void {
		const queryParams: Params = params;
		if (params['skip'] === undefined) {
			queryParams['skip'] = 0;
		}

		this.router.navigate(['.'], {
			relativeTo: this.route,
			queryParamsHandling: 'merge',
			queryParams: params,
		});
	}

	public openNew(): void {
		this.router.navigate(['create'], { relativeTo: this.route });
	}

	public openDetails(trip: Trip): void {
		this.router.navigate([trip.id], { relativeTo: this.route });
	}
}
