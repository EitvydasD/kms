import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/api/auth.service';

@Component({
	selector: 'navbar',
	templateUrl: './navbar.component.html',
	styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
	constructor(private authService: AuthService) {
		// Nothing
	}

	public isAuthorized: boolean = false;

	public ngOnInit(): void {
		this.authService.isAuthenticatedSubject.subscribe({
			next: (isAuthenticated) => {
				this.isAuthorized = isAuthenticated;
			},
		});
	}

	public signOut(): void {
		this.authService.logout();
	}

	public toggleSidebar = (): void => {
		const layoutDiv = <HTMLElement>document.getElementById('MainLayout');

		layoutDiv.classList.toggle('g-sidenav-pinned');
		layoutDiv.classList.toggle('g-sidenav-hidden');
	};
}
