export class Resource { 
    id: string;
    name: string;
    dateOfPublication: Date;
    author: string;
    tags: string[];
    type: string;
    description: string;

    constructor(){
        this.id="";
        this.name="";
        this.author="";
        this.tags=[];
        this.type="";
        this.description="";
        this.dateOfPublication=new Date();
    }
}