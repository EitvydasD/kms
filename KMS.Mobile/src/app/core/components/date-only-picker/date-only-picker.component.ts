import { Component } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { DateOnly } from '../../types/general.types';
import { toDateOnly } from '../../utils/utils';

export type RegisterOnChangeFn = (_: unknown) => void;
export type RegisterOnTouchFn = () => void;

@Component({
	selector: 'date-only-picker',
	templateUrl: './date-only-picker.component.html',
})
export class DateOnlyPickerComponent implements ControlValueAccessor {
	constructor(public control: NgControl) {
		if (this.control !== null) {
			this.control.valueAccessor = this;
		}
	}

	public onChange: RegisterOnChangeFn = () => {
		// Nothing
	};

	public onTouch: RegisterOnTouchFn = () => {
		// Nothing
	};

	public disabled: boolean = false;

	public field: unknown;

	public get value(): Date {
		return this.field as Date;
	}

	// sets the value used by the ngModel of the element
	public set value(val: Date | DateOnly | null | undefined) {
		this.field = val ? new Date(val) : null;
		this.onChange(this.field === null ? null : toDateOnly(val));
		this.onTouch();
	}

	public writeValue(obj: Date | DateOnly | null | undefined): void {
		this.value = obj;
	}

	public registerOnChange(fn: RegisterOnChangeFn): void {
		this.onChange = fn;
	}

	public registerOnTouched(fn: RegisterOnTouchFn): void {
		this.onTouch = fn;
	}

	public setDisabledState(isDisabled: boolean): void {
		this.disabled = isDisabled;
	}

	public updateValue(date: Date | null): void {
		this.value = date;
	}

	public onFocus(): void {
		this.onTouch();
	}
}
