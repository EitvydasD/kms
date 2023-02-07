import { FormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../validators/custom.validators';
import { DateOnly } from './general.types';
import { Role } from './role.types';

export type User = {
	id: string;
	username: string;
	firstName: string;
	lastName: string;
	email: string;
	roles: Role[];
	phone: string;
	birthDate: DateOnly;
};

export type GetUserRequest = {
	isDriver?: boolean;
};

export type UserUpdateRequest = {
	firstName: string;
	lastName: string;
	email: string;
	phone: string;
	roles: Role[];
	birthDate: DateOnly;
};

export type ChangePasswordRequest = {
	oldPassword: string;
	newPassword: string;
};

export class UserHelpers {
	public static getFullName(user: User): string {
		return `${user.firstName} ${user.lastName}`;
	}

	public static createForm(user: User, isProfile: boolean): UntypedFormGroup {
		return new UntypedFormGroup({
			id: new FormControl<string>({ value: user.id, disabled: true }, [Validators.required]),
			firstName: new FormControl<string>({ value: user.firstName, disabled: true }, [Validators.required]),
			lastName: new FormControl<string>({ value: user.lastName, disabled: true }, [Validators.required]),
			email: new FormControl<string>({ value: user.email, disabled: !isProfile }, [Validators.required, Validators.email]),
			phone: new FormControl<string>({ value: user.phone, disabled: !isProfile }, [Validators.required, CustomValidators.phone]),
			birthDate: new FormControl<string>({ value: user.birthDate, disabled: !isProfile }, [Validators.required]),
			roles: new FormControl<Role[]>({ value: user.roles, disabled: true }, [Validators.required]),
		});
	}
}
