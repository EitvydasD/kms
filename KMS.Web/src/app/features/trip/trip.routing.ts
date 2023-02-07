import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OnlyAuthorizedGuard } from '../../core/guards/only-authorized.guard';
import { TripListComponent } from './trip-list/trip-list.component';
import { TripShowComponent } from './trip-show/trip-show.component';

const routes: Routes = [
	{
		path: '',
		component: TripListComponent,
		canActivate: [OnlyAuthorizedGuard],
	},
	{
		path: ':id',
		component: TripShowComponent,
		canActivate: [OnlyAuthorizedGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class TripRoutingModule {}
