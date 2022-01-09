import { TestBed } from '@angular/core/testing';

import { ArticleAssessmentService } from './article-assessment.service';

describe('ArticleAssessmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ArticleAssessmentService = TestBed.get(ArticleAssessmentService);
    expect(service).toBeTruthy();
  });
});
