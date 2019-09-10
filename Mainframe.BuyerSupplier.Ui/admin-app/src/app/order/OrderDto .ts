import { OrderDetailDto } from "./OrderDetailDto";

export class OrderDto{
    id:number;
    orderRefNo:string;
    buyerId:number;
    orderedDate:Date; 
    expectedDiliveredDate:Date;
    orderType:number; 
    status:number;
    deliverySlotId:number;
    supplierCategory: string;
    isDeleted:boolean;
    assignmentSelectionType:number;
    deliverySlotName:string;
    buyerName:string;
    orderDetails:OrderDetailDto[]; 
}
