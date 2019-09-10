import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryItemSubCategoryItemComponent } from './inventory-item-sub-category-item.component';

describe('InventoryItemSubCategoryComponent', () => {
  let component: InventoryItemSubCategoryItemComponent;
  let fixture: ComponentFixture<InventoryItemSubCategoryItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryItemSubCategoryItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryItemSubCategoryItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
