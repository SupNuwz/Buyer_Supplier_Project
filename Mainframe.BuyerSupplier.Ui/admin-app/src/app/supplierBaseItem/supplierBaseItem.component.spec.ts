import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierBaseItemComponent } from './supplierBaseItem.component';

describe('AddNewZoneComponent', () => {
  let component: SupplierBaseItemComponent;
  let fixture: ComponentFixture<SupplierBaseItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierBaseItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierBaseItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
