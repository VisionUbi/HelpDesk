import { Component } from '@angular/core';
import { CategoryDto, CategoryServiceProxy, DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrl: './create-ticket.component.css'
})
export class CreateTicketComponent {
  data: any = {};
  categorys: CategoryDto[];
  departments: DepartmentDto[];
  constructor(public bsModalRef: BsModalRef, private categoryServiceProxy: CategoryServiceProxy, private departmentServiceProxy: DepartmentServiceProxy,) { }
  hideModal() {
    this.bsModalRef.hide();
  }
  getDepartments() {
    this.departmentServiceProxy.getAllDepartments().subscribe((result) => {
      this.departments = result;
    })
  }
  onSubmit() {
    // Handle form submission, e.g., call a service to save the data
    console.log(this.data);
    this.hideModal();
  }
  ngOnInit(): void {
    this.getDepartments();
    this.getCategory();

  }
  getCategory() {
    this.categoryServiceProxy.getAllCategorys().subscribe((result) => {
      this.categorys = result;
    })
  }

}
