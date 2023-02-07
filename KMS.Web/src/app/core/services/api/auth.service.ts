import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin, Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, RegisterRequest } from '../../types/auth.types';
import { AccountService } from '../account.service';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	constructor(private http: HttpClient, private accountService: AccountService, private router: Router) {
		this.isAuthenticated = this.getToken() !== null;
	}

	public isAuthenticated: boolean = false;

	private readonly tokenStorage: string = 'kms-authentication';
	private readonly loginRoute: string = '/login';

	public login(request: LoginRequest): Observable<LoginResponse> {
		return this.http.post<LoginResponse>(`/api/auth/login`, request).pipe(
			tap({
				next: (response) => {
					localStorage.setItem(this.tokenStorage, response.accessToken);
					this.isAuthenticated = true;

					forkJoin([this.accountService.getCurrentUser(), this.accountService.getCurrentPermissions()]).subscribe({
						next: () => {
							this.router.navigate(['/trips']);
						},
					});
				},
			}),
		);
	}

	public register(request: RegisterRequest): Observable<void> {
		return this.http.post<void>('/api/auth/register', request).pipe(
			tap({
				next: () => {
					this.router.navigate([this.loginRoute]);
				},
			}),
		);
	}

	public logout(): void {
		this.isAuthenticated = false;
		localStorage.removeItem(this.tokenStorage);
		this.accountService.discardUserDataOnSignOut();
		this.router.navigate([this.loginRoute]);
	}

	public getToken(): string | null {
		return localStorage.getItem(this.tokenStorage);
	}
}
