import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleAndthemesComponent } from './article-and-themes.component';

describe('ArticleAndthemesComponent', () => {
  let component: ArticleAndthemesComponent;
  let fixture: ComponentFixture<ArticleAndthemesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleAndthemesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleAndthemesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
