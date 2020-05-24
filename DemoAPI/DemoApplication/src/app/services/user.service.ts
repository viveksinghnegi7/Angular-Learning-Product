import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { map, catchError, retry } from "rxjs/operators";

import { environment } from "../../environments/environment";
import { Users } from "../models/Users"; 
import { FormGroup, FormControl, Validators } from "@angular/forms";
import * as _ from 'lodash';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  form: FormGroup = new FormGroup({
    userId: new FormControl(null),
    email: new FormControl('', Validators.email), 
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),

    //mobile: new FormControl('', [Validators.required, Validators.minLength(8)]),
    //city: new FormControl(''),
    //gender: new FormControl('1'),
    //department: new FormControl(0),
    //hireDate: new FormControl(''),
    //isPermanent: new FormControl(false)
  });

  initializeFormGroup() {
    this.form.setValue({
      userId: '',
      email: '',
      firstName: '',
      lastName: '', 
      //mobile: '',
      //city: '',
      //gender: '1',
      //department: 0,
      //hireDate: '',
      //isPermanent: false
    });
  }
  populateForm(user) {
    this.form.setValue(_.omit(user, 'password', 'passwordHash','passwordSalt'));
  }
  getAllUsers(): Observable<Users[]> {
    return this.httpClient.get<Users[]>(environment.baseUrl + 'users/list', this.httpOptions);
  }
   
  // DELETE
  deleteUser(id: any){
    console.log("in");
    return this.httpClient.delete<any>(`${environment.baseUrl}users/delete/${id}`, this.httpOptions).subscribe()
  }
  // Error handling
  errorHandl(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
