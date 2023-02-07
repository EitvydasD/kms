import { PermissionId } from '../enums/permission.enums';

export class PermissionContext {
	constructor(private permissions: string[]) {
		this.permissions = permissions;
	}

	public getPermissions(): string[] {
		return this.permissions;
	}

	public hasPermission(permissionId: PermissionId): boolean {
		return this.permissions.findIndex((permission) => permission === PermissionId[permissionId]) !== -1;
	}
}
