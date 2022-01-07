import { Component, OnInit } from '@angular/core';
import { UpdateUser } from '../models/update-user';
import { User } from '../models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  private _allUsers: User[]
  update_User: UpdateUser;

  constructor(private _userService: UserService ) { }

  ngOnInit() {
    this.getAllUsers();
    this.update_User = new UpdateUser();
  }

  getAllUsers()
  {
    this._userService.getAllUsers().subscribe(res => this._allUsers = res);
  }

  updateUser(user: User)
  {
    this.update_User = new UpdateUser(user.id,
      user.firstName,
      user.lastName,
      user.phoneNumber, 
      user.email,
      );
  }

  saveUpdate()
  {
    this._userService.updateUser(this.update_User).subscribe(res => this.getAllUsers());
    this.cancel();
  }

  cancel() {
    this.update_User = new UpdateUser();
  }

  deleteUser(user: User)
  {
    this._userService.deleteUser(user.id).subscribe(res => this.getAllUsers());
  }
}
