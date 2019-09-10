import { TestBed } from '@angular/core/testing';

import { UnitofmeasureService } from './unitofmeasure.service';

describe('UnitofmeasureService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UnitofmeasureService = TestBed.get(UnitofmeasureService);
    expect(service).toBeTruthy();
  });
});
