import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Theme } from '../../models/theme-models/theme';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private base_theme_url = "https://localhost:44336/api/Theme"
  private headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("AUTH_TOKEN")}`) 
  constructor(private Http: HttpClient) { }

  getAllThemes(): Observable<Theme[]>
  {
    return this.Http.get<Theme[]>(this.base_theme_url, {headers: this.headers});
  }

  deleteTheme(id: number)
  {
    return this.Http.delete(this.base_theme_url + "/delete?id=" + id, {headers: this.headers});
  }

  addTheme(theme: Theme)
  {
    return this.Http.post(this.base_theme_url, theme, {headers: this.headers});
  }
}
