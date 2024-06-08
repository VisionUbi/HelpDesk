import { Component, Injector, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryDto, CategoryServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCategoryComponent } from './create-or-edit-category/create-or-edit-category.component';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent extends AppComponentBase{
  @ViewChild('createOrEditCategoryModal', { static: true }) createOrEditCategoryModal: CreateOrEditCategoryComponent;
  createcategoryForm: FormGroup;
  categorys: CategoryDto[];
  newcategoryName: string = '';
  descriptions: { name: string; value: string }[] = [
    { name: 'IT',value: 'IT' },
    { name: 'Maintainance',value: 'Maintainance' },
  ];
  selectedDescription : string = '';
  constructor(
    injector: Injector,
     private fb: FormBuilder,
     private categoryServiceProxy: CategoryServiceProxy,

    ) { super(injector);}
    createProjectFile(): void {
      this.createOrEditCategoryModal.show();   
  }

   
  ngOnInit(): void {
    this.selectedDescription = this.descriptions[0].value;
    this.getCategories(this.selectedDescription );
    this.createcategoryForm = this.fb.group({
      name: ['', Validators.required]
    });
  }
  getCategories(description : string): void {
    this.categoryServiceProxy.getAllCategorys(description).subscribe((result) => {
      this.categorys = result;
    })
  }
 
  deletecategory(id: number): void {
    this.message.confirm(
        '',
        this.l('AreYouSure'),
        (isConfirmed) => {
            if (isConfirmed) {
                this.categoryServiceProxy.deleteCategory(id)
                    .subscribe(() => {
                      this.getCategories(this.selectedDescription);
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        }
    );
}
  resetFilters(): void {
    this.newcategoryName = '';
    this.categorys= undefined;
    this.getCategories(this.selectedDescription);
}
 

  
}
