import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderPossibilityItemComponent } from './order-possibility-item.component';

describe('OrderPossibilityItemComponent', () => {
  let component: OrderPossibilityItemComponent;
  let fixture: ComponentFixture<OrderPossibilityItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderPossibilityItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderPossibilityItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
