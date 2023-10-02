import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router) { }

  disBtn = false;
  login(){
    this.disBtn = !this.disBtn
    this.router.navigate(['/info']);
  }
}
