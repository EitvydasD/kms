import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { zip } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { UserService } from '../../../core/services/api/user.service';
import { User } from '../../../core/types/user.types';

@Component({
	selector: 'user-list',
	templateUrl: './user-list.component.html',
})
export class UserListComponent implements OnInit {
	constructor(
		private userService: UserService,
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private accountService: AccountService,
	) {
		// Nothing
	}

	public users: User[] = [];
	private currentUser?: User;

	public ngOnInit(): void {
		zip([this.userService.getUsers(), this.accountService.currentUser]).subscribe({
			next: (response) => {
				this.users = response[0];
				this.currentUser = response[1] ?? undefined;
			},
		});
	}

	public openDetails(user: User): void {
		if (this.currentUser?.id === user.id) {
			this.router.navigate(['/profile']);
			return;
		}

		this.router.navigate([user.id], { relativeTo: this.activatedRoute });
	}
}
