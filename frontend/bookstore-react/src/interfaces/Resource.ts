export interface Resource {
    id: string;
    name: string;
    dateOfPublication: Date;
    author: string;
    tags: string[];
    type: string;
    description: string;
}