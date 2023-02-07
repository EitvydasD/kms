import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { SidebarComponent } from './sidebar.component';

@NgModule({
	declarations: [SidebarComponent],
	imports: [CommonModule, RouterModule, TranslateModule],
	exports: [SidebarComponent],
})
export class SidebarModule {}
