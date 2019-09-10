import { OrderOptimizedDetailDto } from "./OrderOptimizedDetailDto";

export class OrderOptimizedPossibilityDto{
    
    //OrderPossibilityType : typeof OrderPossibilityType = OrderPossibilityType;

    orderOptimizedPossibilityId:number;
    itemCost:number;
    deliveryCost:number;
    orderValue:number;
    supplierBaseId:number;
    supplierBase:string;
    orderOptimizedDetails:OrderOptimizedDetailDto[];
    orderPossibilityType:OrderPossibilityType;
}