import { NgModule } from '@angular/core';


import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


@NgModule({
  declarations: [CategoryComponent],
  imports: [
    SharedModule,
    CategoryRoutingModule,
    Ng2SearchPipeModule,
    FormsModule
  ]
})
export class CategoryModule { }
