export class Theme{
    id?: number;
    name: string;

    constructor(name?: string, id?: number)
    {
        this.name = name;
        this.id = id;
    }

}