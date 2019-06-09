import { TestBed, inject } from '@angular/core/testing';

import { DemoServiceService } from './demo-service.service';

describe('DemoServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DemoServiceService]
    });
  });

  it('should be created', inject([DemoServiceService], (service: DemoServiceService) => {
    expect(service).toBeTruthy();
  }));
});
