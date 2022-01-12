import { TestBed } from '@angular/core/testing';

import { CardService } from './card.service';

describe('ArticleServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CardService = TestBed.get(CardService);
    expect(service).toBeTruthy();
  });
});
