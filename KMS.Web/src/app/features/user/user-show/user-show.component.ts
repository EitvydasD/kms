import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FilterableDataSource } from 'angular-helper-utils';
import { finalize, of, zip } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { UserService } from '../../../core/services/api/user.service';
import { RoleService } from '../../../core/services/role.service';
import { Role } from '../../../core/types/role.types';
import { User, UserHelpers, UserUpdateRequest } from '../../../core/types/user.types';

@Component({
	selector: 'user-show',
	templateUrl: './user-show.component.html',
})
export class UserShowComponent implements OnInit {
	constructor(
		private activatedRoute: ActivatedRoute,
		private router: Router,
		private userService: UserService,
		private roleService: RoleService,
		private accountService: AccountService,
	) {
		// Nothing
	}

	@Input()
	public isProfile = false;

	public user?: User;
	public form?: UntypedFormGroup;
	public roles = new FilterableDataSource<Role>();
	private currentUser?: User;

	public ngOnInit(): void {
		zip([this.activatedRoute.params, this.accountService.currentUser, this.accountService.currentPermissions]).subscribe({
			next: (response) => {
				const id = response[0]['id'] as string;

				this.currentUser = response[1] ?? undefined;
				if (response[1]?.id === id) {
					this.router.navigate(['/users/profile']);
					return;
				}

				this.getUser(id);
			},
		});
	}

	private getUser(id: string): void {
		const sub = this.isProfile ? of(this.currentUser) : this.userService.getUser(id);

		sub.subscribe({
			next: (user) => {
				this.user = user;
				this.form = UserHelpers.createForm(user as User, this.isProfile);
			},
		});
	}

	public onRolesOpen(): void {
		if (this.roles.isLoaded) {
			return;
		}

		this.roles.setIsLoading();
		this.roleService
			.getRoles()
			.pipe(finalize(() => this.roles.setIsLoading(false)))
			.subscribe({
				next: (roles) => {
					this.roles.set(roles);
				},
			});
	}

	public onSave(): void {
		const request = this.form?.getRawValue() as UserUpdateRequest;
		const sub = this.isProfile
			? this.accountService.saveProfile(request)
			: this.userService.updateUser(this.user?.id as string, request);
		sub.subscribe({});
	}
}
