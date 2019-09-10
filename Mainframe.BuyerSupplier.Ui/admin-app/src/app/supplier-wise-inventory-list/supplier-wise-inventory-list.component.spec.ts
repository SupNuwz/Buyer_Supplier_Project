import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierWiseInventoryListComponent } from './supplier-wise-inventory-list.component';

describe('SupplierWiseInventoryListComponent', () => {
  let component: SupplierWiseInventoryListComponent;
  let fixture: ComponentFixture<SupplierWiseInventoryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierWiseInventoryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierWiseInventoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
