import { Component, AfterViewInit, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Doc } from '../../shared/models/doc.model';
import { AuthService } from '../../shared/services/auth.service';
import { DocService } from '../../shared/services/doc.service';

@Component({
  selector: 'app-doc',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doc.component.html',
  styleUrl: './doc.component.css'
})

export class DocComponent implements OnInit, AfterViewInit {
  doc: Doc | undefined;

  docForm = new FormGroup({
    title: new FormControl({ value: '', disabled: true }, Validators.required),
    body: new FormControl({ value: '', disabled: true }, Validators.required)
  });

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private docService: DocService, private authService: AuthService) {
    const parameter = this.activatedRoute.snapshot.params['id'];

    if (parameter !== 'new') {
      this.doc = this.docService.getDocById(Number(parameter))!;
    }
    else {
      this.doc = { id: undefined, userId: this.authService.getUserId()!, title: '', body: '' };
    }
  }

  ngOnInit(): void {
    this.docForm.patchValue({
      title: this.doc!.title,
      body: this.doc!.body
    });
  }

  ngAfterViewInit() {
    if (this.doc !== undefined && this.doc.id === undefined) {
      this.toggleEditing();
    }
  }

  upsertDoc() {
    const id = this.doc!.id;
    const userId = this.doc!.userId;
    const title = this.docForm.value.title ?? '';
    const body = this.docForm.value.body ?? '';

    if (id === undefined) {
      this.docService.createDoc(userId, title, body);
    }
    else {
      this.docService.updateDoc({ id, userId, title, body });
    }
    this.toggleEditing();
  }

  closeDoc() {
    this.router.navigateByUrl('/');
  }

  toggleEditing() {
    const container = document.getElementById('docContainer')!;
    const editing = container?.classList.contains('editmode');

    if (editing) {
      container.classList.remove('editmode');

      this.docForm.controls['title'].disable();
      this.docForm.controls['body'].disable();
    }
    else {
      container.classList.add('editmode');

      this.docForm.controls['title'].enable();
      this.docForm.controls['body'].enable();
    }
  }
}
