import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryCostConfigurationItemComponent } from './delivery-cost-configuration-item.component';

describe('DeliveryCostConfigurationItemComponent', () => {
  let component: DeliveryCostConfigurationItemComponent;
  let fixture: ComponentFixture<DeliveryCostConfigurationItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryCostConfigurationItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryCostConfigurationItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
