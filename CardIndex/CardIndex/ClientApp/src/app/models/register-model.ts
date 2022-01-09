export class RegisterModel{
    Email: string;
    Password: string;
    PasswordConfirm: string;
    FirstName: string;
    LastName: string;
    PhoneNumber: string;
    Login: string;

    constructor(email?: string,
        password?: string,
        passwordConfirm?: string,
        fisrtName?: string,
        lastName?: string,
        phoneNumber?: string,
        login?: string,)
    {
        this.Email = email;
        this.Password = password;
        this.PasswordConfirm = passwordConfirm;
        this.FirstName = fisrtName;
        this.LastName = lastName;
        this.PhoneNumber = phoneNumber;
        this.Login = login;
    }
}