import { TestBed } from '@angular/core/testing';

import { CardAssessmentService } from './card-assessment.service';

describe('ArticleAssessmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CardAssessmentService = TestBed.get(CardAssessmentService);
    expect(service).toBeTruthy();
  });
});
