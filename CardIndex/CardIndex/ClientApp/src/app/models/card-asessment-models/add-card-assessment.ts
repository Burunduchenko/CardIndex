

export class AddCardAssessment
{
    rate: number
    comment: string
    cardTitle: string
    userLogin: string

    constructor(
        rate?: number,
        comment?: string,
        cardTitle?: string,
        userLogin?: string,
    )
        {
            this.rate = rate;
            this.comment = comment;
            this.cardTitle = cardTitle;
            this.userLogin = userLogin;
        }
}
