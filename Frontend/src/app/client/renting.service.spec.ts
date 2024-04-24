import { TestBed } from '@angular/core/testing';

import { RentingService } from './renting.service';

describe('RentingService', () => {
  let service: RentingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
