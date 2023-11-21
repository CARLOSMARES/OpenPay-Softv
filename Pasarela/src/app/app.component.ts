import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  isOnline: boolean=false;

  constructor(private cookies: CookieService, private router:Router) {

    this.isOnline = false;

    window.onbeforeunload = this.logout;

    

  }
  logout():void {

    //alert("Desea Salir");

    sessionStorage.clear();

    localStorage.clear();
    if(this.cookies!=null){
      this.cookies.deleteAll();
      window.close();
 
    }    
  }

  ngOnInit(): void {
 
    this.updateOnlineStatus();

    window.addEventListener('online', this.updateOnlineStatus.bind(this));

    window.addEventListener('offline', this.updateOnlineStatus.bind(this));
    
    if(localStorage == null )
    {
      window.location.reload()
    }

  }

  private updateOnlineStatus(): void {

    this.isOnline = window.navigator.onLine;

    console.info(`isOnline=[${this.isOnline}]`);
    //window.location.reload()

  }
}
