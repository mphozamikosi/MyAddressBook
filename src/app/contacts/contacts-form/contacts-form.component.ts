import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Contacts } from 'src/app/shared/contacts.model';
import { ContactsService } from 'src/app/shared/contacts.service';

@Component({
  selector: 'app-contacts-form',
  templateUrl: './contacts-form.component.html',
  styles: [
  ]
})
export class ContactsFormComponent implements OnInit {

  constructor(public service: ContactsService, private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    if(this.service.formData.id == 0){
      this.insertContact(form);
    }else{
      this.updateContact(form);
    }
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData =new Contacts();
  }

  insertContact(form:NgForm){
    this.service.postContactDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.service.openSpinner();
        this.toastr.success('Contact created successfully!', 'Contact registration');
      },
      err => {
        console.log(err);      
        this.toastr.error('Contact not created. Please try again!', 'Contact registration');
      }
    );
  }
  updateContact(form:NgForm){
    this.service.putContactDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Contact updated successfully!', 'Contact update');
      },
      err => {
        console.log(err);      
        this.toastr.error('Contact not updated. Please try again!', 'Contact update');
      }
    );
  }

  isLoading = false;
  
  toggleLoading = () =>{
    this.isLoading = true;
    setTimeout(()=>{
      this.isLoading = false;
    }, 3000)
  }
}
