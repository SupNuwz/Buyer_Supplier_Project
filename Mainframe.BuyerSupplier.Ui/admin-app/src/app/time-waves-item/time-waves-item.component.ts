import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { TimeWavesService } from '../time-waves/time-waves.service'
import { TimeWavesDto } from '../time-waves/TimeWavesDto';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA, MatInput } from '@angular/material';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';

@Component({
  selector: 'app-time-waves-item',
  templateUrl: './time-waves-item.component.html',
  styleUrls: ['./time-waves-item.component.css']
})
export class TimeWavesItemComponent implements OnInit {
  waveType : TimeWavesDto;
  header:string;
  wavesForm: FormGroup; 
 
  validationMessages = {                                                  
     'name': [{ type: 'required', message: 'Wave Name is required' }],
     'time': [{ type: 'required', message: 'Time is required' }],
     'description': [{ type: 'required', message: 'Description is required' }],
  }; 
 
  constructor(private router:Router, private timeWavesService:TimeWavesService,
      public dialogRef: MatDialogRef<TimeWavesItemComponent>,
      @Inject(MAT_DIALOG_DATA) public data: TimeWavesDto,
      public snackBar: MatSnackBar, private formBuilder: FormBuilder)

  { 
    if(data)
    {
      this.waveType = data;
      this.header= "Edit a Time Wave item";
    }
    else{
      this.waveType = new TimeWavesDto();
      this.header= "Add a Time Wave item";
    }
  }
 
  @ViewChild('wavename') WavenameInput: MatInput;
 
  ngOnInit() {
    this.wavesForm = this.formBuilder.group({                       
      name: [this.waveType.name, [Validators.required]],
      time: [this.waveType.time, [Validators.required]],
      description: [this.waveType.description, [Validators.required]],
    });

    this.WavenameInput.focus();

  }

  save(){
    this.timeWavesService.getInventoryItemAvailability(this.waveType.name, this.waveType.id).subscribe(data => {
      if(data==true){
        alert("This time wave item is already available in the time wave list");
      }  
      else{
        this.saveData();
      }      
    });    
  }
   
  saveData() {
     //Update
     if(this.waveType.id > 0)
     {
       this.timeWavesService.editWaves(this.waveType)
         .subscribe(() => {
           this.dialogRef.close(true);  
           this.snackBar.open('Successfully updated the time wave', '', {
             duration: 2000
           });
 
         });
     }
 
     // Insert
     else{
       this.timeWavesService.addTimeWaves(this.waveType)
        .subscribe(() => {
            this.dialogRef.close(true);    
            this.snackBar.open('Successfully added a new time wave', '', {
             duration: 2000    
            });
        });
    }
  }
}


