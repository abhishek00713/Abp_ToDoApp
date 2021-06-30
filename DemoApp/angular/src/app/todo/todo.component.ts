import { ListService, LIST_QUERY_DEBOUNCE_TIME, PagedResultDto } from '@abp/ng.core';
import { query } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { StatusService, TaskService } from '../proxy/app-services';
import { StatusDto } from '../proxy/status-dtos';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';




@Component({

  providers: [
    
    ListService,

  
    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})

export class TodoComponent implements OnInit {
  status = { items: [], totalCount: 0 } as PagedResultDto<StatusDto>;
  closeResult = '';
  form: FormGroup;
  selectedAuthor = {} as StatusDto;
  isModalOpen = false;
  

  constructor(private modalService: NgbModal,
    public readonly list: ListService,
    private statusService: StatusService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService

  ) {

  }
  



 ngOnInit() {
   const taskStreamCreator = (query) => this.statusService.getList(query);
  
  
   this.list.hookToQuery(taskStreamCreator).subscribe((response) => {
  
      this.status = response;
    });



  }

  createAuthor() {
    this.selectedAuthor = {} as StatusDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editAuthor(id: string) {
    this.statusService.get(id).subscribe((status) => {
      this.selectedAuthor = status;
      this.buildForm();
      this.isModalOpen = true;
      
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.statusService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  buildForm() {
    this.form = this.fb.group({
      statusName: [this.selectedAuthor.statusName , Validators.required],
     
    });
    
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedAuthor.id) {
      this.statusService
        .update(this.selectedAuthor.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      
      this.statusService.createASyncByInput(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

}


