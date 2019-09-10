import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog, MatSnackBar } from '@angular/material';
import { VehicleTypeService } from './vehicle-type.service';
import { VehicleTypeDto } from './VehicleTypeDto';
import { VehicleTypeItemComponent } from '../vehicle-type-item/vehicle-type-item.component';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-vehicle-type',
  templateUrl: './vehicle-type.component.html',
  styleUrls: ['./vehicle-type.component.css']
})
export class VehicleTypeComponent implements OnInit {

  displayedColumns=['id', 'name', 'description','edit', 'delete'];
  vehicleTypeList :MatTableDataSource<VehicleTypeDto>;
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  messageDto: MessageDialogDto;
  filterValue:string;

  constructor(private vehicleType:VehicleTypeService, public dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    let s:VehicleTypeDto=new VehicleTypeDto();
    this.vehicleType.getVehicleType().subscribe(data=>
      {
        this.refreshGrid(data);
      });
  }
  
  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.vehicleTypeList.filter = filtetString;
  }

  refreshGrid(data:VehicleTypeDto[]){
        this.vehicleTypeList = new MatTableDataSource(data);
    this.vehicleTypeList.sort = this.sort;
        this.vehicleTypeList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };
        this.vehicleTypeList.paginator = this.paginator;
    this.applyFilter();
  }

  add(){
    const dialogRef = this.dialog.open(VehicleTypeItemComponent, {
      disableClose:true,
      autoFocus:true
    });

    dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
       this.vehicleType.getVehicleType().subscribe(data=>{this.refreshGrid(data);})
    }
    });
  }

  delete(item:VehicleTypeDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Vehicle Type Delete Confirmation";
        this.messageDto.messageBody = "This " + item.name + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
     
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.vehicleType.delete(item.id).subscribe(
          data=>{
            var maxPageIndex =  data.length / this.paginator.pageSize; 
            if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
              this.paginator.pageIndex = maxPageIndex-1;
            }
            this.refreshGrid(data);
          });
        this.snackBar.open('Successfully deleted the vehicle type', '', {
          duration: 2000
        });

      }});
    } 
  
  edit(item:VehicleTypeDto){
    const dialogRef = this.dialog.open(VehicleTypeItemComponent, { 
      data: item,
      disableClose:true,
      autoFocus:true

    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.vehicleType.getVehicleType().subscribe(data=>{this.refreshGrid(data);})
      }
      });
  }
  }
