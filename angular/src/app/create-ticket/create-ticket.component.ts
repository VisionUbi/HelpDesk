import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrl: './create-ticket.component.css'
})
export class CreateTicketComponent {
  data: any = {};
  constructor(public bsModalRef: BsModalRef) {}
  hideModal() {
    this.bsModalRef.hide();
  }

  onSubmit() {
    // Handle form submission, e.g., call a service to save the data
    console.log(this.data);
    this.hideModal();
  }
   
}
