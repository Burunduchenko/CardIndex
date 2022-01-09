import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent  implements OnInit {

  private is_admin: boolean = false;
  private is_authorize: boolean = false;

  constructor(private router: Router,
    private _userService: UserService)
  {

  }
  ngOnInit(): void {
    this.is_admin = this._userService.isInRole(['admin']);
    this.is_authorize = this._userService.isAuthorize();
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  SingOut()
  {
    localStorage.removeItem("AUTH_TOKEN");
    localStorage.removeItem("USER_ROLES");
    this.router.navigate(["login"]).then(() => {
      window.location.reload();
    });
  }

  register()
  {
    this.router.navigate(["/register"]);
  }
}
