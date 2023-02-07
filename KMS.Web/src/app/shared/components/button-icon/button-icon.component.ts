import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
	selector: 'button-icon',
	templateUrl: './button-icon.component.html',
})
export class ButtonIconComponent {
	@Input()
	public title?: string;

	@Input()
	public iconStyle: string = 'fa-solid';

	@Input()
	public icon?: string;

	@Input()
	public background: string = 'bg-gradient-primary';

	@Input()
	public disabled: boolean = false;

	@Input()
	public cssClass?: string;

	@Output()
	// eslint-disable-next-line @angular-eslint/no-output-on-prefix
	public onClick: EventEmitter<void> = new EventEmitter<void>();
}
