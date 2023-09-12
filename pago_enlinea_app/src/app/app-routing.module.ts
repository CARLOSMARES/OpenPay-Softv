import { Component, NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { InformacionClienteComponent } from './informacion-cliente/informacion-cliente.component';
import { TarjetaComponent } from './tarjeta/tarjeta.component';
import { PoliticaPrivacidadComponent } from './politica-privacidad/politica-privacidad.component';
import { TerminosycondicionesComponent } from './terminosycondiciones/terminosycondiciones.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
  { path: 'home', component:HomeComponent },
  { path: 'informacioncliente', component:InformacionClienteComponent},
  { path: 'tarjeta', component:TarjetaComponent},
  { path: 'politica', component: PoliticaPrivacidadComponent},
  { path: 'terminosycondiciones', component: TerminosycondicionesComponent},
  { path: 'changepassword', component: ChangePasswordComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
