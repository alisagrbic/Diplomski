import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeVehicleComponent } from './make-vehicle.component';

describe('MakeVehicleComponent', () => {
  let component: MakeVehicleComponent;
  let fixture: ComponentFixture<MakeVehicleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MakeVehicleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MakeVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
