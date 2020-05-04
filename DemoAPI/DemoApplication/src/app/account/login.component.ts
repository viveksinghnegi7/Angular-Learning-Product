import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthServiceService } from "../services/auth-service.service";
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['login.component.css']
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  returnUrl: string;
  submitted = false;
  constructor(private route: ActivatedRoute,
    private router: Router,
    private authService: AuthServiceService) {

    // redirect to home if already logged in
    if (this.authService.userValue) {
      this.router.navigate(['/']);
    }

  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = new FormGroup(
      {
        email: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]) 
      });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.formGroup.controls; }
  loginProcess() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.formGroup.invalid) {
      return;
    }

    if (this.formGroup.valid) { 
      this.authService.login(this.formGroup.value).subscribe(result => {
        if (result != null) {
          console.log(this.returnUrl);
          this.router.navigate([this.returnUrl]); 
        } else { 
          console.log("Login Not Successful");
        }
      });
    }
  }
}
