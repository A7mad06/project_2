import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-completed-todos',
  templateUrl: './completed-todos.component.html',
  styleUrls: ['./completed-todos.component.css']
})
export class CompletedTodosComponent implements OnInit {
  todos: Todo[] = [];

  constructor(private todoService: TodoService) { }
  ngOnInit(): void {
    this.todoService.getAllCompletedTodos()
      .subscribe({
        next: (res) => {
          this.todos = res;
        }
      });
  }
}