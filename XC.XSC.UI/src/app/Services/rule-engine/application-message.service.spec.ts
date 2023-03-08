import { TestBed } from '@angular/core/testing';

import { ApplicationMessageService } from './application-message.service';

describe('ApplicationMessageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationMessageService = TestBed.get(ApplicationMessageService);
    expect(service).toBeTruthy();
  });
});
