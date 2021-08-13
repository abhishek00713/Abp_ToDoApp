import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TaskRoutingModule } from './task-routing.module';
import { TaskComponent } from './task.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


@NgModule({
  declarations: [TaskComponent],
  imports: [
    SharedModule,
    TaskRoutingModule,
    Ng2SearchPipeModule,
    FormsModule
  ]
})
export class TaskModule { }
