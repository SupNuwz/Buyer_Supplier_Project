import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierInventoryComponent } from './supplier-inventory.component';

describe('SupplierInventoryComponent', () => {
  let component: SupplierInventoryComponent;
  let fixture: ComponentFixture<SupplierInventoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierInventoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
