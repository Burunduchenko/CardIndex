export class CardAssessment{
    id: number
    rate: number
    comment: string
    cardTitle: string
    userLogin: string
    date: Date

    constructor(
        id?: number,
        rate?: number,
        comment?: string,
        cardTitle?: string,
        userLogin?: string,
        date?: Date,
    )
        {
            this.id = id;
            this.rate = rate;
            this.comment = comment;
            this.cardTitle = cardTitle;
            this.userLogin = userLogin;
            this.date = date;
        }
}
