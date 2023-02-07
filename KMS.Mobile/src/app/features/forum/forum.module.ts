import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';

import { ForumListComponent } from './forum-list/forum-list.component';
import { ForumRoutingModule } from './forum.routing';

@NgModule({
	declarations: [ForumListComponent],
	imports: [CommonModule, ForumRoutingModule, TranslateModule, LabelModule, InputsModule, ReactiveFormsModule, ButtonsModule],
})
export class ForumModule {}
