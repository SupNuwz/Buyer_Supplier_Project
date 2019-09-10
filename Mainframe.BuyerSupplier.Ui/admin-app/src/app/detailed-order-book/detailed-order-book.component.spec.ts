import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailedOrderBookComponent } from './detailed-order-book.component';

describe('DetailedOrderBookComponent', () => {
  let component: DetailedOrderBookComponent;
  let fixture: ComponentFixture<DetailedOrderBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailedOrderBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailedOrderBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
