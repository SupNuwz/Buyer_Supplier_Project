import { TestBed } from '@angular/core/testing';

import { InventoryItemCategoriesService } from './inventory-item-categories.service';

describe('InventoryItemCategoriesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InventoryItemCategoriesService = TestBed.get(InventoryItemCategoriesService);
    expect(service).toBeTruthy();
  });
});
