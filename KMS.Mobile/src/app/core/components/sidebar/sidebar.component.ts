import { Component, OnInit } from '@angular/core';
import { zip } from 'rxjs';
import { Routes } from '../../constants/route.constants';
import { AccountService } from '../../services/account.service';
import { RouteInfo } from '../../types/route.types';
import { User } from '../../types/user.types';

@Component({
	selector: 'sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
	public routes: RouteInfo[] = [];

	constructor(private accountService: AccountService) {
		// Nothing
	}

	public ngOnInit(): void {
		zip([this.accountService.currentPermissions, this.accountService.currentUser]).subscribe({
			next: (response) => {
				const [permissions, user] = response;

				if (!user) {
					return;
				}

				this.routes = Routes.map((route) => {
					this.updateIsVisible(route, permissions, user);
					return route;
				});
				this.routes = Routes.filter((menuItem) => menuItem.isVisible);
			},
		});
	}

	public updateIsVisible(route: RouteInfo, permissions: string[], user: User): void {
		route.isVisible = this.isVisible(route, permissions);
		if (route.children) {
			route.children.forEach((child) => {
				this.updateIsVisible(child, permissions, user);
				route.children = route.children?.filter((rc) => rc.isVisible);
			});
		}
	}

	public isVisible(route: RouteInfo, permissions: string[]): boolean {
		return (
			route.anyPermissionRequired.length === 0 || route.anyPermissionRequired.some((permission) => permissions.includes(permission))
		);
	}

	public toggleSidebar = (): void => {
		const layoutDiv = <HTMLElement>document.getElementById('MainLayout');

		layoutDiv.classList.toggle('g-sidenav-pinned');
		layoutDiv.classList.toggle('g-sidenav-hidden');
	};
}
