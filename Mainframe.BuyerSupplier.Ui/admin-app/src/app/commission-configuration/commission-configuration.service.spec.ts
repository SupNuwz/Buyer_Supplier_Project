import { TestBed } from '@angular/core/testing';

import { CommissionConfigurationService } from './commission-configuration.service';

describe('CommissionConfigurationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommissionConfigurationService = TestBed.get(CommissionConfigurationService);
    expect(service).toBeTruthy();
  });
});
