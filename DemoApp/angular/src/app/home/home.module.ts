import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { NgbDatepickerModule, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { DateTimePickerModule } from '@syncfusion/ej2-angular-calendars';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
  declarations: [HomeComponent],
  imports: [SharedModule,
    HomeRoutingModule,
    NgbDatepickerModule,
    NgbTimepickerModule,
    DateTimePickerModule,
    Ng2SearchPipeModule,
    FormsModule,
    NgMultiSelectDropDownModule

  ],
  exports:[HomeComponent]
})
export class HomeModule {}
