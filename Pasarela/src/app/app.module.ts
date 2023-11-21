import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { InformacionClienteComponent } from './informacion-cliente/informacion-cliente.component';
import { TarjetaComponent } from './tarjeta/tarjeta.component';
import { PoliticaPrivacidadComponent } from './politica-privacidad/politica-privacidad.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { TerminosycondicionesComponent } from './terminosycondiciones/terminosycondiciones.component';
import { ChangePasswordComponent } from './change-password/change-password.component'
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    InformacionClienteComponent,
    TarjetaComponent,
    PoliticaPrivacidadComponent,
    TerminosycondicionesComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgxExtendedPdfViewerModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
