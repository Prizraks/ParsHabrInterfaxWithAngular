import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders,HttpParams } from '@angular/common/http';

@Injectable()
export class LoginServices {
    constructor(private http: HttpClient) {

    }
    getNews() {
        return this.http.get('http://localhost:5000/api/news/get');
    }
    filterSort(data:any){
        return this.http.post('http://localhost:5000/api/filterSort/get', data);
    }
    //   getEvents(id:string){
    //     const params = new HttpParams().set('idUser', id);
    //     return this.http.get('http://localhost:5000/api/event/get',{params});
    //   }
}