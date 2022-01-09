import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  private is_admin: boolean = false;
  private is_author: boolean = false;
  constructor(private _userService: UserService)
  {

  }

  ngOnInit(){
    this.is_author = this._userService.isInRole(["author"]);
    this.is_admin = this._userService.isInRole(["admin"]);
  }
}

