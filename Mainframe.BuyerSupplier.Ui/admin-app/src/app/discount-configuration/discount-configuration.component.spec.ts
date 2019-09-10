import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscountConfigurationComponent } from './discount-configuration.component';

describe('DiscountConfigurationComponent', () => {
  let component: DiscountConfigurationComponent;
  let fixture: ComponentFixture<DiscountConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiscountConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiscountConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
