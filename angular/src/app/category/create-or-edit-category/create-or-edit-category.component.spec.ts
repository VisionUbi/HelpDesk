import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditCategoryComponent } from './create-or-edit-category.component';

describe('CreateOrEditCategoryComponent', () => {
  let component: CreateOrEditCategoryComponent;
  let fixture: ComponentFixture<CreateOrEditCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateOrEditCategoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateOrEditCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
