import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../core/services/account.service';
import { CommentService } from '../../../core/services/api/comment.service';
import { Comment, PostCommentRequest } from '../../../core/types/comment.types';
import { User } from '../../../core/types/user.types';

@Component({
	selector: 'forum-list',
	templateUrl: './forum-list.component.html',
})
export class ForumListComponent implements OnInit {
	constructor(private commentService: CommentService, private accountSerivce: AccountService) {
		// Nothing
	}

	public comments: Comment[] = [];
	public currentUser?: User;
	public parentComment?: Comment;

	public form: FormGroup = this.getForm();

	public ngOnInit(): void {
		this.accountSerivce.currentUser.subscribe({
			next: (user) => {
				this.currentUser = user ?? undefined;

				this.getComments();
			},
		});
	}

	private getComments(): void {
		this.commentService.getComments(this.parentComment?.id).subscribe({
			next: (comments) => {
				this.comments = comments;
			},
		});
	}

	public postComment(text?: string): void {
		const formValue = this.form.getRawValue() as Comment;
		const request: PostCommentRequest = {
			title: text ? undefined : formValue.title,
			text: text ?? formValue.text,
			parentId: this.parentComment?.id,
			authorId: this.currentUser?.id ?? '',
		};
		this.commentService.postComment(request).subscribe({
			next: () => {
				this.getComments();
				this.form = this.getForm();
			},
		});
	}

	public viewComment(comment: Comment): void {
		this.parentComment = comment;
		this.form = this.getForm();
		this.getComments();
	}

	private getForm(): FormGroup {
		return new FormGroup({
			title: new FormControl<string | undefined>(
				{ value: this.parentComment ? undefined : '', disabled: this.parentComment !== undefined },
				this.parentComment ? [] : [Validators.required],
			),
			text: new FormControl<string>('', [Validators.required]),
		});
	}
}
