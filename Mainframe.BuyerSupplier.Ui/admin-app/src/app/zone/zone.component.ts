import { Component, OnInit, ViewChild } from '@angular/core';
import { ZoneDto } from './ZoneDto';
import{ ZoneService } from './zone.service';
import { ZoneItemComponent } from '../zone-item/zone-item.component';
import {MatDialog,MatSort, MatTableDataSource, MatPaginator,MatSnackBar} from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-zone',
  templateUrl: './zone.component.html',
  styleUrls: ['./zone.component.css']
})
export class ZoneComponent implements OnInit {

  displayedColumns= ['id', 'supplierBaseID', 'name','description','edit','delete'];
  zoneList :MatTableDataSource<ZoneDto>;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue: string;
  messageDto: any;
  
  applyFilter() {
   this.zoneList.filter = this.filterValue; 
  }
  constructor(private zone:ZoneService ,public dialog: MatDialog,public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.zone.getZone().subscribe(data=>
      {
       this.refreshGrid(data);
      });
  }

  refreshGrid(data:ZoneDto[]){
    this.zoneList = new MatTableDataSource(data);
    this.zoneList.sort = this.sort;
     
     this.zoneList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
      if (typeof data[sortHeaderId] === 'string') {
       return data[sortHeaderId].toLocaleLowerCase();
      }
     
      return data[sortHeaderId];
     };
     this.zoneList.paginator = this.paginator;
     this.applyFilter();
    }; 

    add(){
      const dialogRef = this.dialog.open(ZoneItemComponent, {
       disableClose:true,
       autoFocus:true
      });
    ​
      dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.zone.getZone().subscribe(data=>{this.refreshGrid(data);})
      }
      });
     }
     
     delete(item:ZoneDto){
      this.messageDto = new MessageDialogDto();
          this.messageDto.messageCaption = "Zone Delete Confirmation";
          this.messageDto.messageBody = "This " + item.supplierBaseName + " may be used in other transactions. Do you still want to delete it?";
          
          const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
            disableClose:true,
            data : this.messageDto
          });
       
      dialogRef.afterClosed().subscribe(result => {
        if(result == true)
        {
          this.zone.delete(item.id).subscribe(
            data=>{
              var maxPageIndex =  data.length / this.paginator.pageSize; 
              if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
                this.paginator.pageIndex = maxPageIndex-1;
              }
              this.refreshGrid(data);
            });
          this.snackBar.open('Successfully deleted the zone', '', {
            duration: 2000
          });
  
        }});
      }    

 edit(item:ZoneDto){
  const dialogRef = this.dialog.open(ZoneItemComponent, { 
   disableClose:true,
   data: item
  });
​
  dialogRef.afterClosed().subscribe(result => {
   if(result == true)
   {
     this.zone.getZone().subscribe(data=>{this.refreshGrid(data);})
   }
   });
 }
}
