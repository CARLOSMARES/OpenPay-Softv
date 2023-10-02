import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import * as JsBarcode from 'jsbarcode';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-informacion-cliente',
  templateUrl: './informacion-cliente.component.html',
  styleUrls: ['./informacion-cliente.component.css']
})

export class InformacionClienteComponent implements OnInit{

  //Agregue estos
  idprosa: any;
  amount: any;
  urlBarcode: any;

  logout() {
    localStorage.clear();
    localStorage.clear();
    this.cookies.deleteAll();
    this.cookies.delete("token");
    window.close();
  }

  mostrardetalle() {
    if (this.sindetalle === true) {
      this.sindetalle = false;
      this.detalle = true;
    } else if (this.sindetalle === false) {
      this.sindetalle = true;
      this.detalle = false;
    }
  }

  url: any;
  contrato: any;
  nombre: any;
  direccion: any;
  telefono: any;
  aparato: any;
  concepto: any;
  tvextra: any;
  cortesia: any;
  pagar: any;
  importe: any;
  Periodopi: any;
  Periodopf: any;
  puntosappo: any;
  puntoapa: any;
  puntoscombo: any;
  subtotal: any;
  totalpuntos: any;
  importetotal: any;
  storeModalOpen:any;
  clv_session: any;
  currentpay: any;
  currentuser: any;
  token: any;
  fecha: any;
  dl: any = [];
  sindetalle: any;
  detalle: any;
  arraySize: number = 0;
  array = [1];
  colonia: any;
  serviciocliente: any;

  constructor(private cookies: CookieService) {
    this.currentuser = window.localStorage.getItem("CurrentUser");
    if(this.currentuser==null)
    {
      window.location.href="/home"
    }
    this.urlBarcode = "";
    this.url = "https://documents.openpay.mx/docs/terminos-servicio.html?gad=1&gclid=CjwKCAjw-IWkBhBTEiwA2exyO9s1ofKpjNW5BmQFFan-eW59BIgTzxUU7ZA1H3jVkHlWpLs3BVFxihoC1ZgQAvD_BwE";

      this.currentuser = JSON.parse(this.currentuser);
      this.contrato = this.currentuser.contrato;
      this.nombre = this.currentuser.cliente;
      this.token = this.cookies.get("token");

      this.currentpay = window.localStorage.getItem("currentPay");
      this.clv_session = JSON.parse(this.currentpay);
      this.clv_session = this.clv_session.clv_session;

      this.sindetalle = true;
      this.detalle = false;

      this.obtenerdatospago();
  }

  //COdigo de barras
  barOk = false
  
  openStoreModal(){
    this.storeModalOpen = true;
    this.getBarcode();
  }
  closeStoreModal(){
    this.storeModalOpen = false;
  }
  getBarcode(){
    //Obtenemos datos de las cookies
    this.currentuser = window.localStorage.getItem("CurrentUser");
    this.currentuser = JSON.parse(this.currentuser);
    this.contrato = this.currentuser.contrato;
    this.token = this.cookies.get("token");
    this.currentpay = window.localStorage.getItem("currentPay");
    this.clv_session = JSON.parse(this.currentpay);
    this.clv_session = this.clv_session.clv_session;
    this.amount = window.localStorage.getItem("monto");
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
    axios.post(environment.server + "/Ecom_PagoEnLinea/GetGeneraDatosPagoStore", data, config).then((gdp) => {
      console.log("Si hubo respuesta")
      localStorage.clear();
      localStorage.clear();
      this.cookies.deleteAll();
      this.cookies.delete("token");
      console.clear()
      this.urlBarcode = gdp.data.GetGeneraDatosPagoStoreResult.URLRedireccion;
      window.location.href = this.urlBarcode;
    });
  }

  ngOnInit() {
    // if(this.clv_session != null)
    // {
    //   this.obtenerdatospago();
    // }
    // else{
    //   window.location.href = "/#/home/"
    // }
  }

  obtenerdatospago() {
    let config = {
      headers: {
        'Authorization': `Basic ${this.token}`
      }
    }

    const bodyParameters = {
      Clv_Session: this.clv_session
    };

    const contratos = {
      Contrato: this.contrato
    };

    axios.post(environment.server + "/Ecom_PagoEnLinea/GetDameServicioCliente", contratos, config)
    .then((ServicioList)=>{
      this.serviciocliente = ServicioList.data.GetDameServicioClienteResult;
      console.log(this.serviciocliente)
      console.log("redirect antes....");
      if ((localStorage.length == 1 && sessionStorage.length == 0) || (localStorage.length == 0 && sessionStorage.length == 0)) {
        //document.location = "/#/home/"
        console.log("redirect....");
        window.location.href="/home"
        //console.log("entra",router)

      }else{
        console.log("localstorage: ",localStorage)
        console.log("localstorage: ",localStorage.length)
        console.log("sessionStorage: ",sessionStorage)
        console.log("sessionStorage: ",sessionStorage.length)
        console.log("redirect else....");
      }
      this.serviciocliente.forEach((element: { CLV_TipSer: number; tv: any; wifi: any; tel: any;}) => {
        if(element.CLV_TipSer === 1){
          element.tv = true;
        }
        if(element.CLV_TipSer === 2){
          element.wifi = true;
        }
        if(element.CLV_TipSer === 3){
          element.tel = true;
        }
      });
    })
    axios.post(environment.server + "/Ecom_PagoEnLinea/GetDameDetalleList", bodyParameters, config)
      .then((DetalleList) => {
        this.dl = DetalleList.data.GetDameDetalleListResult;
        axios.post(environment.server + "/Ecom_PagoEnLinea/GetImporteTotal", bodyParameters, config)
          .then((ImporteTotal) => {
            axios.post(environment.server + "/Ecom_PagoEnLinea/GetSumaDetalleList", bodyParameters, config)
              .then((sumadetalle) => {
                let smd = sumadetalle.data.GetSumaDetalleListResult;
                this.subtotal = "$ " + smd[0].Total + ".00";
                this.totalpuntos = "$ " + smd[1].Total + ".00";
                this.importetotal = "$ " + smd[2].Total + ".00";
                localStorage.setItem("monto", smd[2].Total);

                const contrato = {
                  Contrato: this.contrato
                }
                axios.post(environment.server + "/Usuario/GetDatosCliente", contrato, config)
                  .then((cliente) => {
                    let clientedatos = cliente.data.GetDatosClienteResult;
                    this.nombre = clientedatos.Nombre;
                    this.contrato = clientedatos.ContratoCompuesto;
                    this.direccion = clientedatos.Direccion;
                    this.telefono = clientedatos.Telefono;
                    this.colonia = clientedatos.Colonia;
                  })
                var d = new Date();
                var mont = '' + (d.getMonth() + 1);
                var day = '' + d.getDate();
                var year = d.getFullYear();

                if(mont.length < 2){
                  mont = '0' + mont;
                }
                if(day.length < 2){
                  day = '0' + day;
                }

                this.fecha = day + "/" + mont + "/" + year;

              })
          })
      })

  }
}
