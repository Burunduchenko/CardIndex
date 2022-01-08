import { Component, OnInit } from '@angular/core';
import { ManipWithUserRole } from '../models/manipWithUserRole';
import { Role } from '../models/role';
import { UpdateUser } from '../models/update-user';
import { User } from '../models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  private _allUsers: User[];
  private _allRoles: Role[];

  createdRole: Role;
  update_User: UpdateUser;
  asignUserToRole: ManipWithUserRole; 
  addRoleMode: boolean = false;
  provideMode: boolean = false;
  takeAwayMode: boolean = false;

  constructor(private _userService: UserService ) { }

  ngOnInit() {
    this.getAllUsers();
    this.getAllRoles();
  }

  addRole()
  {
    this.cancel();
    this.addRoleMode = true;
  }

  cancel() {
    this.update_User = new UpdateUser();
    this.createdRole = new Role();
    this.addRoleMode = false;

  }



  saveAddRole()
  {
    this._userService.createRole(this.createdRole).subscribe(res => this.getAllRoles());
    this.cancel();
  }


  deleteRole(role: Role)
  {
    this._userService.deleteRole(role.id).subscribe(res => this.getAllRoles());
  }

  getAllRoles()
  {
    this._userService.getRoles().subscribe(res => this._allRoles = res);
  }

  provideUserToRole()
  {
    this.cancelProvide();
    this.provideMode = true;
  }

  saveProvide()
  {
    this._userService.probideUserToRole(this.asignUserToRole).subscribe(res => this.getAllUsers());
    this.cancelProvide();
  }

  cancelProvide()
  {
    this.asignUserToRole = new ManipWithUserRole()
    this.provideMode = false;
  }

  takeUserFromRole()
  {
    this.cancelTakeAway();
    this.takeAwayMode = true;
  }

  cancelTakeAway()
  {
    this.asignUserToRole = new ManipWithUserRole();
    this.takeAwayMode = false;
  }

  saveTakeAway()
  {
    this._userService.takeUserFromRole(this.asignUserToRole).subscribe(res => this.getAllUsers());
    this.cancelTakeAway();
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

  deleteUser(user: User)
  {
    this._userService.deleteUser(user.id).subscribe(res => this.getAllUsers());
  }

}
