import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderOptimizedPossibilityDto } from '../OrderOptimizedPossibilityDto';
import { MatTableDataSource } from '@angular/material';
import { OrderOptimizedDetailDto } from '../OrderOptimizedDetailDto';

@Component({
  selector: 'app-order-possibility-item',
  templateUrl: './order-possibility-item.component.html',
  styleUrls: ['./order-possibility-item.component.css']
})
export class OrderPossibilityItemComponent implements OnInit {
  @Input() orderOptimizedPossibilityDto:OrderOptimizedPossibilityDto;
  @Output() SelectOrderOptimization = new EventEmitter();

  displayedColumns = ['itemName','uom','qty','unitPrice','value'];
  orderOptimizedDetailsData:MatTableDataSource<OrderOptimizedDetailDto>;

  constructor() { }

  ngOnInit() {
    this.orderOptimizedDetailsData = new MatTableDataSource(this.orderOptimizedPossibilityDto.orderOptimizedDetails);
  }

  SelectButtonClick(){
    this.SelectOrderOptimization.emit(this.orderOptimizedPossibilityDto);
    
  }

}
