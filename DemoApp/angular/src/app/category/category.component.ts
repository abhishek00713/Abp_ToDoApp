import { ListService, LIST_QUERY_DEBOUNCE_TIME, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CategoryDto, GetCategoryListDto } from '../proxy/category-dtos';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { CategoryService } from '../proxy/app-services';
import { GetTaskListDto } from '../proxy/task-dtos';

@Component({
  providers: [

    ListService,


    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  searchedKeyword: string = null;

  category = { items: [], totalCount: 0 } as PagedResultDto<CategoryDto>;
  closeResult = '';
  form: FormGroup;
  selectedCategory = {} as CategoryDto;
  isModalOpen = false;




  constructor(
    private modalService: NgbModal,
    public readonly list: ListService,
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService

  ) {

  }

  clear() {
    this.searchedKeyword = '';
    this.list.get();
  }

  search() {
    console.log("searched", this.searchedKeyword);
    this.form = this.fb.group({
      filter: [this.searchedKeyword, Validators.required],

    });
 
    this.categoryService.getList(this.form.value).subscribe((response) => {
     
      this.form.reset();
      this.category = response;
      
    });
  }



  setPage(pageInfo) {

      const statusStreamCreator = (query) => this.categoryService.getList(query);



      this.list.hookToQuery(statusStreamCreator).subscribe((response) => {

        this.category = response;




      });
   
    
 

    


   
  }

  ngOnInit() {
   
   
      this.setPage({ offset: 0, limit: 10 });
  
    


  }

  createCategory() {
    this.selectedCategory = {} as CategoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editCategory(id: string) {
    this.categoryService.get(id).subscribe((category) => {
      this.selectedCategory = category;
      this.buildForm();
      this.isModalOpen = true;

    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.categoryService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  buildForm() {
    this.form = this.fb.group({
      categoryName: [this.selectedCategory.categoryName, Validators.required],

    });

  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedCategory.id) {
      this.categoryService
        .update(this.selectedCategory.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {

      this.categoryService.createASyncByInput(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

}
