import{SupplierBaseDto} from '../supplierBaseManagement/supplierBaseDto';

export class UserDto{
    id:number;
    name:string;
    address:string;
    contactNo:string;
    email:string;
    userType:string;
    defaultSupplierBaseId:number;
    supplierBases:SupplierBaseDto[];
    defaultSupplierBaseName:string;
    deliverySlotId :number;
    deliverySlotName :string;
    category :string;
    inventoryItemName:string;
    standaradInventoryID:number;
    relevantZoneId:number;
    relevantZoneName:string;

}