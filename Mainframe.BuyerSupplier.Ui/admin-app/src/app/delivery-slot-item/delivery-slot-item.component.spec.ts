import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverySlotItemComponent } from './delivery-slot-item.component';

describe('DeliverySlotItemComponent', () => {
  let component: DeliverySlotItemComponent;
  let fixture: ComponentFixture<DeliverySlotItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliverySlotItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliverySlotItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
