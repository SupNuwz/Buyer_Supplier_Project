import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryItemSubCategoryComponent } from './inventory-item-sub-category.component';

describe('InventoryItemSubCategoryComponent', () => {
  let component: InventoryItemSubCategoryComponent;
  let fixture: ComponentFixture<InventoryItemSubCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryItemSubCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryItemSubCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
