import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommissionConfigurationItemComponent } from './commission-configuration-item.component';

describe('CommissionConfigurationItemComponent', () => {
  let component: CommissionConfigurationItemComponent;
  let fixture: ComponentFixture<CommissionConfigurationItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommissionConfigurationItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommissionConfigurationItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
