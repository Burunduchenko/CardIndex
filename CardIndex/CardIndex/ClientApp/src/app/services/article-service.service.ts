import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article } from '../models/article';
import { BuffArticle } from '../models/buff-article';

@Injectable({
  providedIn: 'root'
})
export class ArticleServiceService {
  private base_article_url = "https://localhost:44336/api/Article";
  private headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("AUTH_TOKEN")}`);

  constructor(private Http: HttpClient) { }
  
  getArticles(): Observable<Article[]>{
    return this.Http.get<Article[]>(this.base_article_url, {headers: this.headers});
  }

  deleteArticle(id: number){
    return this.Http.delete(this.base_article_url + '/delete/'+id, {headers: this.headers});
  }

  addArticle(article: BuffArticle)
  {
    return this.Http.post(this.base_article_url + "/AddAricle", article, {headers: this.headers})
  }

  updateArticle(article: BuffArticle)
  {
    return this.Http.put(this.base_article_url + "/UpdateArticle", article, {headers: this.headers})
  }

  getByThemeArticles(theme: string): Observable<Article[]>{
    return this.Http.get<Article[]>(this.base_article_url+"/getByTheme/" + theme, {headers: this.headers});
  }

  getArticleByLenght(lenght: number): Observable<Article[]>
  {
    return this.Http.get<Article[]>(this.base_article_url + "/getByLength/" + lenght, {headers: this.headers});
  }
  
  getArtilceByTitle(title: string): Observable<Article>
  {
    return this.Http.get<Article>(this.base_article_url + "/getByName/" + title, {headers: this.headers});
  }

  getByRangeOfRate(max: number, min: number): Observable<Article[]>
  {
    return this.Http.get<Article[]>(this.base_article_url + "/getByRangeOfRate/" + max + "/" + min, {headers: this.headers});
  }
}
