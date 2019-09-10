import { StandardInventoryDto } from "../standard-inventory/StandardInventoryDto";
import { SupplierBaseDto } from "../supplierBaseManagement/supplierBaseDto";
import { DeliverySlotsDto } from "../delivery-slots/DeliverySlotsDto";
import { SupplierStandardInventoryDto } from "../user/SupplierStandardInventoryDto";
import { ZoneDto } from "../zone/ZoneDto";

export class UserCreationInitialDataDto
{
  standardInventoryList : SupplierStandardInventoryDto[];
  supplierBaseList : SupplierBaseDto[];
  deliverySlotList: DeliverySlotsDto[];
  zoneList: ZoneDto[];
}