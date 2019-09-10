import { TestBed } from '@angular/core/testing';

import { TimeWavesService } from './time-waves.service';

describe('TimeWavesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimeWavesService = TestBed.get(TimeWavesService);
    expect(service).toBeTruthy();
  });
});
