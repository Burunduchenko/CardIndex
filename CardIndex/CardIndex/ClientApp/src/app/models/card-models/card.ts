export class Card{
    id: number
    title: string
    body: string
    authorFullName: string
    themeName: string
    Created: Date
    avgRate: number

    constructor(id: number,
        title: string,
        body: string,
        authorFullName: string,
        themeName: string,
        Created: Date,
        avgRate: number)
        {
            this.id = id;
            this.title = title;
            this.body = body;
            this.authorFullName = authorFullName;
            this.themeName = themeName;
            this.Created = Created;
            this.avgRate = avgRate;
        }
}