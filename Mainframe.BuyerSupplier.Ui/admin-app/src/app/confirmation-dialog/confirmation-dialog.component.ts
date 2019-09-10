import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MessageDialogDto } from './MessageDialogDto';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {

  messageCaption:string;
  messageBody:string;

  constructor(public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MessageDialogDto) { 
      this.messageCaption= data.messageCaption;
      this.messageBody= data.messageBody;
    }

  ngOnInit() {
  }

  Confirm():void{
    this.dialogRef.close(true);

    }

}
