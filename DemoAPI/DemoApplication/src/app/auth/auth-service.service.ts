import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from "rxjs";
import { baseUrl } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http: HttpClient) { }
  login(data): Observable<any> {
    console.log(data);
    return this.http.post(baseUrl+'authenticate', data);
  }
}
