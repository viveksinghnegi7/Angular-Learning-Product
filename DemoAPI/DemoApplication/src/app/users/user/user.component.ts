import { Component, OnInit } from '@angular/core';  
import { UserService } from '../../services/user.service';
import { MatDialogRef } from '@angular/material/dialog';
 
//import { NotificationService } from '../../shared/notification.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(public service: UserService,
    //private departmentService: DepartmentService,
    //private notificationService: NotificationService,
    public dialogRef: MatDialogRef<UserComponent>) { }

  ngOnInit(): void {
    this.service.getAllUsers(); 
  } 

  onClear() {
    this.service.form.reset();
    this.service.initializeFormGroup();
    //this.notificationService.success(':: Submitted successfully');
  }

  onSubmit() {
    if (this.service.form.valid) {
      if (!this.service.form.get('userId').value)
        this.service.insertUser(this.service.form.value).subscribe();
      else
        this.service.updateUser(this.service.form.value).subscribe();
      this.service.form.reset();
      this.service.initializeFormGroup();
      //this.notificationService.success(':: Submitted successfully');
      this.onClose();
    }
  }

  onClose() {
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.dialogRef.close();
  }

}

