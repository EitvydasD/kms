import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ChangePasswordRequest, User, UserUpdateRequest } from '../types/user.types';

@Injectable({
	providedIn: 'root',
})
export class AccountService {
	constructor(private http: HttpClient) {
		// Nothing
	}

	private currentUserSubject = new BehaviorSubject<User | null>(null);
	private currentPermissionsSubject = new BehaviorSubject<string[]>([]);

	public get currentUser(): Observable<User | null> {
		return this.currentUserSubject.asObservable();
	}

	public get currentPermissions(): Observable<string[]> {
		return this.currentPermissionsSubject.asObservable();
	}

	public getCurrentUser(): Observable<User> {
		return this.http.get<User>('api/profile').pipe(
			tap({
				next: (account) => {
					this.currentUserSubject.next(account);
				},
			}),
		);
	}

	public getCurrentPermissions(): Observable<string[]> {
		return this.http.get<string[]>('api/profile/permissions').pipe(
			tap({
				next: (permissions) => {
					this.currentPermissionsSubject.next(permissions);
				},
			}),
		);
	}

	public saveProfile(request: UserUpdateRequest): Observable<User> {
		return this.http.put<User>(`api/profile`, request).pipe(
			tap({
				next: (user) => {
					this.currentUserSubject.next(user);
				},
			}),
		);
	}

	public changePassword(request: ChangePasswordRequest): Observable<void> {
		return this.http.put<void>(`api/profile/password`, request);
	}

	private discardCurrentUser(): void {
		this.currentUserSubject.next(null);
	}

	public discardUserDataOnSignOut(): void {
		this.discardCurrentUser();
	}
}
