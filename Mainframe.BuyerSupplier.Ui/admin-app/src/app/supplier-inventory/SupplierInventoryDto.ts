export class SupplierInventoryDto
{
    id:number;
    supplierStandardInventoryId:number;
    unitPrice:number;
    qty:number;
    availableQty:number;
    processingQty:number;
    isDeleted:boolean;
    grade:string;
    supplierName:string;
    inventoryItemName:string;
}