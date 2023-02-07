import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OnlyAuthorizedGuard } from './core/guards/only-authorized.guard';
import { MainLayoutComponent } from './layouts/main/main-layout.component';
import { UnauthorizedLayoutComponent } from './layouts/unauthorized/unauthorized-layout.component';

const routes: Routes = [
	{
		path: '',
		component: MainLayoutComponent,
		children: [
			{
				path: 'profile',
				loadChildren: () => import('./features/profile/profile.module').then((x) => x.ProfileModule),
				canLoad: [OnlyAuthorizedGuard],
			},
			{
				path: 'forum',
				loadChildren: () => import('./features/forum/forum.module').then((x) => x.ForumModule),
				canLoad: [OnlyAuthorizedGuard],
			},
			{
				path: 'trips',
				loadChildren: () => import('./features/trip/trip.module').then((x) => x.TripModule),
				canLoad: [OnlyAuthorizedGuard],
			},
		],
	},
	{
		path: '',
		component: UnauthorizedLayoutComponent,
		loadChildren: () => import('./features/auth/auth.module').then((x) => x.AuthModule),
	},
	{
		path: '**',
		redirectTo: '/profile',
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
