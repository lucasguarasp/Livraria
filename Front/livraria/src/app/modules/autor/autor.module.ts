import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutorRoutingModule } from './autor-routing.module';
import { AutorComponent } from './autor.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [AutorComponent],
  imports: [
    SharedModule,
    CommonModule,
    AutorRoutingModule
  ]
})
export class AutorModule { }
