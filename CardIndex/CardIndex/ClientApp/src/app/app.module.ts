import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ArticleAndthemesComponent } from './article-andthemes/article-andthemes.component';
import { ArticleAssessmentComponent } from './article-assessment/article-assessment.component';
import { UserComponent } from './user/user.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ArticleAndthemesComponent,
    ArticleAssessmentComponent,
    UserComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'articles', component: ArticleAndthemesComponent },
      { path: 'article-assessment', component: ArticleAssessmentComponent },
      { path: 'users', component: UserComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
