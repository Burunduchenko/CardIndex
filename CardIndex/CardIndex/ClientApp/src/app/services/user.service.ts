import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  private headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("AUTH_TOKEN")}`) 
  constructor(private Http: HttpClient) { }

  createRole(role: Role)
  {
    return this.Http.post(this.base_Administartion_url + "/createRole", role);
  }

  getRoles(): Observable<Role[]>
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
    return this.Http.delete(this.base_Administartion_url + "/DeleteUser/" + id, {headers: this.headers});
  }

  deleteRole(id: string)
  {
      return this.Http.delete(this.base_Administartion_url + "/DeleteRole/" + id, {headers: this.headers})
  }

  updateUser(user: UpdateUser)
  {
    return this.Http.put(this.base_Administartion_url + "/Update", user, {headers: this.headers});
  }

  login(email: string, password: string): Observable<string>
  {
    return this.Http.post<string>(this.base_Administartion_url + "/logon",  {email: email, password: password});
  }


  isInRole(roles: string[]): boolean
  {
    let userRoles = localStorage.getItem("USER_ROLES");
    for(let role of roles)
    {
      for(let userRole of userRoles)
      {
        if(role == userRole)
        {
          console.log('is in role: true');
          return true;
        }
      }
    }
    console.log('is in role:  fasle');
    return false;
  }


}
