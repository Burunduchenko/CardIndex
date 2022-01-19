export class UpdateUser{
    id: string;
    login: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;

    constructor(
        id?: string,
        firstName?: string,
        lastName?: string,
        phoneNumber?: string,
        login?: string,
    )
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.phoneNumber = phoneNumber;
        this.login = login;
    }
}