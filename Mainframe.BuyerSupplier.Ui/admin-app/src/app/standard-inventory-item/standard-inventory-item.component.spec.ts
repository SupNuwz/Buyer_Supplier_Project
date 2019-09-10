import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StandardInventoryItemComponent } from './standard-inventory-item.component';

describe('StandardInventoryItemComponent', () => {
  let component: StandardInventoryItemComponent;
  let fixture: ComponentFixture<StandardInventoryItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StandardInventoryItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StandardInventoryItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
