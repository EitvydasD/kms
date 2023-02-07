import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment, PostCommentRequest } from '../../types/comment.types';

@Injectable({
	providedIn: 'root',
})
export class CommentService {
	constructor(private http: HttpClient) {
		// Nothing
	}

	public getComments(parentId?: string): Observable<Comment[]> {
		if (parentId) {
			return this.http.get<Comment[]>(`${this.getEndpoint()}/parentId/${parentId}`);
		}
		return this.http.get<Comment[]>(this.getEndpoint());
	}

	public postComment(request: PostCommentRequest): Observable<Comment> {
		return this.http.post<Comment>(this.getEndpoint(), request);
	}

	private getEndpoint(commentId?: string) {
		if (commentId) {
			return `api/comment/${commentId}`;
		}
		return 'api/comment';
	}
}
