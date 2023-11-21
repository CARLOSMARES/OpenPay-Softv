import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-politica-privacidad',
  templateUrl: './politica-privacidad.component.html',
  styleUrls: ['./politica-privacidad.component.css']
})
export class PoliticaPrivacidadComponent{

  constructor(private cookies: CookieService, private router: Router){}

  pdfSource = "../../assets/politica/AVISO_DE_PRIVACIDAD_2023.pdf";

  logout() {

    localStorage.clear();

    localStorage.clear();

    this.cookies.deleteAll();

    this.cookies.delete("token");

    window.close();

  }

}
