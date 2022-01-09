export class BuffArticle{ 
    id: number
    title: string
    body: string
    authorFullName: string
    themeName: string

    constructor(
        id?: number,
        title?: string,
        body?: string,
        authorFullName?: string,
        themeName?: string,)
        {
            this.id = id;
            this.title = title;
            this.body = body;
            this.authorFullName = authorFullName;
            this.themeName = themeName;
        }
}