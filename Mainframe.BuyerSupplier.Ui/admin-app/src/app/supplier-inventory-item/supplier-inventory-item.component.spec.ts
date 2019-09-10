import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierInventoryItemComponent } from './supplier-inventory-item.component';

describe('SupplierInventoryItemComponent', () => {
  let component: SupplierInventoryItemComponent;
  let fixture: ComponentFixture<SupplierInventoryItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierInventoryItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierInventoryItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
