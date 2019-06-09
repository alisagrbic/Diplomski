import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesReserveComponent } from './vehicles-reserve.component';

describe('VehiclesReserveComponent', () => {
  let component: VehiclesReserveComponent;
  let fixture: ComponentFixture<VehiclesReserveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VehiclesReserveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesReserveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
