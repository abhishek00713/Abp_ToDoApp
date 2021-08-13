import { ListService, LIST_QUERY_DEBOUNCE_TIME, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { GetTaskListDto, TaskDto } from '../proxy/task-dtos';
import { TaskService } from '../proxy/app-services';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { HttpClient, HttpParams } from '@angular/common/http';
@Component({
  providers: [

    ListService,


    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {


  searchedKeyword: string;

  task = { items: [], totalCount: 0 } as PagedResultDto<TaskDto>;
  closeResult = '';
  form: FormGroup;
  selectedTask = {} as TaskDto;
  isModalOpen = false;


  constructor(private modalService: NgbModal,
    public readonly list: ListService,
    private taskService: TaskService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService

  ) {

  }

  setPage(pageInfo) {
   


      const taskStreamCreator = (query) => this.taskService.getList(query);


    

      this.list.hookToQuery(taskStreamCreator).subscribe((response) => {

        this.task = response;
        console.log("tttaa", response);
      });

  


  }
  ngOnInit() {

    this.setPage({ offset: 0, limit: 10 });

 

  }

  createTask() {
    this.selectedTask = {} as TaskDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editTask(id: string) {
    this.taskService.get(id).subscribe((task) => {
      this.selectedTask = task;
      this.buildForm();
      this.isModalOpen = true;

    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.taskService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  buildForm() {
    this.form = this.fb.group({
      taskName: [this.selectedTask.taskName, Validators.required],

    });

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

    this.taskService.getList(this.form.value).subscribe((response) => {

      this.form.reset();
      this.task = response;

    });
  }



  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedTask.id) {
      this.taskService
        .update(this.selectedTask.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {

      this.taskService.createASyncByInput(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

}
