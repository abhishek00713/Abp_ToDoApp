import { AuthService, ListService, LIST_QUERY_DEBOUNCE_TIME, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { CategoryService, PriorityService, StatusService, TaskService, ToDoService } from '../proxy/app-services';
import { TaskDto } from '../proxy/task-dtos';
import { ToDoDto } from '../proxy/to-do-dtos';

@Component({

  providers: [

    ListService,


    { provide: LIST_QUERY_DEBOUNCE_TIME, useValue: 500 },
  ],
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit{
  searchedKeyword: string;

  todo = { items: [], totalCount: 0 } as PagedResultDto<ToDoDto>;
  
  closeResult = '';
  form: FormGroup;
  selectedToDo = {} as ToDoDto;
  isModalOpen = false;
  logged_in_User: any; 
  //categorylist: string[] = [];
  categorylist = [];
  statuslist = [];
  prioritylist = [];
  tasklist = [];

  public dateValue: Date = new Date("05/16/2017 3:00 AM");

  // City Names
  City: any = ['Florida', 'South Dakota', 'Tennessee', 'Michigan']

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();

  }

  constructor(private oAuthService: OAuthService,
    private authService: AuthService,
    public readonly list: ListService,
    private todoService: ToDoService,
    private categoryService: CategoryService,
    private taskService: TaskService,
    private statusService: StatusService,
    private priorityService: PriorityService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private cdRef: ChangeDetectorRef

  ) { }


  ngOnInit() {
    


    console.log("before", this.categorylist);

    //Category List
    const categoryStreamCreator = (query) => this.categoryService.getList(query);
    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {
        

        this.categorylist.push({
          id: response.items[i].id,
          Name: response.items[i].categoryName
      });
        
      }
     
      console.log("loopcates", this.categorylist);
      
   
    });

    //Status List
    const statusStreamCreator = (query) => this.statusService.getList(query);
    this.list.hookToQuery(statusStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {
        

        this.statuslist.push({
          id: response.items[i].id,
          Name: response.items[i].statusName
        });

      }

      console.log("status", this.statuslist);


    });

    //Priority List
    const PriorityStreamCreator = (query) => this.priorityService.getList(query);
    this.list.hookToQuery(PriorityStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {
        

        this.prioritylist.push({
          id: response.items[i].id,
          Name: response.items[i].priorityName
        });

      }

      console.log("Priority", this.prioritylist);


    });

    //Task List
    const taskStreamCreator = (query) => this.taskService.getList(query);
    this.list.hookToQuery(taskStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {
        

        this.tasklist.push({
          id: response.items[i].id,
          Name: response.items[i].taskName
        });

      }

      console.log("tasklist", this.tasklist);


    });

    //ToDo List
    const todoStreamCreator = (query) => this.todoService.getList(query);
    this.list.hookToQuery(todoStreamCreator).subscribe((response) => {
    
      for (let ids = 0; ids < response.totalCount; ids++) {
        let categoryid = response.items[ids].categoryId;
        let statusid = response.items[ids].statusId;
        let taskid = response.items[ids].taskId;
        let priorityid = response.items[ids].priorityId;
        response.items[ids].date = response.items[ids].date.slice(0,10);
        //Category
        this.categoryService.get(categoryid).subscribe((response) => {
         
          this.todo.items[ids].category = response.categoryName;
        });

        //Task
        this.taskService.get(taskid).subscribe((response) => {
          
          this.todo.items[ids].task = response.taskName;
        });

        //Priority
        this.priorityService.get(priorityid).subscribe((response) => {

          this.todo.items[ids].priority = response.priorityName;
        });

        //status
        this.statusService.get(statusid).subscribe((response) => {

          this.todo.items[ids].status = response.statusName;
        });
      }
      this.todo = response;
    });


    this.oAuthService.loadUserProfile().then(
      data => this.logged_in_User = data.sub);

      
  }


  login() {
    this.authService.navigateToLogin();
    
  }

  createToDo() {
    this.selectedToDo = {} as ToDoDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editToDo(id: string) {
    this.todoService.get(id).subscribe((Todo) => {
      this.selectedToDo = Todo;
      this.buildForm();
      this.isModalOpen = true;

    });
  }


  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((Todo) => {
        if (Todo === Confirmation.Status.confirm) {
          this.todoService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

  buildForm() {
    
    
    this.form = this.fb.group({
      categoryid: [this.selectedToDo.categoryId, Validators.required],
      statusid: [this.selectedToDo.statusId, Validators.required],
      priorityid: [this.selectedToDo.priorityId, Validators.required],
      taskid: [this.selectedToDo.taskId, Validators.required],
      date: [this.selectedToDo.date, Validators.required],
      remarks: [this.selectedToDo.remarks, Validators.required],
      assignedBy:[this.logged_in_User]

    });

    console.log("form", this.form);

  }

  

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedToDo.id) {
      this.todoService
        .update(this.selectedToDo.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {

      this.todoService.createASyncByInput(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

}
