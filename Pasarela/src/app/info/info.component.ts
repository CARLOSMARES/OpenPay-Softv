import { Component } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent {
  showText: boolean = false;
  toggleText() {
    this.showText = !this.showText;
  }
}

