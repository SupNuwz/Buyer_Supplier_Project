import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatDialog, MatPaginator,MatSnackBar } from '@angular/material';
import { UserDto } from './UserDto';
import { UserComponent } from '../user/user.component';
import { UserService } from './user.service';
import { MessageDialogDto } from '../confirmation-dialog/MessageDialogDto';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-users',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersComponent implements OnInit {

  displayedColumns=['id', 'name', 'address', 'contactNo','email','userType',
  'defaultSupplierBaseId','relevantZoneId','deliverySlotId','category','edit','delete'];
  usersList:MatTableDataSource<UserDto>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue :string;
  messageDto: any;
  
  applyFilter() {

    let filtetString="";

    if(this.filterValue)
        filtetString = this.filterValue.trim().toLowerCase(); 

        this.usersList.filter = filtetString;
  }
  constructor(private users:UserService,public dialog: MatDialog,public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.loadData();
  }
  loadData()
  {
    this.users.getuser().subscribe(data=>
      {
        this.usersList = new MatTableDataSource(data);
        this.usersList.sort = this.sort;

        this.usersList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
          if (typeof data[sortHeaderId] === 'string') {
            return data[sortHeaderId].toLocaleLowerCase();
          }
        
          return data[sortHeaderId];
        };

        this.usersList.paginator = this.paginator;
        this.applyFilter();
      }
    );
  }
  

add(){
  const dialogRef = this.dialog.open(UserComponent, {
    disableClose:true,
    autoFocus:true
  });

  dialogRef.afterClosed().subscribe(result => {
  if(result == true)
  {
     this.loadData();
  }
  });
  
}
edit(user:UserDto){
  const dialogRef = this.dialog.open(UserComponent, { 
    data: user,
    disableClose:true,
    autoFocus:true
  });

  dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
      this.loadData();
    }
    });
}

delete(user:UserDto){
  this.messageDto = new MessageDialogDto();
        this.messageDto.messageCaption = "User List Delete Confirmation";
        this.messageDto.messageBody = "This " + user.name + " may be used in other transactions. Do you still want to delete it?";
        
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { 
    disableClose:true,
          data : this.messageDto
  });
  
  dialogRef.afterClosed().subscribe(result => {
    if(result == true)
    {
      this.users.deleteUser(user.id).subscribe(
        data=>
        {
          var maxPageIndex =  data.length / this.paginator.pageSize; 
          if(!this.paginator.hasNextPage() && this.paginator.pageIndex == maxPageIndex){
            this.paginator.pageIndex = maxPageIndex-1;
          }
          this.loadData();
          this.snackBar.open('Successfully Deleted a User', '', {
        duration: 3000
      });
        });
      
    }
    });
}

}

