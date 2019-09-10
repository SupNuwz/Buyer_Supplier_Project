import { Component, OnInit,Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialog, MatSort, MatTableDataSource, MatPaginator, MatSnackBar } from '@angular/material';
import { SupplierBaseManagementService } from '../supplierBaseManagement/supplier-base-management.service';
import { SupplierBaseDto } from '../supplierBaseManagement/supplierBaseDto';
import { WatchListService } from '../watch-list/watch-list.service';
import { WatchListDto } from '../watch-list/WatchListDto';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-watch-list',
  templateUrl: './watch-list.component.html',
  styleUrls: ['./watch-list.component.css']
})

export class WatchListComponent implements OnInit {
  displayedColumns = ['supplierName', 'standardInventoryName', 'quantityAvailable', 'price'];
  watchListList :MatTableDataSource<WatchListDto>;
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  filterValue:string; 

  watchListItem:WatchListDto;
  supplierBaseList:SupplierBaseDto[];
  watchListForm: FormGroup;   

  selectedBaseId;
  
  changeSupplierBase(event){ 
    console.log(event);
  }


  constructor(private watchList: WatchListService,
              private supplierBaseManagement:SupplierBaseManagementService, 
              private formBuilder: FormBuilder ){}

  ngOnInit() {
    
    this.supplierBaseManagement.getSuppliers().subscribe(data => {
       this.supplierBaseList = data;
    });

    this.watchListForm = this.formBuilder.group({                                       
       supplierBaseControl: [,],
    });    

  }

  getWatchlistData(selectedBaseId){
    this.watchList.getWatchList(selectedBaseId).subscribe(data=>
      {
        this.refreshGrid(data);
      });
  }

  refreshGrid(data:WatchListDto[]){
    this.watchListList = new MatTableDataSource(data);
    this.watchListList.sort = this.sort;
    this.watchListList.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
      if (typeof data[sortHeaderId] === 'string') {
        return data[sortHeaderId].toLocaleLowerCase();
      }
      
      return data[sortHeaderId];
    };
    this.watchListList.paginator = this.paginator;
    this.applyFilter();
  }
  
  applyFilter(){
    let filterString="";

    if(this.filterValue)
      filterString = this.filterValue.trim().toLowerCase(); 
      this.watchListList.filter = filterString;
  }
  
}    
  