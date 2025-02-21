import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doc } from '../models/doc.model';
import { AuthService } from './auth.service';
import { firstValueFrom } from 'rxjs';
import { APIURL } from '../constants';

@Injectable({
  providedIn: 'root'
})

export class DocService {
  protected docs: Doc[] = [];

  constructor(private http: HttpClient, private authService: AuthService) {
    if (this.authService.isLoggedIn()!) {
      let userId = this.authService.getUserId();

      if (userId !== undefined) {
        this.getDocsByUser(userId);
      }
    }
  }

  getDocById(docId: number): Doc | undefined {
    return this.docs.find(x => x.id === docId);
  }

  async getDocsByUser(userId: number) {
    let result = this.http.get<Doc[]>(`${APIURL}/docs/getbyuser/${userId}`, { headers: this.getHeaders() });

    this.docs = await firstValueFrom(result);

    return this.docs;
  }

  async createDoc(userId: number, title: string, body: string) {
    let result = this.http.post<Doc>(`${APIURL}/docs/create/`, { userId, title, body }, { headers: this.getHeaders() });
    let createdDoc = await firstValueFrom(result);

    this.docs.push(createdDoc);

    this.docs = Object.assign([], this.docs);
  }

  async updateDoc(doc: Doc) {
    let result = this.http.put<Doc>(`${APIURL}/docs/update/`, doc, { headers: this.getHeaders() });
    let updatedDoc = await firstValueFrom(result);
    let docIndex = this.docs.findIndex(x => x.id === updatedDoc.id);

    this.docs[docIndex] = updatedDoc;

    this.docs = Object.assign([], this.docs);
  }

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });
  }
}
