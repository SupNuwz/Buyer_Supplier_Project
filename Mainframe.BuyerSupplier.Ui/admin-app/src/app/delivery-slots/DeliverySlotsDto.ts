import { Time } from "@angular/common";

export class DeliverySlotsDto{
    id:number;
    slotName:string;
    firstWaveTime:string;
    secondWaveTime:string;
    startTime:string;
    cutoffTime:string;
    countdownTime:string;
    orderAcceptTime:string;
    orderCofirmTime:string;
    dispatchesConfirmTime:string;
    endTime:string;
    disabled:boolean;
}