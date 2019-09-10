import { Component, OnInit, ViewChild } from '@angular/core';
import { OrderDto } from '../order/OrderDto ';
import { OrderService } from '../order/order.service';
import { MatDialog, MatSort, MatTableDataSource, MatPaginator, MatSnackBar } from '@angular/material';
import { DetailedOrderBookComponent } from '../detailed-order-book/detailed-order-book.component';
import { OrderDetailDto } from '../order/OrderDetailDto';
import { UserService } from '../Users-List/user.service';
import { UserDto } from '../Users-List/UserDto';

@Component({
  selector: 'app-order-book',
  templateUrl: './order-book.component.html',
  styleUrls: ['./order-book.component.css']
})
export class OrderBookComponent implements OnInit {
  displayedColumns: string[] = ['id','buyerId','orderRefNo','viewMore'];
  OrderBookList:any; 
  buyer: UserDto[];

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue:string;  

  constructor(private orderService:OrderService,public dialog: MatDialog,private userService:UserService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData(){
    this.orderService.getOrders().subscribe(data => {
      this.refreshGrid(data);
    });

    
  }

  applyFilter(){
    let filtetString="";

  if(this.filterValue)
      filtetString = this.filterValue.trim().toLowerCase(); 

      this.OrderBookList.filter = filtetString;
  }


  refreshGrid(data:OrderDto[]){
    this.OrderBookList = new MatTableDataSource(data);
  this.OrderBookList.sort = this.sort;

    this.OrderBookList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
      if (typeof data[sortHeaderId] === 'string') {
        return data[sortHeaderId].toLocaleLowerCase();
      }
    
      return data[sortHeaderId];
    };
    this.OrderBookList.paginator = this.paginator;
  this.applyFilter();
}

viewMore(order:OrderDto){
  const dialogRef = this.dialog.open(DetailedOrderBookComponent, {
    data:order,
    autoFocus:true,
    width:"500px",
    
    
  });
  
}

}
