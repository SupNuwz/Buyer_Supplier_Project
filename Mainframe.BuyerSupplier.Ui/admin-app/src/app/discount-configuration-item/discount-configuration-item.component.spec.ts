import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscountConfigurationItemComponent } from './discount-configuration-item.component';

describe('DiscountConfigurationItemComponent', () => {
  let component: DiscountConfigurationItemComponent;
  let fixture: ComponentFixture<DiscountConfigurationItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiscountConfigurationItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiscountConfigurationItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
