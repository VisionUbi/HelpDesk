import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent {
  createDepartmentForm: FormGroup;
  departments: DepartmentDto[];
  newDepartmentName: string = '';
  constructor(
    private modalService: NgbModal,
     private fb: FormBuilder,
     private departmentServiceProxy: DepartmentServiceProxy,

    ) {}
    openCreateModal(content: any) {
      const modalRef = this.modalService.open(content, { centered: true });
      // Optionally, add a listener to close the modal when clicking outside
      modalRef.result.then(() => {}, () => {});
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
  createDepartment() {
    this.departmentServiceProxy.createDepartment(this.newDepartmentName).subscribe((result) => {
      this.getDepartments();
    }, error => {
      console.error(error);
    }).add(() => {
      this.modalService.dismissAll(); // Ensure this is called after successful operation
    });
  }
  
}
