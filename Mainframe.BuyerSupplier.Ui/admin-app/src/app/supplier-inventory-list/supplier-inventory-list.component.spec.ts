import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierInventoryListComponent } from './supplier-inventory-list.component';

describe('SupplierInventoryListComponent', () => {
  let component: SupplierInventoryListComponent;
  let fixture: ComponentFixture<SupplierInventoryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierInventoryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierInventoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
