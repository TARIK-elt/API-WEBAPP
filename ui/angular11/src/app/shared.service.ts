import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly apiUrl="http://localhost:50951/api";
readonly photoURL="http://localhost:50951/photos";

  constructor(private http:HttpClient) { }
  

  getDepList():Observable<any[]>{
    return this.http.get<any>(this.apiUrl+'/Department');
  }
  addDepartment(val:any){
    return this.http.post(this.apiUrl+'/Department',val);
  }
  updateDepartment(val:any){
    return this.http.put(this.apiUrl+'/Department',val);
  }
  deleteDepartment(val:any){
    return this.http.delete(this.apiUrl+'/Department/'+val);
  }
 

  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.apiUrl+'/Employee');
  }
  addEmployee(val:any){
    return this.http.post(this.apiUrl+'/Employee',val);
  }
  updateEmployee(val:any){
    return this.http.put(this.apiUrl+'/Employee',val);
  }
  deleteEmployee(val:any){
    return this.http.delete(this.apiUrl+'/Employee/'+val);
  } 


  uploadPhoto(val:any){
    return this.http.post(this.apiUrl+'/Employee/SaveFile/',val);
  }
  getAllDepartmentNames():Observable<any[]>{
    return this.http.get<any[]>(this.apiUrl+'/Employee/GetAllDepartmentNames');
  }
}
