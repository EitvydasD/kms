import { PermissionId } from '../enums/permission.enums';
import { RouteInfo } from '../types/route.types';

export const Routes: RouteInfo[] = [
	{
		path: '/forum',
		title: 'menu.forum',
		icon: 'fa-regular fa-comments',
		type: 'link',
		anyPermissionRequired: [PermissionId.CommentView],
	},
	{
		path: '/trips',
		title: 'menu.trips',
		icon: 'fa-solid fa-location-dot',
		type: 'link',
		anyPermissionRequired: [PermissionId.TripView],
	},
	{
		path: '/profile',
		title: 'menu.profile',
		icon: 'fa-regular fa-user',
		type: 'link',
		anyPermissionRequired: [],
	},
];
