import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private email: string;
  private password: string;
  private token: string;

  constructor(
    private Http: HttpClient,
    private _userSErvice: UserService,
    private router: Router) { }

  ngOnInit() {
  }


  login()
  {
    this._userSErvice.login(this.email, this.password).subscribe(token => this.token = token);
  }
  Submit()
  {
    this.login();
    localStorage.setItem("AUTH_TOKEN", this.token);
    this.router.navigate(['']);


    let jwtData = this.token.split('.')[1];
    let decodedJwtJsonData = window.atob(jwtData);
    let decodedJwtData = JSON.parse(decodedJwtJsonData);

    let roles = decodedJwtData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    localStorage.setItem("USER_ROLES", roles);
    
    this.router.navigate(['']).then(() => {
      window.location.reload();
    });
  }


}
