import { SupplierInventoryDto } from "../supplier-inventory/SupplierInventoryDto";

export class SupplierStandardInventoryDto{
 id:number;
 standaradInventoryID:number;
 supplierID:number;
 inventoryItemName:string;
 group:string;
 isSelected:boolean;
supplierInventories:SupplierInventoryDto[];
}