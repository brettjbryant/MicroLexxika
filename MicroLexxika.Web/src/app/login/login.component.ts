import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  @Input() exitModal = (): void => { };
  public displayError = false;
  public errorMessage = "";

  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  constructor(private router: Router, private authService: AuthService) { }

  login() {
    this.errorMessage = this.getLoginErrorMessage(0);
    this.displayError = false;

    const credentials = this.loginForm.value;

    if (credentials.username && credentials.password) {
      this.authService.login(credentials.username, credentials.password)
        .subscribe({
          next: (response) => {
            if (response.status === 200) {
              this.success();
            }
            else {
              this.failed(response.status);
            }
          },
          error: (error: any) => {
            this.failed(error.status);
          },
        });
    }
  }

  success() {
    this.exitModal();

    this.router.routeReuseStrategy.shouldReuseRoute = () => { return false; };
    this.router.navigateByUrl('/', { onSameUrlNavigation: 'reload', skipLocationChange: true }).then(() => {
      this.router.navigate([this.router.url]);
    });
  }

  failed(statusCode: number) {
    this.errorMessage = this.getLoginErrorMessage(statusCode);
    this.displayError = true;
  }

  getLoginErrorMessage(statusCode: number) {
    switch (statusCode) {
      case 204:
        return "User not found";
      case 401:
        return "Incorrect Password";
      default:
        return "";
    }
  }
}
