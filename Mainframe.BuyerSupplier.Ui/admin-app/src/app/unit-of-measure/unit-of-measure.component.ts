import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog, MatSnackBar } from '@angular/material';
import { UnitofmeasureService } from './unitofmeasure.service';
import { UnitOfMeasureDto } from './UnitOfMeasureDto';
import { UnitOfMeasureItemComponent } from '../unit-of-measure-item/unit-of-measure-item.component';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-unit-of-measure',
  templateUrl: './unit-of-measure.component.html',
  styleUrls: ['./unit-of-measure.component.css']
})
export class UnitOfMeasureComponent implements OnInit {

  displayedColumns=['id', 'name', 'description','edit', 'delete'];
  unitOfMeasureList :MatTableDataSource<UnitOfMeasureDto>;
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  messageDto: MessageDialogDto;
  filterValue:string;

  constructor(private unitOfMeasure:UnitofmeasureService, public dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    let s:UnitOfMeasureDto=new UnitOfMeasureDto();
    this.unitOfMeasure.getUnitOfMeasure().subscribe(data=>
      {
        this.refreshGrid(data);
      });
  }
  
  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.unitOfMeasureList.filter = filtetString;
  }

  refreshGrid(data:UnitOfMeasureDto[]){
        this.unitOfMeasureList = new MatTableDataSource(data);
    this.unitOfMeasureList.sort = this.sort;
        this.unitOfMeasureList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };
        this.unitOfMeasureList.paginator = this.paginator;
    this.applyFilter();
  }

  add(){
    const dialogRef = this.dialog.open(UnitOfMeasureItemComponent, {
      disableClose:true,
      autoFocus:true
    });

    dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
       this.unitOfMeasure.getUnitOfMeasure().subscribe(data=>{this.refreshGrid(data);})
    }
    });
  }

  delete(item:UnitOfMeasureDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Unit of Measure Delete Confirmation";
        this.messageDto.messageBody = "This " + item.name + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
     
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.unitOfMeasure.delete(item.id).subscribe(
          data=>{
            var maxPageIndex =  data.length / this.paginator.pageSize; 
            if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
              this.paginator.pageIndex = maxPageIndex-1;
            }
            this.refreshGrid(data);
          });
        this.snackBar.open('Successfully deleted the unit of measure', '', {
          duration: 2000
        });

      }});
    } 
  
  edit(item:UnitOfMeasureDto){
    const dialogRef = this.dialog.open(UnitOfMeasureItemComponent, { 
      data: item,
      disableClose:true,
      autoFocus:true

    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.unitOfMeasure.getUnitOfMeasure().subscribe(data=>{this.refreshGrid(data);})
      }
      });
  }
  }
