import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { InfoComponent } from './info/info.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'info', component: InfoComponent },
  // Agrega más rutas según sea necesario
  
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redirige a /login si la URL está vacía
  { path: '**', redirectTo: '/login' } // Redirige a /login si la ruta no coincide con ninguna definida
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
