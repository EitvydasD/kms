import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { firstValueFrom } from 'rxjs';
import { AvailableCultures } from './constants/culture.constants';
import { AccountService } from './services/account.service';
import { AuthService } from './services/api/auth.service';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
declare let require: any;

@Injectable({ providedIn: 'root' })
export class AppInitializer {
	constructor(
		private translateService: TranslateService,
		private authService: AuthService,
		private accountService: AccountService,
		private router: Router,
	) {
		// Nothing
	}

	public async initialize(): Promise<void> {
		try {
			AvailableCultures.forEach((x) => {
				this.translateService.setTranslation(
					`${x.isoCode}`,
					// eslint-disable-next-line @typescript-eslint/no-unsafe-argument, @typescript-eslint/no-unsafe-call
					require(`../../assets/i18n/${x.isoCode}.json`),
				);
			});
			const defaultCulture = AvailableCultures.find((x) => x.default === true);

			// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
			this.translateService.setDefaultLang(defaultCulture!.isoCode);

			if (this.authService.isAuthenticated) {
				this.authService.isAuthenticated = true;
				await firstValueFrom(this.accountService.getCurrentUser());
				await firstValueFrom(this.accountService.getCurrentPermissions());
			}
		} catch (error) {
			this.router.navigate(['/logout']);
		} finally {
			// ignore
		}
	}
}
