import { Component, Injector, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDepartmentComponent } from './create-or-edit-department/create-or-edit-department.component';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent extends AppComponentBase{
  @ViewChild('createOrEditProjectFileModal', { static: true }) createOrEditProjectFileModal: CreateOrEditDepartmentComponent;
  createDepartmentForm: FormGroup;
  departments: DepartmentDto[];
  showModal = false;
  newDepartmentName: string = '';
  constructor(
    injector: Injector,
     private fb: FormBuilder,
     private departmentServiceProxy: DepartmentServiceProxy,

    ) {        super(injector);}
    createProjectFile(): void {
      this.createOrEditProjectFileModal.show();   
  }

   
  ngOnInit(): void {
    this.getDepartments();
    this.createDepartmentForm = this.fb.group({
      name: ['', Validators.required]
    });
  }
  getDepartments() {
    this.departmentServiceProxy.getAllDepartments().subscribe((result) => {
      this.departments = result;
    })
  }
 
  deleteDepartment(id: number): void {
    this.message.confirm(
        '',
        this.l('AreYouSure'),
        (isConfirmed) => {
            if (isConfirmed) {
                this.departmentServiceProxy.deleteDepartment(id)
                    .subscribe(() => {
                      this.getDepartments();
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        }
    );
}
  resetFilters(): void {
    this.newDepartmentName = '';
    this.departments= undefined;
    this.getDepartments();
}
}
