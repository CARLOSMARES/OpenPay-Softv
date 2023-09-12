import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent implements OnInit {

  nueva: string = "";
  nuevarepetida: string = "";
  messageError: any;
  token: any;
  currentUser: any;
  contrato: any;
  error: any;
  correcto: any;

  constructor(private cookies: CookieService, private router: Router) {
    this.token = this.cookies.get('token');
    this.currentUser = window.localStorage.getItem('CurrentUser');
    this.currentUser = JSON.parse(this.currentUser);
    this.contrato = this.currentUser.contrato;
  }

  ngOnInit(): void {

  }

  SetUpdateClave() {
    console.clear();
    if (this.nueva === this.nuevarepetida) {
      try {
        let config = {
          headers: {
            Authorization: `Basic ${this.token}`,
          },
        };

        let jsonbody = {
          Contrato: this.contrato,
          Nueva: this.nueva,
        };

        axios
          .post(
            environment.server + '/Ecom_PagoEnLinea/fnChangePassword',
            jsonbody,
            config
          )
          .then((Respuesta) => {
            if (Respuesta.data.fnChangePasswordResult === 1) {
              this.correcto = true;

              this.cookies.deleteAll;
              window.localStorage.clear;

              this.router.navigate(['home']);
              if (
                (localStorage.length === 1 && sessionStorage.length === 0) ||
                (localStorage.length === 0 && sessionStorage.length === 0)
              ) {
                this.router.navigate(['home']);
              }
            }
          });
      } catch (err: any) {
        this.error = true;
        this.messageError = err.Message;
      }
    } else {
      this.error = true;
      this.messageError = 'Las contraseña no coinciden';
    }
  }
}
