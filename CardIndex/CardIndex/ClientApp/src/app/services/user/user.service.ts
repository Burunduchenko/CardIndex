import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ManipWithUserRole } from '../../models/user-models/manipWithUserRole';
import { RegisterModel } from '../../models/user-models/register-model';
import { Role } from '../../models/user-models/role';
import { UpdateUser } from '../../models/user-models/update-user';
import { User } from '../../models/user-models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private base_Administartion_url = "https://localhost:44336/Administration";
  private headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("AUTH_TOKEN")}`) 
  constructor(private Http: HttpClient) { }

  createRole(role: Role)
  {
    return this.Http.post(this.base_Administartion_url + "/createRole", role, {headers: this.headers});
  }

  getAllRoles(): Observable<Role[]>
  {
    return this.Http.get<Role[]>(this.base_Administartion_url + "/getRoles", {headers: this.headers});
  }

  probideUserToRole(provideUserToRoles: ManipWithUserRole)
  {

    return this.Http.post(this.base_Administartion_url + "/provideUserToRole", provideUserToRoles, {headers: this.headers});
  }

  takeUserFromRole(takeUserFromRoleModel: ManipWithUserRole)
  {
    return this.Http.post(this.base_Administartion_url + "/takeUserFromRole", takeUserFromRoleModel, {headers: this.headers});
  }

  getAllUsers(): Observable<User[]>
  {
    
    return this.Http.get<User[]>(this.base_Administartion_url + "/GetAllUsers", {headers: this.headers});
  }

  deleteUser(id: string)
  {
    return this.Http.delete(this.base_Administartion_url + "/DeleteUser?id=" + id, {headers: this.headers});
  }

  deleteRole(id: string)
  {
      return this.Http.delete(this.base_Administartion_url + "/DeleteRole?id=" + id, {headers: this.headers})
  }

  updateUser(user: UpdateUser)
  {
    return this.Http.put(this.base_Administartion_url + "/Update", user, {headers: this.headers});
  }

  login(email: string, password: string): Observable<string>
  {
    return this.Http.post<string>(this.base_Administartion_url + "/logon",  {email: email, password: password});
  }


  registerUser(registerModel: RegisterModel): Observable<void>
  {
    return this.Http.post<void>(this.base_Administartion_url + "/register",  registerModel);
  }

  isInRole(roles: string[]): boolean
  {
    let userRoles = localStorage.getItem("USER_ROLES").split(",");
    for(let role of roles)
    {
      for(let userRole of userRoles)
      {
        if(role == userRole)
        {
          return true;
        }
      }
    }
    return false;
  }

  
  isAuthorize():boolean
  {
    if(localStorage.getItem("AUTH_TOKEN"))
    {
      return true;
    }
    return false;
  }

}
