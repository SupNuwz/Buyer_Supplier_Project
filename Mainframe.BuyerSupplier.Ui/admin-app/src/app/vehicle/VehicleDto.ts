export class VehicleDto{
    id:number;
    supplierBaseId: number;
    driverContactNo: number;
    numberPlate: string;
    vehicleTypeId: number;
    availability:boolean = false;
    supplierBase: string;
    vehicleType: string;
    colorCode:string;
    maximumCapacity:string;
}