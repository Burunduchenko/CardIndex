export class UpdateUser{
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;

    constructor(
        id?: string,
        firstName?: string,
        lastName?: string,
        phoneNumber?: string,
        email?: string,
    )
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.phoneNumber = phoneNumber;
        this.email = email;
    }
}