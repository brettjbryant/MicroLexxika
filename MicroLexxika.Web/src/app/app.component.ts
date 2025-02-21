import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavComponent } from "./nav/nav.component";
import { RouterModule } from '@angular/router';
import { TITLE } from "../shared/constants";

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterModule, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [],
})

export class AppComponent {
  title = TITLE;
}
