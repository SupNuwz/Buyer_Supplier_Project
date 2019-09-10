export class NavBarDto{
    routerLink:string;
    title:string;
    name:string;
    isMenuItem:boolean = false;
    childs: NavBarDto[] = [];
}