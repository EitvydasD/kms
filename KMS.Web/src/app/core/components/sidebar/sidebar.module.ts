import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { SidebarItemComponent } from './sidebar-item/sidebar-item.component';
import { SidebarComponent } from './sidebar.component';

@NgModule({
	declarations: [SidebarComponent, SidebarItemComponent],
	imports: [CommonModule, RouterModule, TranslateModule],
	exports: [SidebarComponent],
})
export class SidebarModule {}
