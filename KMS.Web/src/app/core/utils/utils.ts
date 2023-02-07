import { formatDate } from '@progress/kendo-angular-intl';
// eslint-disable-next-line import/order
import { Buffer } from 'buffer';
import { DateOnly } from '../types/general.types';

export function encodeBase64(data: string): string {
	return Buffer.from(data).toString('base64');
}
export function decodeBase64(data: string): string {
	return Buffer.from(data, 'base64').toString('ascii');
}

export function toDateOnly(date: Date | DateOnly | null | undefined): string | null {
	if (date === null) {
		return null;
	}

	return formatDate(date, 'yyyy-MM-dd') as string;
}
