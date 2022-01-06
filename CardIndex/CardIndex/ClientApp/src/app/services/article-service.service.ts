import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article } from '../models/article';
import { BuffArticle } from '../models/buff-article';

@Injectable({
  providedIn: 'root'
})
export class ArticleServiceService {
  private base_article_url = "https://localhost:44336/api/Article"

  constructor(private Http: HttpClient) { }
  
  getArticles(): Observable<Article[]>{
    return this.Http.get<Article[]>(this.base_article_url);
  }

  getByThemeArticles(theme: string): Observable<Article[]>{
    return this.Http.get<Article[]>(this.base_article_url+"/getByTheme/" + theme);
  }

  deleteArticle(id: number){
    return this.Http.delete(this.base_article_url + '/delete/'+id);
  }

  addArticle(article: BuffArticle)
  {
    return this.Http.post(this.base_article_url + "/AddAricle", article)
  }

  updateArticle(article: BuffArticle)
  {
    return this.Http.put(this.base_article_url + "/UpdateArticle", article)
  }
}
