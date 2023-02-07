import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OnlyAuthorizedGuard } from '../../core/guards/only-authorized.guard';
import { OnlyUnauthorizedGuard } from '../../core/guards/only-unauthorized.guard';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
	{
		path: 'login',
		component: LoginComponent,
		canActivate: [OnlyUnauthorizedGuard],
	},
	{
		path: 'register',
		component: RegisterComponent,
		canActivate: [OnlyUnauthorizedGuard],
	},
	{
		path: 'logout',
		component: LogoutComponent,
		canActivate: [OnlyAuthorizedGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AuthRouting {}
