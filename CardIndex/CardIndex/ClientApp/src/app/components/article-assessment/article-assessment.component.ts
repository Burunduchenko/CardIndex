import { Component, OnInit } from '@angular/core';
import { AddArticleAssessment } from '../../models/article-models/add-article-assessment';
import { Article } from '../../models/article-models/article';
import { ArticleAssessment } from '../../models/article-asessment-models/article-assessment';
import { ArticleAssessmentService } from '../../services/article-assessment/article-assessment.service';
import { ArticleService } from '../../services/article/article.service';
import { UserService } from '../../services/user/user.service';

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
    private _articleService: ArticleService,
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
