export class Article{
    id: number
    title: string
    body: string
    authorFullName: string
    themeName: string
    Created: Date
    AvgRate: number

    constructor(id: number,
        title: string,
        body: string,
        authorFullName: string,
        themeName: string,
        Created: Date,
        AvgRate: number)
        {
            this.id = id;
            this.title = title;
            this.body = body;
            this.authorFullName = authorFullName;
            this.themeName = themeName;
            this.Created = Created;
            this.AvgRate = AvgRate;
        }
}