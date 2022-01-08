import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private router: Router,
    private _userService: UserService)
  {

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
    this.router.navigate(["login"]);
  }

  isInRoles(roles: string[]): boolean
  {
    return this._userService.isInRole(roles);
  }
}
