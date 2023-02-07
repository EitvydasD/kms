import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, tap } from 'rxjs';
import { AlertService } from '../services/alert.service';

@Injectable()
export class ErrorNotificationInterceptor implements HttpInterceptor {
	constructor(private alertService: AlertService, private translateService: TranslateService) {
		// Nothing
	}

	public intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		return next.handle(request).pipe(
			tap({
				error: (error) => {
					const object = this.getErrorObject(error);
					this.alertService.error(object.title, object.message);
				},
			}),
		);
	}

	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	private getErrorObject(error: any): { title: string; message?: string } {
		// eslint-disable-next-line @typescript-eslint/no-unsafe-assignment, @typescript-eslint/no-unsafe-member-access
		const code = error?.error?.ErrorCode;

		if (code) {
			return { title: this.translateService.instant(`ERROR.${code as string}`) as string };
		}

		return {
			title: this.translateService.instant('ERROR.ERROR_UNKNOWN') as string,
			message: this.translateService.instant('ERROR.MESSAGES.CONTACT_ADMIN') as string,
		};
	}
}
