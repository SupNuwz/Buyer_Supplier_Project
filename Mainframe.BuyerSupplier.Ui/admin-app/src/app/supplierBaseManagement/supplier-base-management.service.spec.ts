import { TestBed } from '@angular/core/testing';

import { SupplierBaseManagementService } from './supplier-base-management.service';

describe('NewZoneServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SupplierBaseManagementService = TestBed.get(SupplierBaseManagementService);
    expect(service).toBeTruthy();
  });
});
