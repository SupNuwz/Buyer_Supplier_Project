import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { TableDataSource } from './table-datasource';
​
​
​
// TODO: replace this with real data from your application
const EXAMPLE_DATA: TableItem[] = [
 {id: 1, name: 'Hydrogen'},
 {id: 2, name: 'Helium'},
 {id: 3, name: 'Lithium'},
 {id: 4, name: 'Beryllium'},
 {id: 5, name: 'Boron'},
 {id: 6, name: 'Carbon'},
 {id: 7, name: 'Nitrogen'},
 {id: 8, name: 'Oxygen'},
 {id: 9, name: 'Fluorine'},
 {id: 10, name: 'Neon'},
 {id: 11, name: 'Sodium'},
 {id: 12, name: 'Magnesium'},
 {id: 13, name: 'Aluminum'},
 {id: 14, name: 'Silicon'},
 {id: 15, name: 'Phosphorus'},
 {id: 16, name: 'Sulfur'},
 {id: 17, name: 'Chlorine'},
 {id: 18, name: 'Argon'},
 {id: 19, name: 'Potassium'},
 {id: 20, name: 'Calcium'},
 {id: 10, name: 'Neon'},
 {id: 11, name: 'Sodium'},
 {id: 12, name: 'Magnesium'},
 {id: 13, name: 'Aluminum'},
 {id: 14, name: 'Silicon'},
 {id: 15, name: 'Phosphorus'},
 {id: 16, name: 'Sulfur'},
 {id: 17, name: 'Chlorine'},
 {id: 18, name: 'Argon'},
 {id: 19, name: 'Potassium'},
 {id: 20, name: 'Calcium'},
];
​
export interface TableItem {
 name: string;
 id: number;
}
​
@Component({
 selector: 'app-table',
 templateUrl: './table.component.html',
 styleUrls: ['./table.component.css'],
})
​
​
export class TableComponent implements OnInit {
 @ViewChild(MatPaginator) paginator: MatPaginator;
 @ViewChild(MatSort) sort: MatSort;
 dataSource: TableDataSource;
​
 /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
 displayedColumns = ['id', 'name'];
​
 ngOnInit() {
  this.dataSource = new TableDataSource(EXAMPLE_DATA,this.paginator, this.sort);
 }
}
