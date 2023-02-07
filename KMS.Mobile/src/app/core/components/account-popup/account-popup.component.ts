import { Component, ElementRef, HostListener, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
	selector: 'account-popup',
	templateUrl: './account-popup.component.html',
	styleUrls: ['./account-popup.component.scss'],
})
export class AccountPopupComponent implements OnInit {
	constructor(private element: ElementRef, private accountService: AccountService) {
		// Nothing
	}

	@HostListener('document:click', ['$event'])
	private clickOut(event: Event): void {
		if (
			this.isExpanded &&
			!(event.target as HTMLElement).classList.contains('account-popup-toggle') &&
			// eslint-disable-next-line @typescript-eslint/no-unsafe-member-access, @typescript-eslint/no-unsafe-call
			!this.element.nativeElement.contains(event.target)
		) {
			this.isExpanded = false;
		}
	}

	public fullName: string = '';
	public role: string = '';
	public isExpanded: boolean = false;
	public isAuthorized: boolean = false;

	public ngOnInit(): void {
		this.accountService.currentUser.subscribe({
			next: (user) => {
				if (!user) {
					return;
				}

				this.fullName = `${user.firstName} ${user.lastName}`;
				this.role = user.roles.map((role) => role.name).join('/');
				this.isAuthorized = true;
			},
		});
	}
}
