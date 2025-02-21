import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DocComponent } from './doc/doc.component';
import { authGuard } from '../shared/auth.guard';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'doc/:id',
    component: DocComponent,
    canActivate: [authGuard]
  },
  {
    path: 'doc/new',
    component: DocComponent,
    canActivate: [authGuard]
  }
];
