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

  searchedKeyword: string;

  status = { items: [], totalCount: 0 } as PagedResultDto<StatusDto>;
  closeResult = '';
  form: FormGroup;
  selectedStatus = {} as StatusDto;
  isModalOpen = false;
  statuslist = [];

  constructor(private modalService: NgbModal,
    public readonly list: ListService,
    private statusService: StatusService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService

  ) {
    
  }
  



  ngOnInit() {
    
  
   const statusStreamCreator = (query) => this.statusService.getList(query);
  
  
   this.list.hookToQuery(statusStreamCreator).subscribe((response) => {
     for (let i = 0; i < response.totalCount; i++) {


       this.statuslist.push({
         id: response.items[i].id,
         Name: response.items[i].statusName
       });

     }


      this.status = response;
    });
    console.log("list", this.list);
  }

  createStatus() {
    this.selectedStatus = {} as StatusDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editStatus(id: string) {
    this.statusService.get(id).subscribe((status) => {
      this.selectedStatus = status;
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
      statusName: [this.selectedStatus.statusName , Validators.required],
     
    });
    
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedStatus.id) {
      this.statusService
        .update(this.selectedStatus.id, this.form.value)
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


