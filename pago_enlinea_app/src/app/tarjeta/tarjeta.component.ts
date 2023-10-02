import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import axios from 'axios';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import {Location, LocationStrategy} from "@angular/common";

@Component({
  selector: 'app-tarjeta',
  templateUrl: './tarjeta.component.html',
  styleUrls: ['./tarjeta.component.css']
})
export class TarjetaComponent implements OnInit {

  token: any=this.cookies.get("token");
  clv_session: any;
  currentpay: any;
  idprosa: any;
  currentuser: any;
  contrato: any;
  amount: any;
  url: any;

  constructor(private cookies: CookieService, private router: Router, location: LocationStrategy) {
    //Primera Validacion para Edge PC
    //Segunda Validacion para Chrome y para Safari PC

   location.onPopState(() => {
    alert("Usted será redirigido al inicio.");
    window.location.reload();
  });
    if ((localStorage.length === 1 && sessionStorage.length === 0) || (localStorage.length === 0 && sessionStorage.length === 0)) {

      router.navigate(["/home"]);

    } else {
      this.currentuser = window.localStorage.getItem("CurrentUser");
      this.currentuser = JSON.parse(this.currentuser);
      this.contrato = this.currentuser.contrato;
      this.token = this.cookies.get("token");
      this.currentpay = window.localStorage.getItem("currentPay");
      this.clv_session = JSON.parse(this.currentpay);
      this.clv_session = this.clv_session.clv_session;

      this.amount = window.localStorage.getItem("monto");
      this.reedirigir();
    }
  }

  ngOnInit() {
    //Primera Validacion para Edge
    //Segunda Validacion para Chrome y para Safari
    history.pushState(null, document.title, location.href);
    if ((localStorage.length == 1 && sessionStorage.length == 0) || (localStorage.length == 0 && sessionStorage.length == 0)) {
      this.router.navigate(["home"]);
    }
  }

  reedirigir() {
    let config = {
      headers: {
        'Authorization': `Basic: ${this.token}`
      }
    }

    let data = {
      Clv_Session: this.clv_session,
      Contrato: this.contrato,
      Total: this.amount
    }
    localStorage.removeItem("currentuser");
    axios.post(environment.server + "/Ecom_PagoEnLinea/GetGeneraDatosPago", data, config)
      .then((gdp) => {
        localStorage.clear();
        localStorage.clear();
        this.cookies.deleteAll();
        this.cookies.delete("token");
        window.location.href = gdp.data.GetGeneraDatosPagoResult.URLRedireccion;
      });
  }

  recargar(){
    this.router.navigate(["home"]);
    if ((localStorage.length === 1 && sessionStorage.length === 0) || (localStorage.length === 0 && sessionStorage.length === 0)) {

      this.router.navigate(["home"]);

    }
  }


}
