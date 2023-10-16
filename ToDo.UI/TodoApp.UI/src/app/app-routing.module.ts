import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodosComponent } from './components/todos/todos.component';
import { DeletedTodosComponent } from './components/deleted-todos/deleted-todos.component';

const routes: Routes = [
  {
    path: '',
    component: TodosComponent
  },
  {
    path: 'todos',
    component: TodosComponent
  },
  {
    path: 'deletedTodos',
    component: DeletedTodosComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
