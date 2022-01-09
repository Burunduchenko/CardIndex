export class User{

    id: string
    email: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    login: string; 
    roles: string[];

    constructor(
        id: string,
        email: string,
        firstName: string,
        lastName: string,
        phoneNumber: string,
        login: string, 
        roles: string[]
    )
    {
        this.id = id;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.phoneNumber = phoneNumber;
        this.login = login;
        this.roles = roles;
    }
}