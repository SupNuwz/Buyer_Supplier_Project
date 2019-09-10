import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryCostConfigurationComponent } from './delivery-cost-configuration.component';

describe('DeliveryCostConfigurationComponent', () => {
  let component: DeliveryCostConfigurationComponent;
  let fixture: ComponentFixture<DeliveryCostConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryCostConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryCostConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
