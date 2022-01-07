import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UpdateUser } from '../models/update-user';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private base_Administartion_url = "https://localhost:44336/Administration";
  constructor(private Http: HttpClient) { }


  createRole()
  {

  }

  getRoles()
  {

  }

  assignUserToRoles()
  {

  }

  getAllUsers(): Observable<User[]>
  {
    return this.Http.get<User[]>(this.base_Administartion_url + "/GetAllUsers");
  }

  deleteUser(id: string)
  {
    return this.Http.delete(this.base_Administartion_url + "/DeleteUser/" + id);
  }

  deleteRole()
  {

  }

  updateUser(user: UpdateUser)
  {
    return this.Http.put(this.base_Administartion_url + "/Update", user);
  }
}
