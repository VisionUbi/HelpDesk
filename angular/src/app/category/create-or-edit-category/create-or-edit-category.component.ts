import { Component, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryDto, CategoryServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-create-or-edit-category',
  templateUrl: './create-or-edit-category.component.html',
  styleUrl: './create-or-edit-category.component.css'
})
export class CreateOrEditCategoryComponent extends AppComponentBase{
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  categorys: CategoryDto = new CategoryDto();
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  active = false;
  saving = false;
  descriptions: { name: string; value: string }[] = [
    { name: 'IT',value: 'IT' },
    { name: 'Maintainance',value: 'Maintainance' },
  ];
  selectedDescription : string = '';
  constructor(
     private categoryServiceProxy: CategoryServiceProxy,
     injector: Injector,

    ) {
      super(injector);
  }
  show(categoryId?: number): void {
    
    if (!categoryId) { 
        this.categorys.id = categoryId;            
                
        this.active = true;
        this.modal.show();
    } else {
        this.categoryServiceProxy.getCategoryForEdit(categoryId).subscribe(result => {
            this.categorys = result;
            this.active = true;
            this.modal.show();
        });
    }                      
                
}
save(): void {
  this.saving = true; 
  this.categorys.description = this.selectedDescription
  this.categoryServiceProxy.createOrEdit(this.categorys)
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
    this.categorys.name = '';
}

}
