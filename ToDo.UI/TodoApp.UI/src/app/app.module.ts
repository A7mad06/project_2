import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodosComponent } from './components/todos/todos.component';
import { DeletedTodoComponent } from './components/deleted-todo/deleted-todo.component';
import { CompletedTodosComponent } from './components/completed-todos/completed-todos.component';

@NgModule({
  declarations: [
    AppComponent,
    TodosComponent,
    DeletedTodoComponent,
    CompletedTodosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
