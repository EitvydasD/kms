import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ButtonModule } from '@progress/kendo-angular-buttons';
import { DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { GridModule } from '@progress/kendo-angular-grid';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';

import { DateOnlyPickerModule } from '../../core/components/date-only-picker/date-only-picker.module';
import { UserListComponent } from './user-list/user-list.component';
import { UserShowComponent } from './user-show/user-show.component';
import { UserRoutingModule } from './user.routing';

@NgModule({
	declarations: [UserListComponent, UserShowComponent],
	imports: [
		CommonModule,
		UserRoutingModule,
		TranslateModule,
		FormsModule,
		ReactiveFormsModule,
		InputsModule,
		GridModule,
		LabelModule,
		DropDownsModule,
		DatePickerModule,
		ButtonModule,
		DateOnlyPickerModule,
	],
	exports: [UserShowComponent],
})
export class UserModule {}
