import { CommonModule } from '@angular/common';
import { Component, TemplateRef } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { LoginComponent } from "../login/login.component";
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-nav',
  imports: [CommonModule, LoginComponent, RouterLink],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})

export class NavComponent {
  loginModal?: BsModalRef;

  constructor(private authService: AuthService, private modalService: BsModalService, private router: Router) { }

  openModal(template: TemplateRef<any>) {
    this.loginModal = this.modalService.show(template, { animated: false });
  }

  exitModal = (): void => {
    this.loginModal?.hide();
  };

  public logout() {
    this.authService.logout();

    this.router.routeReuseStrategy.shouldReuseRoute = () => { return false; };
    this.router.navigateByUrl('/', { onSameUrlNavigation: 'reload', skipLocationChange: true }).then(() => {
      this.router.navigate([this.router.url]);
    });
  }

  public isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}
