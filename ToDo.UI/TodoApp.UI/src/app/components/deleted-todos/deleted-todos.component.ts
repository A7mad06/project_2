import { Component } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-deleted-todos',
  templateUrl: './deleted-todos.component.html',
  styleUrls: ['./deleted-todos.component.css']
})
export class DeletedTodosComponent {
  todos : Todo[]=[];
  constructor(private todoService: TodoService) {}
  ngOnInit(): void {
    this.getAllDeletedTodos();
  }
  getAllDeletedTodos() {
    this.todoService.getAllDeletedTodos()
      .subscribe({
        next: (todos) => {
          this.todos = todos;
        }
      });
  }
  undoDeletedTodo(id:string,todo:Todo) {
    this.todoService.undoDeletedTodo(id,todo)
      .subscribe({
        next: (res) =>
          this.getAllDeletedTodos()
      });
  }
}
