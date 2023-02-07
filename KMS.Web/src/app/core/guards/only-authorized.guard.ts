import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment } from '@angular/router';
import { AuthService } from '../services/api/auth.service';

@Injectable({ providedIn: 'root' })
export class OnlyAuthorizedGuard implements CanActivate, CanLoad {
	constructor(private router: Router, private authService: AuthService) {
		// Nothing
	}

	// eslint-disable-next-line @typescript-eslint/no-unused-vars
	public canLoad(route: Route, segments: UrlSegment[]): boolean {
		return this.canAccess();
	}

	// eslint-disable-next-line @typescript-eslint/no-unused-vars
	public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
		return this.canAccess();
	}

	public canAccess(): boolean {
		if (this.authService.isAuthenticated) {
			return true;
		}

		this.router.navigate(['/login']);

		return false;
	}
}
