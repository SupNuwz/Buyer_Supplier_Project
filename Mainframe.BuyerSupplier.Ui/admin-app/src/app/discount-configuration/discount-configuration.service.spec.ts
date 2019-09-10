import { TestBed } from '@angular/core/testing';

import { DiscountConfigurationService } from './discount-configuration.service';

describe('DiscountConfigurationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DiscountConfigurationService = TestBed.get(DiscountConfigurationService);
    expect(service).toBeTruthy();
  });
});
