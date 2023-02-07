import { Component, OnInit } from '@angular/core';
import { Routes } from '../../constants/route.constants';
import { AccountService } from '../../services/account.service';
import { RouteInfo } from '../../types/route.types';

@Component({
	selector: 'sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
	constructor(private accountService: AccountService) {
		// Nothing
	}

	public routes: RouteInfo[] = Routes;
	private permissions: string[] = [];

	public ngOnInit(): void {
		this.accountService.currentPermissions.subscribe({
			next: (permissions) => {
				this.permissions = permissions;

				this.routes = this.routes.map((route: RouteInfo) => {
					this.updateIsVisible(route);
					return route;
				});

				this.routes = this.routes.filter((route) => route.isVisible);
			},
		});
	}

	public updateIsVisible(route: RouteInfo): void {
		route.isVisible = this.isVisible(route);
		if (route.children) {
			route.children.forEach((child) => {
				child.isVisible = this.isVisible(child);
				route.children = route.children?.filter((rc) => rc.isVisible);
			});
		}
	}

	public isVisible(route: RouteInfo): boolean {
		return (
			route.anyPermissionRequired.length === 0 ||
			route.anyPermissionRequired.some((permission) => this.permissions.includes(permission))
		);
	}
}
