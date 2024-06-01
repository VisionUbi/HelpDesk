import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryDto, CategoryServiceProxy, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  createDepartmentForm: FormGroup;
  categorys: CategoryDto[];
  newDepartmentName: string = '';
  constructor(
    private modalService: NgbModal,
     private fb: FormBuilder,
     private categoryServiceProxy: CategoryServiceProxy,

    ) {}
  openCreateModal(content: any) {
    this.modalService.open(content, { centered: false });
  }

  ngOnInit(): void {
    this.getDepartments();
    this.createDepartmentForm = this.fb.group({
      name: ['', Validators.required]
    });
  }
  getDepartments() {
    this.categoryServiceProxy.getAllCategorys().subscribe((result) => {
      this.categorys = result;
    })
  }

  
}
