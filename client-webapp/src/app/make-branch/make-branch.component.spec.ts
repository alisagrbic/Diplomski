import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeBranchComponent } from './make-branch.component';

describe('MakeBranchComponent', () => {
  let component: MakeBranchComponent;
  let fixture: ComponentFixture<MakeBranchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MakeBranchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MakeBranchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
