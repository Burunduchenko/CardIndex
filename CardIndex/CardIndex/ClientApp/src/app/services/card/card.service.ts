import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Card } from '../../models/card-models/card';
import { BuffCard } from '../../models/card-models/buff-card';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  private base_card_url = "https://localhost:44336/api/Card";
  private headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("AUTH_TOKEN")}`);

  constructor(private Http: HttpClient) { }
  
  getAllCards(): Observable<Card[]>{
    return this.Http.get<Card[]>(this.base_card_url, {headers: this.headers});
  }

  deleteCard(id: number){
    return this.Http.delete(this.base_card_url + '/delete?id='+id, {headers: this.headers});
  }

  addCard(card: BuffCard)
  {
    return this.Http.post(this.base_card_url + "/AddCard", card, {headers: this.headers})
  }

  updateCard(card: BuffCard)
  {
    return this.Http.put(this.base_card_url + "/UpdateCard", card, {headers: this.headers})
  }

  getCardsByTheme(theme: string): Observable<Card[]>{
    return this.Http.get<Card[]>(this.base_card_url+"/getByTheme?theme=" + theme, {headers: this.headers});
  }

  getCardsByLenght(lenght: number): Observable<Card[]>
  {
    return this.Http.get<Card[]>(this.base_card_url + "/getByLength?length=" + lenght, {headers: this.headers});
  }
  
  getCardByTitle(title: string): Observable<Card>
  {
    return this.Http.get<Card>(this.base_card_url + "/getByTitle?title=" + title, {headers: this.headers});
  }

  getCardsByRangeOfRate(max: number, min: number): Observable<Card[]>
  {
    return this.Http.get<Card[]>(this.base_card_url + "/getByRangeOfRate?max="+ max+"&min="  + min, {headers: this.headers});
  }
}
