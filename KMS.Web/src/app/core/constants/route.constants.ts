import { PermissionId } from '../enums/permission.enums';
import { RouteInfo } from '../types/route.types';

export const Routes: RouteInfo[] = [
	{
		path: '/users',
		title: 'menu.users',
		icon: 'fa-solid fa-users',
		anyPermissionRequired: [PermissionId.UserView],
	},
	{
		path: '/forum',
		title: 'menu.forum',
		icon: 'fa-regular fa-comments',
		anyPermissionRequired: [PermissionId.CommentView],
	},
	{
		path: '/trips',
		title: 'menu.trips',
		icon: 'fa-solid fa-location-dot',
		anyPermissionRequired: [PermissionId.TripView],
	},
	{
		path: '/profile',
		title: 'menu.profile',
		icon: 'fa-regular fa-user',
		anyPermissionRequired: [],
	},
];
