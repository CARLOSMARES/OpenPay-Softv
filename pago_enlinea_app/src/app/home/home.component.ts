import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isOnline: boolean=false;

  username: string = "";
  password: string = "";
  loginfrm: any = true;
  avisofrm: any = false;
  error:any=false;
  messageError:string="";
  prueba: any;

  constructor( private cookies: CookieService) {

    this.isOnline = false;

  }

  ngOnInit(): void {

    this.prueba = "Hola Mundo";

    this.updateOnlineStatus();

    window.addEventListener('online', this.updateOnlineStatus.bind(this));

    window.addEventListener('offline', this.updateOnlineStatus.bind(this));
  }

  private updateOnlineStatus(): void {

    this.isOnline = window.navigator.onLine;

    console.info(`isOnline=[${this.isOnline}]`);

  }

  initial(): void {

    this.loginfrm = true;

    this.avisofrm = false;

    this.username = "";

    this.password = "";

  }

  async login() {

    if ((this.username === "" && this.password === "") || (this.username === "" || this.password === "")) {
      /*const alert = await this.alertController.create({
        header: 'Alert',
        subHeader: 'Mensaje Importante',
        message: 'Usuario y/o Contraseña Vacias',
        buttons: ['OK'],
      });
      await alert.present();*/
    }
    else {

      console.clear()


      if (this.username.length != 0) {
        if (this.password.length != 0) {

          let token: any = btoa(`${this.username}:${this.password}`);
          //console.log(token)
          let parametros: any = {
            "@Contrato": this.username
          };
          let config = {
            headers: {
              'Authorization': `Basic ${token}`
            }
          }
          axios.post(environment.server + "/Usuario/LogOn", JSON.stringify(parametros), config)
            .then((response) => {
              if (response.data.LogOnResult.Token) {
                //console.log('factory.login', response);
                this.cookies.set("token", token);
                localStorage.setItem("CurrentUser", JSON.stringify({
                  //token: response.data.LogOnResult.Token,
                  nombre: response.data.LogOnResult.Nombre,
                  idRol: response.data.LogOnResult.IdRol,
                  idUsuario: response.data.LogOnResult.IdUsuario,
                  contrato: response.data.LogOnResult.Contrato,
                  login: this.username,
                  menu: response.data.LogOnResult.Menu,
                  cliente: response.data.LogOnResult.Login,
                  idTransaction: null
                }))
                const bodyParameters = {
                  Contrato: response.data.LogOnResult.Contrato
                };
                axios.post(environment.server + "/DameClv_Session/GetDeepDameClv_Session", bodyParameters, config)
                  .then((response) => {
                    //clv_session: response.data.GetDeepDameClv_SessionResult.IdSession;
                    if(response.data.GetDeepDameClv_SessionResult.Clv_Session != 0)
                    {
                      localStorage.setItem("currentPay", JSON.stringify(
                        {
                          userLogueado: 0,
                          userModal: 0,
                          idSession: null,
                          //clvSessionCobra: response.data,
                          descripcion: null,
                          logeoAutomatico: 0,
                          clv_session: response.data.GetDeepDameClv_SessionResult.Clv_Session
                        }
                      ))
                      this.loginfrm = false
                      this.avisofrm = true
                    }
                    else{
                      this.error = true;
                      var Mensaje = response.data.GetDeepDameClv_SessionResult.Msg;
                      this.messageError = Mensaje;
                    }
                  })
              }
            }
            )
            .catch(async (error) => {
              console.log(error)
              this.error=true;
              this.messageError="Usuario y Contraseña Incorrecta";
              /*if (error.response) {
                const alert = await this.alertController.create({
                  header: 'Alert',
                  subHeader: 'Mensaje Importante',
                  message: 'Usuario y Contraseña Incorrecta',
                  buttons: ['OK'],
                });
                await alert.present();
              }*/
            });
        }

      }
    }
  }

  onKey(event:any){
    this.error=false;
  }
}
