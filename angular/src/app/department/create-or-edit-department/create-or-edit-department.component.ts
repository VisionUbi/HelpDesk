import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-create-or-edit-department',
  templateUrl: './create-or-edit-department.component.html',
  styleUrl: './create-or-edit-department.component.css'
})
export class CreateOrEditDepartmentComponent extends AppComponentBase{
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  departments: DepartmentDto = new DepartmentDto();
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  active = false;
  saving = false;

  constructor(
     private departmentServiceProxy: DepartmentServiceProxy,
     injector: Injector,

    ) {
      super(injector);
  }
  show(departmentId?: number): void {
    
    if (!departmentId) { 
        this.departments.id = departmentId;            
                
        this.active = true;
        this.modal.show();
    } else {
        this.departmentServiceProxy.getdepartmentForEdit(departmentId).subscribe(result => {
            this.departments = result;
            this.active = true;
            this.modal.show();
        });
    }                      
                
}
save(): void {
  this.saving = true; 

  this.departmentServiceProxy.createOrEdit(this.departments)
   .pipe(finalize(() => { this.saving = false;}))
   .subscribe(() => {
      this.notify.info(this.l('SavedSuccessfully'));
      this.close();
      this.modalSave.emit(null);
   });
}
  close(): void {
    this.active = false;
    this.modal.hide();
    this.departments.name = '';
}

}
