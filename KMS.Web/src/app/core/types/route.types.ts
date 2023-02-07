import { PermissionId } from '../enums/permission.enums';

export type RouteInfo = {
	path: string;
	title: string;
	icon: string;
	children?: RouteInfo[];
	expanded?: boolean;
	isRoot?: boolean;
	isVisible?: boolean;
	anyPermissionRequired: PermissionId[];
};
