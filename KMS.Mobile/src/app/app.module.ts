import { registerLocaleData } from '@angular/common';
import { APP_INITIALIZER, Injector, NgModule } from '@angular/core';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import localeEn from '@angular/common/locales/en';
import localeLt from '@angular/common/locales/lt';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateModule } from '@ngx-translate/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppInitializer } from './core/app.initializer';
import { AccountPopupComponent } from './core/components/account-popup/account-popup.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { SidebarModule } from './core/components/sidebar/sidebar.module';
import { ErrorNotificationInterceptor } from './core/interceptors/error-notification.interceptor';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { SuccessNotificationInterceptor } from './core/interceptors/success-notification.interceptor';
import { ServiceLocator } from './core/service-locator';
import { MainLayoutComponent } from './layouts/main/main-layout.component';
import { UnauthorizedLayoutComponent } from './layouts/unauthorized/unauthorized-layout.component';

function initApp(initializer: AppInitializer) {
	return () => initializer.initialize();
}

@NgModule({
	declarations: [AppComponent, NavbarComponent, MainLayoutComponent, UnauthorizedLayoutComponent, AccountPopupComponent],
	imports: [BrowserModule, BrowserAnimationsModule, AppRoutingModule, TranslateModule.forRoot(), HttpClientModule, SidebarModule],
	providers: [
		AppInitializer,
		{
			provide: APP_INITIALIZER,
			useFactory: initApp,
			deps: [AppInitializer],
			multi: true,
		},
		{ provide: HTTP_INTERCEPTORS, useClass: SuccessNotificationInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: ErrorNotificationInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
	],
	bootstrap: [AppComponent],
})
export class AppModule {
	constructor(private injector: Injector) {
		ServiceLocator.injector = injector;
	}
}

registerLocaleData(localeEn);
registerLocaleData(localeLt);
