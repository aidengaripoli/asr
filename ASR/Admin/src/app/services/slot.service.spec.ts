import { TestBed } from '@angular/core/testing';

import { SlotService } from './slot.service';

describe('SlotService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SlotService = TestBed.get(SlotService);
    expect(service).toBeTruthy();
  });
});
