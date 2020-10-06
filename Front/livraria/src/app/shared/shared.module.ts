import { NgSelectModule } from '@ng-select/ng-select';
import { LivroService } from './providers/livro.service';
import { AutorService } from './providers/autor.service';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule, NgSelectModule

  ],
  declarations: [

  ],
  entryComponents: [

  ],
  exports: [
    CommonModule, FormsModule, ReactiveFormsModule, NgSelectModule
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        AutorService, LivroService
      ],
    };
  }
}
