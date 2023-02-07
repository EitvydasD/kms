import { User } from './user.types';

export type Comment = {
	id: string;
	title?: string;
	text: string;
	author: User;
	createdAt: Date;
};

export type PostCommentRequest = {
	title?: string;
	text: string;
	authorId: string;
	parentId?: string;
};
