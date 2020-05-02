import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthServiceService } from "../auth/auth-service.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['login.component.css']
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  constructor(private authService:AuthServiceService) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = new FormGroup(
      {
        email: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]) 
      });
  }
  loginProcess() {
    if (this.formGroup.valid) {
      this.authService.login(this.formGroup.value).subscribe(result => {
        if (result.success) {
          var a = result.deserialize;
          console.log(a);
          console.log("Login Successful");

        } else {
          var b = result.deserialize();
          console.log(b);
          console.log("Login Not Successful");
        }
      });
    }
  }
}
