import { PermissionId } from '../enums/permission.enums';

declare type MenuType = 'sub' | 'link';

export type RouteInfo = {
	isVisible?: boolean;
	anyPermissionRequired: PermissionId[];
	path: string;
	title: string;
	type: MenuType;
	icon: string;
	collapse?: string;
	isCollapsed?: boolean;
	children?: RouteInfo[];
};
