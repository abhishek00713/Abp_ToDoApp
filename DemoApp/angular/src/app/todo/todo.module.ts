import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TodoRoutingModule } from './todo-routing.module';
import { TodoComponent } from './todo.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
  declarations: [TodoComponent],
  imports: [
    TodoRoutingModule,
    SharedModule,
    Ng2SearchPipeModule,
    FormsModule
  ]
})
export class TodoModule { }
