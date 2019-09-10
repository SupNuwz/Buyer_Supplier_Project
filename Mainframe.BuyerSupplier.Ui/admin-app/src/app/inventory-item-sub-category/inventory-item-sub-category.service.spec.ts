import { TestBed } from '@angular/core/testing';

import { InventoryItemSubCategoryService } from './inventory-item-sub-category.service';

describe('InventoryItemSubCategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InventoryItemSubCategoryService = TestBed.get(InventoryItemSubCategoryService);
    expect(service).toBeTruthy();
  });
});
