import { TestBed } from '@angular/core/testing';

import { StandardInventoryService } from './standard-inventory.service';

describe('StandardInventoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StandardInventoryService = TestBed.get(StandardInventoryService);
    expect(service).toBeTruthy();
  });
});
