import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Role } from '../types/role.types';

@Injectable({
	providedIn: 'root',
})
export class RoleService {
	constructor(private http: HttpClient) {
		// Nothing
	}

	public getRoles(): Observable<Role[]> {
		return this.http.get<Role[]>(this.getEndpoint());
	}

	private getEndpoint(id?: string): string {
		if (id) {
			return `api/role/${id}`;
		}

		return `api/role`;
	}
}
