import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { DateOnlyPickerComponent } from './date-only-picker.component';

@NgModule({
	declarations: [DateOnlyPickerComponent],
	imports: [CommonModule, DatePickerModule],
	exports: [DateOnlyPickerComponent],
})
export class DateOnlyPickerModule {}
