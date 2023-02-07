import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/api/auth.service';
import { RegisterRequest } from '../../../core/types/auth.types';

@Component({
	selector: 'register',
	templateUrl: './register.component.html',
})
export class RegisterComponent {
	constructor(private authService: AuthService) {
		// Nothing
	}

	public form: FormGroup = new FormGroup({
		firstName: new FormControl<string>('', [Validators.required]),
		lastName: new FormControl<string>('', [Validators.required]),
		email: new FormControl<string>('', [Validators.required, Validators.email]),
		phone: new FormControl<string>('', [Validators.required]),
		username: new FormControl<string>('', [Validators.required]),
		password: new FormControl<string>('', [Validators.required]),
	});

	public signUp(request: RegisterRequest): void {
		this.authService.register(request).subscribe();
	}
}
