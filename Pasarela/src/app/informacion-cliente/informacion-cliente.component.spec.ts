import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InformacionClienteComponent } from './informacion-cliente.component';

describe('InformacionClienteComponent', () => {
  let component: InformacionClienteComponent;
  let fixture: ComponentFixture<InformacionClienteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InformacionClienteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InformacionClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
