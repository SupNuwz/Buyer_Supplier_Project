
export class StandardInventoryDto{
    id:number;
    itemName:string;
    inventoryItemCategoryId:number;
    inventoryItemCategoryName:string;  
    inventoryItemSubCategoryId:number;
    inventoryItemSubCategoryName:string;  
    quantityUnitOfMesureId:number;
    quantityUnitOfMeasureName:string;
    seasonality:string;
    minimumInventory:number;
    fileID :number;
    fileUrl:string;
    added:boolean;
    qty:number;
}