import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeServiceComponent } from './make-service.component';

describe('MakeServiceComponent', () => {
  let component: MakeServiceComponent;
  let fixture: ComponentFixture<MakeServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MakeServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MakeServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
