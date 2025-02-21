import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink } from '@angular/router';
import { Doc } from '../../shared/models/doc.model';
import { AuthService } from "../../shared/services/auth.service";
import { DocService } from "../../shared/services/doc.service";

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})

export class HomeComponent implements OnInit {
  loggedIn = false;
  docs: Doc[] = [];

  constructor(private authService: AuthService, public docService: DocService) { }

  ngOnInit(): void {
    let isUserLoggedIn = this.authService.isLoggedIn()!;

    if (isUserLoggedIn) {
      let userId = this.authService.getUserId();

      if (userId !== undefined) {
        this.getDocsList(userId);
      }
    }

    this.loggedIn = isUserLoggedIn;
  }

  async getDocsList(userId: number) {
    this.docs = await this.docService.getDocsByUser(userId);
  }
}
