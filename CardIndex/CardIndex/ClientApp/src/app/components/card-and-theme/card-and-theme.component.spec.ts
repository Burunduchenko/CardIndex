import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import {CardAndThemesComponent } from './card-and-theme.component';

describe('ArticleAndthemesComponent', () => {
  let component: CardAndThemesComponent;
  let fixture: ComponentFixture<CardAndThemesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardAndThemesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardAndThemesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
