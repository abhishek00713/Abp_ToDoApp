import { AuthService, ListService, LIST_QUERY_DEBOUNCE_TIME, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { AssignedToUserService, CategoryService, DefinitionAttachmentService, PriorityService, StatusService, TaskService, ToDoService } from '../proxy/app-services';
import { CategoryDto } from '../proxy/category-dtos';
import { PriorityDto } from '../proxy/priority-dtos';
import { StatusDto } from '../proxy/status-dtos';
import { TaskDto } from '../proxy/task-dtos';
import { GetToDoListDto, ToDoDto } from '../proxy/to-do-dtos';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UserDtos } from '../proxy';
import { AbpUserDto } from '../proxy/user-dtos';
import { HttpClient } from '@angular/common/http';
import { CreateDefinitionAttachmentDto } from '../proxy/definition-attachment-dtos';
import { Byte } from '@angular/compiler/src/util';



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

  is_not_logged_in = false;
  is_userlogged_in = false;
  is_adminlogged_in = false;

  assigned_user_todo_list = [];

  assigned_user_todo = { items: []} as PagedResultDto<ToDoDto>;

  todo = { items: [], totalCount: 0 } as PagedResultDto<ToDoDto>;

  isDownloadFinish = false;
  downloaded_File_Name: string;

  file_content: string;

  logged_User_role: string;
  isAdminModalOpen = false;

 

  category =[] as CategoryDto;
  status = [] as StatusDto;
  priority = [] as PriorityDto;
  task = [] as TaskDto;
  
  isUserModalOpen = false;
  closeResult = '';
  form: FormGroup;
  selected = {} as GetToDoListDto;
  selectedToDo = {} as ToDoDto;
  isModalOpen = false;
  logged_in_User: any; 
  //categorylist: string[] = [];
  categorylist = [];
  statuslist = [];
  prioritylist = [];
  tasklist = [];
  userlist = [];
  isSearchOpen = false;
  isFileUpload = false;

  isUserListOpen = false;
  todo_id: string = '';
  selected_user_list = [];
  //selected_user_id = []; 
  data = [];
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  loadContent = false;
  selected_todo_user_list = { items: [], totalCount: 0 } as PagedResultDto<AbpUserDto>;
  unselect_list = [];
  assignedTouser_id=[];

  public dateValue: Date = new Date("05/16/2017 3:00 AM");

  // City Names
  City: any = ['Florida', 'South Dakota', 'Tennessee', 'Michigan']


  files = {};
  fileToUpload: FormData;
  fileUpload: any;
  fileUpoadInitiated: boolean;
  fileDownloadInitiated: boolean;
  private baseUrl = 'http://localhost:4000/api/blobstorage';  

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
    private cdRef: ChangeDetectorRef,
    private assignedToUser: AssignedToUserService,
    private http: HttpClient,
    private definitionttachmentService: DefinitionAttachmentService

  ) { }


  
  User_setPage(pageInfo) {

    
    this.assignedToUser.get_User_assigned_todo_list(this.logged_in_User).subscribe((response) => {
      
      this.assigned_user_todo.totalCount = response.length;

      for (let i = 0; i < response.length; i++) {
        console.log("re", response[i]);

        this.todoService.get(response[i]).subscribe((output) => {


          let categoryid = output.categoryId;
          let statusid = output.statusId;
          let taskid = output.taskId;
          let priorityid = output.priorityId;
         
 

          this.categoryService.get(categoryid).subscribe((categoryresponse) => {

            this.assigned_user_todo.items[i].category = categoryresponse;
          });

          this.taskService.get(taskid).subscribe((taskresponse) => {

            this.assigned_user_todo.items[i].task = taskresponse;
          });

          this.statusService.get(statusid).subscribe((statusresponse) => {
            this.assigned_user_todo.items[i].status = statusresponse;
          });

          this.priorityService.get(priorityid).subscribe((priorityresponse) => {

            this.assigned_user_todo.items[i].priority = priorityresponse;
          });

          this.assigned_user_todo.items[i] = output;
          
          console.log("list", this.assigned_user_todo);

        });
      }
    });

   
  }

  setPage(pageInfo) {

    
  
    //ToDo List
    const todoStreamCreator = (query) => this.todoService.getList(query);
    this.list.hookToQuery(todoStreamCreator).subscribe((response) => {
    

      for (let ids = 0; ids < response.items.length; ids++) {
        
        let categoryid = response.items[ids].categoryId;
        let statusid = response.items[ids].statusId;
        let taskid = response.items[ids].taskId;
        let priorityid = response.items[ids].priorityId;
        response.items[ids].date = response.items[ids].date.slice(0, 10);
        //Category
        this.categoryService.get(categoryid).subscribe((response) => {

          this.category = response;

          if (this.category.categoryName === null) {
            this.category.categoryName = "category";
          }
          else {
            
            this.todo.items[ids].category = this.category;
          }
          


        });


        //Task
        this.taskService.get(taskid).subscribe((response) => {

          this.task = response;


          this.todo.items[ids].task = this.task;
        });

        //Priority
        this.priorityService.get(priorityid).subscribe((response) => {

          this.priority = response;

          this.todo.items[ids].priority = this.priority;
        });

        //status
        this.statusService.get(statusid).subscribe((response) => {

          this.status = response;

          this.todo.items[ids].status = this.status;
        });
      }
      this.todo = response;
      
    });


    this.oAuthService.loadUserProfile().then(
      data => this.logged_in_User = data.sub);

    

  }


  AdminPage() {



    //Category List
    const categoryStreamCreator = (query) => this.categoryService.getFullListByInput(query);

    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {


        this.categorylist.push({
          id: response.items[i].id,
          Name: response.items[i].categoryName
        });

      }




    });

    //Status List
    const statusStreamCreator = (query) => this.statusService.getFullListByInput(query);
    this.list.hookToQuery(statusStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {


        this.statuslist.push({
          id: response.items[i].id,
          Name: response.items[i].statusName
        });

      }




    });

    //Priority List
    const PriorityStreamCreator = (query) => this.priorityService.getFullListByInput(query);
    this.list.hookToQuery(PriorityStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {


        this.prioritylist.push({
          id: response.items[i].id,
          Name: response.items[i].priorityName
        });

      }




    });

    //Task List
    const taskStreamCreator = (query) => this.taskService.getFullListByInput(query);
    this.list.hookToQuery(taskStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {


        this.tasklist.push({
          id: response.items[i].id,
          Name: response.items[i].taskName
        });

      }




    });





    this.setPage({ offset: 0, limit: 10 });
  }

  UserPage(id: any) {


   
    this.logged_in_User = id;
    
    this.User_setPage({ offset: 0, limit: 10 });
    
  }

  ngOnInit() {
    
    

    this.is_not_logged_in = false;
    this.is_adminlogged_in = false;
    this.is_userlogged_in = false;
    if (this.oAuthService.getAccessToken() == null) {
      this.is_not_logged_in = true;

      
     
    }
    else {
    
      
      this.oAuthService.loadUserProfile().then((data) => {
        //NavbarPart
        //This line of code is for maintaining the components of the navbar as per the user
       
        localStorage.removeItem('role');
        localStorage.setItem('role', data.role);
       
        var invis = localStorage.getItem('invis');
        if (invis == "true" && data.role == "admin") {
          location.reload();
        }
        else if(invis =="false" && data.role =="user"){
          location.reload();
        }

        //UI Part
        if (data.role == 'admin') {
          console.log("tywesa");
          this.is_adminlogged_in = true;
          this.AdminPage();
        }
        else {
          this.is_userlogged_in = true;
          this.UserPage(data.sub);
        }

      });
    }
    

    console.log("home ko invis", localStorage.getItem('invis'));
    console.log("home ko invis", localStorage.getItem('role'));

 
      
  }

  //File Upload

  public closeDropdown() {
    
    this.isUserModalOpen = false;
    this.selected_user_list = [];
    this.selectedItems = [];
    this.dropdownList = [];
    
    console.log("closed", this.dropdownList);
    console.log("closed", this.selectedItems);

  }
  
  public onFilterChange(item: any) {
    console.log(item);
  }
  public onDropDownClose(item: any) {
    
    console.log("dd",item);
  }

  public onItemSelect(item: any) {
    
    this.selected_user_list.push(item);
    console.log("item selecterde", this.selected_user_list);
    

  }
  public onDeSelect(item: any) {
    this.unselect_list.push(item);
    console.log(item);
  }

  public onSelectAll(items: any) {
    for (let i = 0; i < items.length; i++) {
      this.selected_user_list.push(items[i]);
    }
    
    console.log("all", items.length);
  }
  public onDeSelectAll(items: any) {
    console.log(items);
  }

  SearchModal() {
    this.selectedToDo = {} as ToDoDto;
    
    this.form = this.fb.group({
      filterCategory: [this.selectedToDo.categoryId, Validators.required],
      filterStatus: [this.selectedToDo.statusId, Validators.required],
      filterPriority: [this.selectedToDo.priorityId, Validators.required],
      filterTask: [this.selectedToDo.taskId, Validators.required],



    });
    this.isSearchOpen = true;

  }

  search() {
    
    this.isSearchOpen = false;
    

    
    this.todoService.getAllList(this.form.value).subscribe((response) => {

     
      
      for (let ids = 0; ids < response.totalCount; ids++) {
        let categoryid = response.items[ids].categoryId;
        let statusid = response.items[ids].statusId;
        let taskid = response.items[ids].taskId;
        let priorityid = response.items[ids].priorityId;
        response.items[ids].date = response.items[ids].date.slice(0, 10);
        //Category
        this.categoryService.get(categoryid).subscribe((response) => {

          this.category = response;

          this.todo.items[ids].category = this.category;



        });

        //Task
        this.taskService.get(taskid).subscribe((response) => {

          this.task = response;


          this.todo.items[ids].task = this.task;
        });

        //Priority
        this.priorityService.get(priorityid).subscribe((response) => {

          this.priority = response;

          this.todo.items[ids].priority = this.priority;
        });

        //status
        this.statusService.get(statusid).subscribe((response) => {

          this.status = response;

          this.todo.items[ids].status = this.status;
        });
      }
      this.form.reset();
      this.todo = response;

    });


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

  AddUser() {
    
    
    //this.selected_user_list = [];
    this.selected = {} as GetToDoListDto;
    //this.userlist = [];

    this.isUserModalOpen = true;

   


    this.form = this.fb.group({
      filter: [this.selected.filter, Validators.required],


    });

    console.log("formssss", this.form);
    //List of all the user

    this.form = this.fb.group({
      preselect: [this.selectedItems]
    });
    console.log("form", this.form);

    this.todoService.getUserListByInput(this.form.value).subscribe((results) => {

      for (let result = 0; result < results.length; result++) {
        this.userlist.push({ item_id: results[result].id, item_text: results[result].name });
      }
      this.dropdownList = this.userlist;
      console.log("usder", this.userlist);

    });



      

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      enableCheckAll: true,
      selectAllText: 'Select All',
      unSelectAllText: 'Unselect All',
      allowSearchFilter: true,
      limitSelection: -1,
      clearSearchFilter: true,
      maxHeight: 197,
      itemsShowLimit: 3,
      searchPlaceholderText: 'Search ',
      noDataAvailablePlaceholderText: 'No data available',
      closeDropDownOnSelection: false,
      showSelectedItemsAtTop: true,
      defaultOpen: false
    };

    //List of selected user
    for (let i = 0; i < this.selected_todo_user_list.totalCount; i++) {
      this.selectedItems.push({
        item_id: this.selected_todo_user_list.items[i].id,
        item_text: this.selected_todo_user_list.items[i].name
      })
    }

    console.log("pre",this.selectedItems);
    setTimeout(() => {
      
      this.form = this.fb.group({
        preselect: [this.selectedItems]
      });
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

   

  }

  DeleteUser(id: string) {
    this.assignedTouser_id = [];
    
    this.assignedToUser.get_Todo_idByTodoidAndUserid(this.todo_id, id).subscribe((response) => {
      this.assignedTouser_id.push(response);
    });

    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.assignedToUser.delete(this.assignedTouser_id[0]).subscribe(() => this.list.get());
        }
      })
    
    console.log("tid",this.assignedTouser_id);
    console.log("todo_id", this.todo_id);
    console.log("user", id);
  }

  public saveUser() {

    //this.selected_user_id = [];
    for (let i = 0; i < this.selected_user_list.length; i++) {

      
      this.form = this.fb.group({
        userId: new FormControl(this.selected_user_list[i].item_id.toString(), Validators.required),
        toDoId: new FormControl(this.todo_id, Validators.required),
      });

      this.assignedToUser.createASyncByInput(this.form.value).subscribe(() => {
        this.isUserModalOpen = false;
        this.form.reset();
        this.list.get();
        this.selected_user_list = [];
      });
     
    }

    


   
     
    console.log("todo_id", this.todo_id);
    console.log("form_saved", this.form);
    
    
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
          this.categorylist = [];
          this.prioritylist = [];
          this.statuslist = [];
          this.tasklist = [];
        });
    } else {

      this.todoService.createASyncByInput(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.categorylist = [];
        this.prioritylist = [];
        this.statuslist = [];
        this.tasklist = [];
      });
    }
  }


  UserList(id: string) {
   
    this.dropdownList = [];
    this.selectedItems = [];
    this.selected_user_list = [];
    this.selected_todo_user_list.items = [];
    this.selected_todo_user_list.totalCount = 0;
    this.userlist=[]

    this.todo_id = id;
    //this.selected_todo_user_list.items = [];

    const todoStreamCreator = (query) => this.assignedToUser.get_Todo_userListByTodoid(this.todo_id);
    this.list.hookToQuery(todoStreamCreator).subscribe((response) => {
      this.selected_todo_user_list = response;

    });
   
   
   
    this.isUserListOpen = true;

  }


  FileUploadModal(id: string) {
    
    this.todo_id = id;
    console.log("id", this.todo_id);
    this.isFileUpload = true;
    this.showBlobs();
  }

  showBlobs() {
    this.files = [];
    this.isDownloadFinish = false;

    const attachmentStreamCreator = (query) => this.definitionttachmentService.getList(query);
    this.list.hookToQuery(attachmentStreamCreator).subscribe((response) => {
      for (let i = 0; i < response.totalCount; i++) {
        this.files = response.items;
        
      }

      console.log("files", this.files);

    });

  }

  addFile() {
    if (!this.fileUpoadInitiated) {
      document.getElementById('fileUpload').click();
    }
    console.log("add thickyo");
  }



  handleFileInput(files: any) {

  
    console.log("handling", files);

    var reader = new FileReader();
    var fileToRead = document.querySelector('input').files[0];
    var input_fileattachment = {} as CreateDefinitionAttachmentDto;
    var todo_id = this.todo_id;
    var def = this.definitionttachmentService;
    

    // attach event, that will be fired, when read is end
    reader.addEventListener("loadend", function () {
      // reader.result contains the contents of blob as a typed array
      // we insert content of file in DOM here
      let s = reader.result;
      for (let i = 0; i < files.length; i++) {
        input_fileattachment.fileName = todo_id;
        input_fileattachment.caption = files[i].name;
        input_fileattachment.toDoId = todo_id;
        input_fileattachment.binaryFile = s.toString();



      }

      def.createASyncByInput(input_fileattachment).subscribe(() => {
 
      });

    });

    // start reading a loaded file
    reader.readAsText(fileToRead)



    this.showBlobs();
   

    
   

    
  }

  downloadFile(fileName: string, caption:string) {
    console.log("download", fileName);
    this.definitionttachmentService.getBlobFile(fileName).subscribe(() => {
      this.isDownloadFinish = true;
      
      this.downloaded_File_Name = caption;
    });
    this.isDownloadFinish = false;
  }
  deleteFile(fileName: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.definitionttachmentService.deleteBlobFile(fileName).subscribe(() => {
            this.list.get();

            this.showBlobs();
          });
        }
      });

    

  }



}
