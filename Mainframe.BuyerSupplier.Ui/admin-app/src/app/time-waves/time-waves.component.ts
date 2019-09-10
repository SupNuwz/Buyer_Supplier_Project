import { Component, OnInit, ViewChild } from '@angular/core';
import { TimeWavesDto } from './TimeWavesDto';
import { TimeWavesService } from './time-waves.service';
import { TimeWavesItemComponent } from '../time-waves-item/time-waves-item.component'
import { MatDialog, MatSort, MatTableDataSource, MatPaginator, MatSnackBar } from '@angular/material';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-time-waves',
  templateUrl: './time-waves.component.html',
  styleUrls: ['./time-waves.component.css']
})
export class TimeWavesComponent implements OnInit {

  displayedColumns = ['id', 'name', 'time', 'description', 'edit', 'delete'];
  timeWavesList:MatTableDataSource<TimeWavesDto>;
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  messageDto: any;
  filterValue:string;

  constructor( private timeWaves:TimeWavesService, public dialog: MatDialog, 
    public snackBar: MatSnackBar) { }

  ngOnInit() {
    let s:TimeWavesDto=new TimeWavesDto();
    this.timeWaves.getTimeWaves().subscribe(data=>
      {
        this.refreshGrid(data);
      });
  } 
  
  applyFilter(){
    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.timeWavesList.filter = filtetString;
  }

  refreshGrid(data:TimeWavesDto[]){
        this.timeWavesList = new MatTableDataSource(data);
    this.timeWavesList.sort = this.sort;
        this.timeWavesList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };
        this.timeWavesList.paginator = this.paginator;
    this.applyFilter();
    }
  
  add(){
    
    const dialogRef = this.dialog.open(TimeWavesItemComponent, {
      disableClose:true,
      autoFocus:true
    });

    dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
        this.timeWaves.getTimeWaves().subscribe(data=>{this.refreshGrid(data);})
    }
    });
    
  }

  delete(type:TimeWavesDto){
    this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "Time Waves Delete Confirmation";
        this.messageDto.messageBody = "This '" + type.name + "' may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
          disableClose:true,
          data : this.messageDto
        });
    
    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
        this.timeWaves.delete(type.id).subscribe(data=>
        {
          var maxPageIndex =  data.length / this.paginator.pageSize; 
          if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
            this.paginator.pageIndex = maxPageIndex-1;
          }
          this.refreshGrid(data);
        });  
        this.snackBar.open('Successfully deleted the time wave', '', {
          duration: 2000
        });
      }
      });

  }
  
  edit(type:TimeWavesDto){
    const dialogRef = this.dialog.open(TimeWavesItemComponent, {  
      disableClose:true,
      data: type
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result == true)
      {
         this.timeWaves.getTimeWaves().subscribe(data=>{this.refreshGrid(data);})
      }
      });
  }
  }

