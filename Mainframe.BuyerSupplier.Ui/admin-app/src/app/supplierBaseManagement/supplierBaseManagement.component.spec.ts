import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierBaseManagementComponent } from './supplierBaseManagement.component';

describe('SupplierBaseManagementComponent', () => {
  let component: SupplierBaseManagementComponent;
  let fixture: ComponentFixture<SupplierBaseManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierBaseManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierBaseManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
