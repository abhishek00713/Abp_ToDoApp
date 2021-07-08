import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PriorityRoutingModule } from './priority-routing.module';
import { PriorityComponent } from './priority.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


@NgModule({
  declarations: [PriorityComponent],
  imports: [
    SharedModule,
    PriorityRoutingModule,
    Ng2SearchPipeModule,
    FormsModule
  ]
})
export class PriorityModule { }
