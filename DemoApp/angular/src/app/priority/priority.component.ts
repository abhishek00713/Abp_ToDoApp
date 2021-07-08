import { ListService, LIST_QUERY_DEBOUNCE_TIME, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { PriorityDto } from '../proxy/priority-dtos';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { PriorityService } from '../proxy/app-services';

@Component({
  providers: [

    ListService,


    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
  selector: 'app-priority',
  templateUrl: './priority.component.html',
  styleUrls: ['./priority.component.scss']
})
export class PriorityComponent implements OnInit {
 
  searchedKeyword: string;

  priority = { items: [], totalCount: 0 } as PagedResultDto<PriorityDto>;
  closeResult = '';
  form: FormGroup;
  selectedPriority = {} as PriorityDto;
  isModalOpen = false;


  constructor(private modalService: NgbModal,
    public readonly list: ListService,
    private priorityService: PriorityService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService

  ) {

  }


 ngOnInit() {
   const statusStreamCreator = (query) => this.priorityService.getList(query);
  
  
   this.list.hookToQuery(statusStreamCreator).subscribe((response) => {
  
     this.priority = response;
    });



  }

  createPriority() {
    this.selectedPriority = {} as PriorityDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editPriority(id: string) {
    this.priorityService.get(id).subscribe((priority) => {
      this.selectedPriority = priority;
      this.buildForm();
      this.isModalOpen = true;
      
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.priorityService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  buildForm() {
    this.form = this.fb.group({
      priorityName: [this.selectedPriority.priorityName , Validators.required],
     
    });
    
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedPriority.id) {
      this.priorityService
        .update(this.selectedPriority.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      
      this.priorityService.createASyncByInput(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }


}
