import { ValidatorFn, Validators } from '@angular/forms';

export class CustomValidators {
	public static get phone(): ValidatorFn {
		return Validators.pattern('^\\+[0-9]{10,12}$');
	}
}
