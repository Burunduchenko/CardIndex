import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ManipWithUserRole } from '../models/manipWithUserRole';
import { Role } from '../models/role';
import { UpdateUser } from '../models/update-user';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private base_Administartion_url = "https://localhost:44336/Administration";
  constructor(private Http: HttpClient) { }


  createRole(role: Role)
  {
    return this.Http.post(this.base_Administartion_url + "/createRole", role);
  }

  getRoles(): Observable<Role[]>
  {
    return this.Http.get<Role[]>(this.base_Administartion_url + "/getRoles");
  }

  probideUserToRole(provideUserToRoles: ManipWithUserRole)
  {
    return this.Http.post(this.base_Administartion_url + "/provideUserToRole", provideUserToRoles);
  }

  takeUserFromRole(takeUserFromRoleModel: ManipWithUserRole)
  {
    return this.Http.post(this.base_Administartion_url + "/takeUserFromRole", takeUserFromRoleModel);
  }

  getAllUsers(): Observable<User[]>
  {
    return this.Http.get<User[]>(this.base_Administartion_url + "/GetAllUsers");
  }

  deleteUser(id: string)
  {
    return this.Http.delete(this.base_Administartion_url + "/DeleteUser/" + id);
  }

  deleteRole(id: string)
  {
      return this.Http.delete(this.base_Administartion_url + "/DeleteRole/" + id)
  }

  updateUser(user: UpdateUser)
  {
    return this.Http.put(this.base_Administartion_url + "/Update", user);
  }
}
