import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleAssessmentComponent } from './article-assessment.component';

describe('ArticleAssessmentComponent', () => {
  let component: ArticleAssessmentComponent;
  let fixture: ComponentFixture<ArticleAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
