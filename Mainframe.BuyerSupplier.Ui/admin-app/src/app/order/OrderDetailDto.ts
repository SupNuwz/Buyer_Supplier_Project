export class OrderDetailDto {
    id:number;
    orderId:number;
    standardInventoryId:number;
    qty:number;
    isDeleted:boolean;
    itemName:string;
    orderAssignments:any;
}
