import { Component, OnInit, ViewChild } from '@angular/core';
import { VehicleDto } from '././VehicleDto';
import { VehicleService } from './vehicle.service';
import { VehicleItemComponent } from '../vehicle-item/vehicle-item.component'
import {MatDialog,MatSort, MatTableDataSource, MatPaginator,MatSnackBar} from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css']
})
export class VehicleComponent implements OnInit {
  ​
   displayedColumns= ['id', 'supplierBaseId', 'driverContactNo', 'numberPlate','vehicleTypeId','colorCode','maximumCapacity','availability', 'edit','delete'];
   vehicleList :MatTableDataSource<VehicleDto>;
   
   @ViewChild(MatSort) sort: MatSort;
   @ViewChild(MatPaginator) paginator: MatPaginator;
   filterValue: string;
   messageDto: any;
   
   applyFilter() {
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.vehicleList.filter = filtetString;
  }

   constructor( private vehicleService:VehicleService, public dialog: MatDialog,public snackBar: MatSnackBar) { }
  ​
   ngOnInit() {

    let so:VehicleDto=new VehicleDto();
    this.vehicleService.getVehicle().subscribe(data=>{this.refreshGrid(data);});
    }
  ​
    refreshGrid(data:VehicleDto[]){
     this.vehicleList = new MatTableDataSource(data);
     this.vehicleList.sort = this.sort;
      
      this.vehicleList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
       if (typeof data[sortHeaderId] === 'string') {
        return data[sortHeaderId].toLocaleLowerCase();
       }
      
       return data[sortHeaderId];
      };
      this.vehicleList.paginator = this.paginator;
      this.applyFilter();
     };  
   
   add(){
    const dialogRef = this.dialog.open(VehicleItemComponent, {
     disableClose:true,
     autoFocus:true
    });
  ​
    dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
      this.vehicleService.getVehicle().subscribe(data=>{this.refreshGrid(data);})
    }
    });
   }
  ​
   delete(item:VehicleDto){
    this.messageDto = new MessageDialogDto();
      this.messageDto.messageCaption = "Vehicle Delete Confirmation";
      this.messageDto.messageBody = "This " + item.supplierBase + " may be used in other transactions. Do you still want to delete it?";
      
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
       disableClose:true,
       data : this.messageDto
      });
    
    dialogRef.afterClosed().subscribe(result => {
     if(result == true)
     {
      this.vehicleService.delete(item.id).subscribe(
       data=>{var maxPageIndex = data.length / this.paginator.pageSize; 
        if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
         this.paginator.pageIndex = maxPageIndex-1;
        }     
        this.refreshGrid(data);
       }
      );
      this.snackBar.open('Successfully deleted the Vehicle item', '', {
       duration: 3000
      }); 
     }
     });
   }
   
   edit(item:VehicleDto){
    const dialogRef = this.dialog.open(VehicleItemComponent, { 
     disableClose:true,
     data: item,
     autoFocus:true
    });
  ​
    dialogRef.afterClosed().subscribe(result => {
     if(result == true)
     {
       this.vehicleService.getVehicle().subscribe(data=>{this.refreshGrid(data);})
     }
     });
   }
   }
  ​
