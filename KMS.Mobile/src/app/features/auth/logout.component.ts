import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/api/auth.service';

@Component({
	selector: 'logout',
	template: '',
})
export class LogoutComponent implements OnInit {
	constructor(private authService: AuthService) {
		// Nothing
	}

	public ngOnInit(): void {
		this.authService.logout();
	}
}
