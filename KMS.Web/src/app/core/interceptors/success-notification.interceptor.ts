import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { AlertService } from '../services/alert.service';

@Injectable()
export class SuccessNotificationInterceptor implements HttpInterceptor {
	constructor(private alertService: AlertService) {
		// Nothing
	}

	public intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		// Exclude GET and OPTIONS requests
		if (request.method === 'GET' || request.method === 'OPTIONS') {
			return next.handle(request);
		}

		// Exclude reference check
		if (request.url.includes('/auth/')) {
			return next.handle(request);
		}

		return next.handle(request).pipe(tap({ complete: () => this.alertService.success() }));
	}
}
