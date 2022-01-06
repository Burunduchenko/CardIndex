import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Theme } from '../models/theme';

@Injectable({
  providedIn: 'root'
})
export class ThemeServiceService {
  private base_theme_url = "https://localhost:44336/api/Theme"
  constructor(private Http: HttpClient) { }

  getAll(): Observable<Theme[]>
  {
    return this.Http.get<Theme[]>(this.base_theme_url);
  }

  deleteTheme(id: number)
  {
    return this.Http.delete(this.base_theme_url + "/delete/" + id);
  }

  addTheme(theme: Theme)
  {
    return this.Http.post(this.base_theme_url, theme);
  }
}
