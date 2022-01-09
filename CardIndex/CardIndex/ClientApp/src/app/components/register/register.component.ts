
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterModel } from '../../models/user-models/register-model';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerModel: RegisterModel = new RegisterModel();

  constructor(
    private _userService: UserService,
    private _router: Router) { }

  ngOnInit() {
  }

  register()
  {
    this._userService.registerUser(this.registerModel).subscribe(res => this._userService.getAllUsers());
    this.cancel(); 
  }

  cancel()
  {
    this._router.navigate(["login"]);
  }

}