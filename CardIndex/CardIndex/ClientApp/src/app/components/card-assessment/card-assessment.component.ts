import { Component, OnInit } from '@angular/core';
import { AddCardAssessment } from 'src/app/models/card-asessment-models/add-card-assessment';
import { CardAssessment } from 'src/app/models/card-asessment-models/card-assessment';
import { Card } from 'src/app/models/card-models/card';
import { CardAssessmentService } from 'src/app/services/card-assessment/card-assessment.service';
import { CardService } from 'src/app/services/card/card.service';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-card-assessment',
  templateUrl: './card-assessment.component.html',
  styleUrls: ['./card-assessment.component.css']
})
export class CardAssessmentComponent implements OnInit {

  private _allAssessments: CardAssessment[];
  private _allCards: Card[];
  addMode: boolean;
  addAssessmentModel: AddCardAssessment = new AddCardAssessment();

  is_admin: boolean = false;

  constructor(private _cardAssessmentService: CardAssessmentService,
    private _cardService: CardService,
    private _userService: UserService) { }

  ngOnInit() {
    this.getAllAssessment();
    this._cardService.getAllCards().subscribe(cards => this._allCards = cards);
    this.is_admin = this._userService.isInRole(['admin']);
  }


  getAllAssessment()
  {
    return this._cardAssessmentService.getAllAssessment().subscribe(assessments => this._allAssessments = assessments);
  }


  cancel() {
    this.addAssessmentModel = new AddCardAssessment();
    this.addMode = false;
  }

  add() {
    this.cancel();
    this.addMode = true;
  }

  saveAssessmentAdd()
  {
    this._cardAssessmentService.addAssessment(this.addAssessmentModel).subscribe(result => this.getAllAssessment());
    this.cancel();
  }

  deleteAssessment(assessment: CardAssessment)
  {
    this._cardAssessmentService.deleteAssessment(assessment.id).subscribe(result => this.getAllAssessment());
  }
}
