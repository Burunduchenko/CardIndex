import { Component, OnInit } from '@angular/core';
import { Card } from '../../models/card-models/card';
import { BuffCard } from '../../models/card-models/buff-card';
import { Theme } from '../../models/theme-models/theme';
import { CardService } from '../../services/card/card.service';
import { ThemeService } from '../../services/theme/theme.service';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-card-and-theme',
  templateUrl: './card-and-theme.component.html',
  styleUrls: ['./card-and-theme.component.css'],
  
})
export class CardAndThemesComponent implements OnInit {

  
  private _allCards: Card[];
  cards: Card[];
  searchTheme: string;
  searchTitle: string;
  lenght: number;

  minRate: number;
  maxRate: number;

  buffCard: BuffCard = new BuffCard()
  addMode: boolean = false;

  is_admin: boolean = false;
  is_author: boolean = false;


  private _allThemes: Theme[];
  buffTheme: Theme;
  addThemeMode: boolean = false;
  
  constructor(private _cardService: CardService,
    private _themeService: ThemeService,
    private _userService: UserService) { }

  ngOnInit() {
    this.getAllCards();
    this.getAllThemes();
    this.cards = this._allCards;
    this.is_admin = this._userService.isInRole(['admin']);
    this.is_author = this._userService.isInRole(['author']);
  }


  getAllThemes()
  {
    return this._themeService.getAllThemes().subscribe(themes => this._allThemes = themes);
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
    this.buffCard = new BuffCard();
    this.addMode = false;
  }

  add() {
    this.cancel();
    this.addMode = true;
  }

  saveAdd(){
    this._cardService.addCard(this.buffCard).subscribe(res => this.getAllCards());
    this.cancel();
  }

  update(card: Card)
  {
    this.buffCard = new BuffCard(card.id,
      card.title,
      card.body,
      card.authorFullName, 
      card.themeName);
  }

  saveUpdate()
  {
    this._cardService.updateCard(this.buffCard).subscribe(res => this.getAllCards());
    this.cancel();
  }

  getAllCards()
  {
    return this._cardService.getAllCards().subscribe(cards => this._allCards = cards);
  }

  delete(card: Card){
    this._cardService.deleteCard(card.id).subscribe(res => this.getAllCards());
  }

  searchByTheme() {
    if(!this.searchTheme)
    {
      this.getAllCards()
      return;
    }
    this._cardService.getCardsByTheme(this.searchTheme).subscribe(cards => this._allCards = cards);
  }

  searchByLenght()
  {
    if(!this.lenght)
    {
      this.getAllCards()
      return;
    }
    this._cardService.getCardsByLenght(this.lenght).subscribe(cards => this._allCards = cards);
  }

  searchByTitle()
  {
    if(!this.searchTitle)
    {
      this.getAllCards()
      return;
    }
    this._allCards = [];
    this._cardService.getCardByTitle(this.searchTitle)
      .subscribe(cards => this._allCards.push(cards));
  }

  searchByRate()
  {
    if(!this.maxRate || !this.minRate)
    {
      this.getAllCards();
      return;
    }
    this._cardService.getCardsByRangeOfRate(this.maxRate, this.minRate).subscribe(cards => this._allCards = cards);
  }
}
