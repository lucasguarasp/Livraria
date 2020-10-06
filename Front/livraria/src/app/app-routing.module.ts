import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
   { path: 'livro', loadChildren: () => import('./modules/livro/livro.module').then(m => m.LivroModule) },
   { path: 'autor', loadChildren: () => import('./modules/autor/autor.module').then(m => m.AutorModule) },
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
