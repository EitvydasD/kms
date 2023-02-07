import { Component, Input } from '@angular/core';
import { RouteInfo } from '../../../types/route.types';

@Component({
	selector: 'sidebar-item',
	templateUrl: './sidebar-item.component.html',
	styleUrls: ['./sidebar-item.component.scss'],
})
export class SidebarItemComponent {
	@Input()
	public route?: RouteInfo;

	@Input()
	public path: string = '';
}
