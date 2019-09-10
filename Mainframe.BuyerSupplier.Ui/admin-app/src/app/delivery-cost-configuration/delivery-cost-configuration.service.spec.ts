import { TestBed } from '@angular/core/testing';

import { DeliveryCostConfigurationService } from './delivery-cost-configuration.service';

describe('DeliveryCostConfigurationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeliveryCostConfigurationService = TestBed.get(DeliveryCostConfigurationService);
    expect(service).toBeTruthy();
  });
});
