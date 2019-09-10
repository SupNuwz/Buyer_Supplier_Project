import { TestBed } from '@angular/core/testing';

import { DeliverySlotsService } from './delivery-slots.service';

describe('DeliverySlotsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeliverySlotsService = TestBed.get(DeliverySlotsService);
    expect(service).toBeTruthy();
  });
});
