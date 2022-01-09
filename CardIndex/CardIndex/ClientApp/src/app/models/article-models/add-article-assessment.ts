

export class AddArticleAssessment
{
    rate: number
    comment: string
    articleTitle: string
    userLogin: string

    constructor(
        rate?: number,
        comment?: string,
        articleTitle?: string,
        userLogin?: string,
    )
        {
            this.rate = rate;
            this.comment = comment;
            this.articleTitle = articleTitle;
            this.userLogin = userLogin;
        }
}