import { Component, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryDto, CategoryServiceProxy, DepartmentDto, DepartmentServiceProxy, TicketDto, TicketServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrl: './create-ticket.component.css'
})
export class CreateTicketComponent extends AppComponentBase{
  categorys:  { name: string; value: number }[] = [];
  ticket: TicketDto = new TicketDto();
  departments: { name: string; value: number }[] = [];
  selectedDepartment: number = undefined;
  selectedCategory: number = undefined;
  active: boolean = false;
  descriptions: { name: string; value: string }[] = [
    { name: 'IT',value: 'IT' },
    { name: 'Maintainance',value: 'Maintainance' },
  ];
  selectedDescription : string = '';
  constructor(
    public bsModalRef: BsModalRef,
    private categoryServiceProxy: CategoryServiceProxy,
    private departmentServiceProxy: DepartmentServiceProxy,
    private ticketServiceProxy: TicketServiceProxy,
    injector: Injector,
    private _router: Router
    ) {
      super(injector);
  }
  hideModal() {
    this.bsModalRef.hide();
  }
  createTicekt() {  
    this.ticket.departmentType = this.selectedDescription;
    this.ticket.departmentId = this.selectedDepartment;
    this.ticket.categoryId = this.selectedCategory;
    this.ticketServiceProxy.createCategory(this.ticket).subscribe((result) => {
      
      this.notify.success(this.l('Ticket Created Successfully'));
      this._router.navigate(['/app/my-ticket']);
      this.resetFilters();
    })
  }
  resetFilters() {
    this.ticket = new TicketDto();
    this.selectedDepartment = undefined;
    this.selectedCategory = undefined;
    this.active = false;
    this.selectedDescription = undefined;
  }
  getDepartments(selected :string) {
    this.departmentServiceProxy.getAllDepartments(selected).subscribe((result: DepartmentDto[]) => {
      this.departments = result.map(department => ({
        name: department.name,
        value: department.id
      }));
    });
  }
  onchange(selected :string){
    if(selected != ''){
      this.active= true;
      this.selectedDescription = selected;
      this.getCategory(selected);
      this.getDepartments(selected);
    }

  }
  getCategory(selected :string) {
    this.categoryServiceProxy.getAllCategorys(selected).subscribe((result) => {
      this.categorys = result.map(category => ({
        name : category.name,
        value: category.id
      }));
    })
  }
  onSubmit() {
    this.hideModal();
  }
  ngOnInit(): void {
    this.getDepartments(this.selectedDescription);
    this.getCategory(this.selectedDescription);

  }
 

}
