import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCardAssessment } from 'src/app/models/card-asessment-models/add-card-assessment';
import { CardAssessment } from '../../models/card-asessment-models/card-assessment';

@Injectable({
  providedIn: 'root'
})
export class CardAssessmentService {

  private base_card_assessment_url = "https://localhost:44336/api/CardAssessment";
  private headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("AUTH_TOKEN")}`) ;

  constructor(private Http: HttpClient) { }

  getAllAssessment(): Observable<CardAssessment[]>
  {
    return this.Http.get<CardAssessment[]>(this.base_card_assessment_url, {headers: this.headers});
  }

  deleteAssessment(id: number)
  {
    return this.Http.delete(this.base_card_assessment_url + "/delete?id=" + id, {headers: this.headers});
  }

  addAssessment(assessment: AddCardAssessment)
  {
    return this.Http.post(this.base_card_assessment_url, assessment, {headers: this.headers});
  }
}
