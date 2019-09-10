export class OrderAssignmentDto{
    id : number;
    orderDetailID : number;
    supplierInventoryID : number;
    qty : number;
    supplierAcknowledgement : boolean;
    vehicleAcknowledgement : boolean;
    buyerAcknowledgement : boolean;
    isDeleted : boolean;
}