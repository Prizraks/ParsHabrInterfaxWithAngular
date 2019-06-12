import { Component,OnInit, ViewChild } from '@angular/core';
import {FormGroup,FormControl, Validators,FormBuilder} from '@angular/forms';
import {MatPaginator, MatTableDataSource} from '@angular/material';
import {LoginServices} from './/services/services';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  formData: FormGroup;
  submitted=false;

  constructor(private router:Router,private services:LoginServices,private formBuilder:FormBuilder){}

  ngOnInit(){
    this.formData=this.formBuilder.group({
      sourceName:['',Validators.required],
      sort:['',Validators.required]
    })     
    this.dataSource.paginator = this.paginator;
    this.loadAllNews();
  }

  FilterSort(){
    this.submitted=true;
    if(this.formData.valid)
    {
      console.log("ff", this.formData.value);
       const data={
        sourceName: this.formData.value.sourceName,
        sort: this.formData.value.sort
      }
      this.services.filterSort(data).subscribe(
        (res: any) =>{this.dataSource.data=res})
        this.submitted=false;
    }

  }
  loadAllNews(){
    this.services.getNews().subscribe((data:any[])=>{this.dataSource.data=data})
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['sourceName', 'name', 'datePubl','timePubl', 'descr'];
  dataSource= new MatTableDataSource();
  
}