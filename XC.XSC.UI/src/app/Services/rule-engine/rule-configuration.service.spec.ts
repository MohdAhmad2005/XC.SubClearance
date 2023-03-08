import { TestBed } from '@angular/core/testing';

import { RuleConfigurationService } from './rule-configuration.service';

describe('RuleConfigurationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RuleConfigurationService = TestBed.get(RuleConfigurationService);
    expect(service).toBeTruthy();
  });
});
