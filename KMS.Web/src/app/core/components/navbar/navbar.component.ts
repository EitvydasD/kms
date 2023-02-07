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
		this.isAuthorized = this.authService.isAuthenticated;
	}

	public signOut(): void {
		this.authService.logout();
	}
}
