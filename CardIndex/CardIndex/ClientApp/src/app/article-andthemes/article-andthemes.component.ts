import { Component, OnInit } from '@angular/core';
import { createFalse } from 'typescript';
import { Article } from '../models/article';
import { BuffArticle } from '../models/buff-article';
import { Theme } from '../models/theme';
import { ArticleServiceService } from '../services/article-service.service';
import { ThemeServiceService } from '../services/theme-service.service';

@Component({
  selector: 'app-article-andthemes',
  templateUrl: './article-andthemes.component.html',
  styleUrls: ['./article-andthemes.component.css'],
  
})
export class ArticleAndthemesComponent implements OnInit {

  
  private _allArticles: Article[];
  articles: Article[];
  searchTheme: string;
  searchTitle: string;
  lenght: number;

  minRate: number;
  maxRate: number;

  buffArticle: BuffArticle = new BuffArticle()
  addMode: boolean = false;


  private _allThemes: Theme[];
  buffTheme: Theme;
  addThemeMode: boolean = false;
  
  constructor(private _articleService: ArticleServiceService,
    private _themeService: ThemeServiceService) { }

  ngOnInit() {
    this.getAllArticles();
    this.getAllThemes();
    this.articles = this._allArticles;
  }


  getAllThemes()
  {
    return this._themeService.getAll().subscribe(themes => this._allThemes = themes);
  }

  deleteTheme(theme: Theme)
  {
    this._themeService.deleteTheme(theme.id).subscribe(res => this.getAllThemes());
  }


  cancelTheme()
  {
    this.buffTheme = new Theme();
    this.addThemeMode = false;
  }

  addTheme(theme: Theme )
  {
    this.cancelTheme()
    this.addThemeMode = true;
  }

  saveThemeAdd()
  {
    this._themeService.addTheme(this.buffTheme).subscribe(res => this.getAllThemes());
    this.cancelTheme();
  }


  cancel() {
    this.buffArticle = new BuffArticle();
    this.addMode = false;
  }

  add() {
    this.cancel();
    this.addMode = true;
  }

  saveAdd(){
    this._articleService.addArticle(this.buffArticle).subscribe(res => this.getAllArticles());
    this.cancel();
  }

  update(article: Article)
  {
    this.buffArticle = new BuffArticle(article.id,
      article.title,
      article.body,
      article.authorFullName, 
      article.themeName);
  }

  saveUpdate()
  {
    this._articleService.updateArticle(this.buffArticle).subscribe(res => this.getAllArticles());
    this.cancel();
  }

  getAllArticles()
  {
    return this._articleService.getArticles().subscribe(articles => this._allArticles = articles);
  }

  delete(article: Article){
    this._articleService.deleteArticle(article.id).subscribe(res => this.getAllArticles());
  }

  searchByTheme() {
    if(!this.searchTheme)
    {
      this.getAllArticles()
      return;
    }
    this._articleService.getByThemeArticles(this.searchTheme).subscribe(articles => this._allArticles = articles);
  }

  searchByLenght()
  {
    if(!this.lenght)
    {
      this.getAllArticles()
      return;
    }
    this._articleService.getArticleByLenght(this.lenght).subscribe(articles => this._allArticles = articles);
  }

  searchByTitle()
  {
    if(!this.searchTitle)
    {
      this.getAllArticles()
      return;
    }
    this._allArticles = [];
    this._articleService.getArtilceByTitle(this.searchTitle)
    .subscribe(articles => this._allArticles.push(articles));
  }

  searchByRate()
  {
    if(!this.maxRate || !this.minRate)
    {
      this.getAllArticles();
      return;
    }
    this._articleService.getByRangeOfRate(this.maxRate, this.minRate).subscribe(res => this._allArticles = res);
  }
}
