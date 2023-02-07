export type LoginRequest = {
	username: string;
	password: string;
};

export type LoginResponse = {
	accessToken: string;
};

export type RegisterRequest = {
	username: string;
	firstName: string;
	lastName: string;
	email: string;
	phone: string;
	password: string;
};
