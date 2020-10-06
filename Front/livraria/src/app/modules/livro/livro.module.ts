import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LivroRoutingModule } from './livro-routing.module';
import { LivroComponent } from './livro.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [LivroComponent],
  imports: [
    SharedModule,
    CommonModule,
    LivroRoutingModule
  ],
  entryComponents: []
})
export class LivroModule { }
