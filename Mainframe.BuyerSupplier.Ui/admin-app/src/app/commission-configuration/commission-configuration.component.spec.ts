import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommissionConfigurationComponent } from './commission-configuration.component';

describe('CommissionConfigurationComponent', () => {
  let component: CommissionConfigurationComponent;
  let fixture: ComponentFixture<CommissionConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommissionConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommissionConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
