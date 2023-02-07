import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

import { GridModule } from '@progress/kendo-angular-grid';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { TripListComponent } from './trip-list/trip-list.component';
import { TripShowComponent } from './trip-show/trip-show.component';
import { TripRoutingModule } from './trip.routing';

@NgModule({
	declarations: [TripListComponent, TripShowComponent],
	imports: [
		CommonModule,
		TripRoutingModule,
		GridModule,
		LabelModule,
		InputsModule,
		FormsModule,
		TranslateModule,
		DropDownsModule,
		DatePickerModule,
		ReactiveFormsModule,
	],
})
export class TripModule {}
