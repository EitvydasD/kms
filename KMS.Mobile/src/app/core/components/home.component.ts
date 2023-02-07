import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'home-component',
	template: '',
})
export class HomeComponent implements OnInit {
	constructor(private router: Router) {
		// Nothing
	}

	public ngOnInit(): void {
		this.router.navigate(['/profile']);
	}
}
