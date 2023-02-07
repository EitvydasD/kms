import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetUserRequest, User, UserUpdateRequest } from '../../types/user.types';

@Injectable({
	providedIn: 'root',
})
export class UserService {
	constructor(private http: HttpClient) {
		// Nothing
	}

	public getUsers(request?: GetUserRequest): Observable<User[]> {
		return this.http.get<User[]>(this.getEndpoint(), {
			params: {
				isDriver: request?.isDriver?.toString() ?? '',
			},
		});
	}

	public getUser(id: string): Observable<User> {
		return this.http.get<User>(this.getEndpoint(id));
	}

	public updateUser(id: string, request: UserUpdateRequest): Observable<User> {
		return this.http.put<User>(this.getEndpoint(id), request);
	}

	private getEndpoint(id?: string): string {
		if (id) {
			return `api/user/${id}`;
		}

		return `api/user`;
	}
}
