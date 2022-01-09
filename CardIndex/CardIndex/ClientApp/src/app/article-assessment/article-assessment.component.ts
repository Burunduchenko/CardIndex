import { Component, OnInit } from '@angular/core';
import { AddArticleAssessment } from '../models/add-article-assessment';
import { Article } from '../models/article';
import { ArticleAssessment } from '../models/article-assessment';
import { ArticleAssessmentService } from '../services/article-assessment.service';
import { ArticleServiceService } from '../services/article-service.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-article-assessment',
  templateUrl: './article-assessment.component.html',
  styleUrls: ['./article-assessment.component.css']
})
export class ArticleAssessmentComponent implements OnInit {

  private _allAssessments: ArticleAssessment[];
  private _allArticles: Article[];
  addMode: boolean;
  addAssessmentModel: AddArticleAssessment = new AddArticleAssessment();

  is_admin: boolean = false;

  constructor(private _articleAssessmentService: ArticleAssessmentService,
    private _articleService: ArticleServiceService,
    private _userService: UserService) { }

  ngOnInit() {
    this.getAllAssessment();
    this._articleService.getArticles().subscribe(articles => this._allArticles = articles);
    this.is_admin = this._userService.isInRole(['admin']);
  }


  getAllAssessment()
  {
    return this._articleAssessmentService.getAllAssessment().subscribe(assessments => this._allAssessments = assessments);
  }


  cancel() {
    this.addAssessmentModel = new AddArticleAssessment();
    this.addMode = false;
  }

  add() {
    this.cancel();
    this.addMode = true;
  }

  saveAssessmentAdd()
  {
    this._articleAssessmentService.addAssessment(this.addAssessmentModel).subscribe(res => this.getAllAssessment());
    this.cancel();
  }

  deleteAssessment(assessment: ArticleAssessment)
  {
    this._articleAssessmentService.deleteAssessment(assessment.id).subscribe(res => this.getAllAssessment());
  }
}
