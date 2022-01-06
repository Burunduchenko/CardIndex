import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddArticleAssessment } from '../models/add-article-assessment';
import { ArticleAssessment } from '../models/article-assessment';

@Injectable({
  providedIn: 'root'
})
export class ArticleAssessmentService {

  private base_article_assessment_url = "https://localhost:44336/api/ArticleRate"

  constructor(private Http: HttpClient) { }

  getAllAssessment(): Observable<ArticleAssessment[]>
  {
    return this.Http.get<ArticleAssessment[]>(this.base_article_assessment_url);
  }

  deleteAssessment(id: number)
  {
    return this.Http.delete(this.base_article_assessment_url + "/delete/" + id);
  }

  addAssessment(assessment: AddArticleAssessment)
  {
    return this.Http.post(this.base_article_assessment_url, assessment);
  }
}
