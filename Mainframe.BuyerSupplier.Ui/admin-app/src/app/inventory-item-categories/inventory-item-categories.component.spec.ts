import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryItemCategoriesComponent } from './inventory-item-categories.component';

describe('InventoryItemCategoriesComponent', () => {
  let component: InventoryItemCategoriesComponent;
  let fixture: ComponentFixture<InventoryItemCategoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryItemCategoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryItemCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
