import { Component, OnInit } from '@angular/core';
import { Contacts } from '../shared/contacts.model';
import { ContactsService } from '../shared/contacts.service';
import { ToastrService } from 'ngx-toastr';
import { ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styles: [
  ]
})

export class ContactsComponent implements OnInit {

  searchText: any;
  p: number = 1;
  csvRecords: Contacts[] = [];
  header: boolean = true;
  data: any;
  constructor(public service: ContactsService, private toastr:ToastrService, private ngxCsvParser: NgxCsvParser) { }
  
  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Contacts){
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Do you wish to delete this contact?')){
      this.service.deleteContact(id).subscribe(
        res=>{
          this.toastr.success('Contact successfully deleted!', 'Contact delete');
          this.service.refreshList();
        },err=>{
          console.log(err);
          this.toastr.error('Contact not deleted! Please try again!', 'Contact update');
        }
      )
    }
  }

  insertContacts(form:any){
    this.service.postMultipleContactDetails(form).subscribe(
      res =>{
        //this.contactForm.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Contacts uploaded successfully!', 'Contacts registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Contact not uploaded. Check that your CSV file is closed and has the appropriate headers. Please try again!', 'Contacts registration');
      }
    );
  }

  Search(){
    if(this.searchText == ""){
      this.ngOnInit();
    }else{
      this.service.list = this.service.list.filter(res =>{
        if(res.firstName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase())){
          return res.firstName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
        }
        else if(res.surname.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase())){
          return res.surname.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
        }
        else if(res.tel.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase())){
          return res.tel.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
        }else{
          return res.firstName.toLocaleLowerCase().match(this.searchText.toLocaleLowerCase());
        }
        
      });
    }
  }

  key: string = 'firstName';
  reverse: boolean =false;
  sort(key: string){
    this.key =key;
    this.reverse = !this.reverse;
  }

  @ViewChild('fileImportInput') fileImportInput: any;

  fileChangeListener($event: any): void {

    const files = $event.srcElement.files;
    this.header = (this.header as unknown as string) === 'true' || this.header === true;

    this.ngxCsvParser.parse(files[0], { header: this.header, delimiter: ',' })
      .pipe().subscribe((result: any) => {
        console.log('Result', result);
        this.data = result;
        this.insertContacts(this.data);
        this.csvRecords = result;
      }, (error: NgxCSVParserError) => {
        console.log('Error', error);
      });
  }
}
