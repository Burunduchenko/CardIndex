export class ArticleAssessment{
    id: number
    rate: number
    comment: string
    articleName: string
    userLogin: string
    date: Date

    constructor(
        id?: number,
        rate?: number,
        comment?: string,
        articleName?: string,
        userLogin?: string,
        date?: Date,
    )
        {
            this.id = id;
            this.rate = rate;
            this.comment = comment;
            this.articleName = articleName;
            this.userLogin = userLogin;
            this.date = date;
        }
}