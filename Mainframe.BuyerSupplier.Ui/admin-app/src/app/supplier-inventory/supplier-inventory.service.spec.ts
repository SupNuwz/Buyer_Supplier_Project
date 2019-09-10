import { TestBed } from '@angular/core/testing';

import { SupplierInventoryService } from './supplier-inventory.service';

describe('SupplierInventoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SupplierInventoryService = TestBed.get(SupplierInventoryService);
    expect(service).toBeTruthy();
  });
});
