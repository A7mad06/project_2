using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using todo.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TodoController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=Todo;Trusted_Connection=true");
            List<TodoModels> Lst = new List<TodoModels>();
            SqlCommand cmd = new SqlCommand("Select * From Todos where IsDeleted=0", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TodoModels obj = new TodoModels();
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Description = Convert.ToString(dr[1]);
                obj.IsCompleted = Convert.ToBoolean(dr[2]);
                obj.IsDeleted = Convert.ToBoolean(dr[3]);
                obj.CompletedDate = Convert.ToDateTime(dr[6]);
                obj.CreatedDate = Convert.ToDateTime(dr[4]);
                obj.DeletedDate = Convert.ToDateTime(dr[5]);
                obj.CategoryName = Convert.ToString(dr[7]);
                Lst.Add(obj);
            }
            return Ok(Lst);
        }
        [HttpGet]
        [Route("deleted-todos")]
        public async Task<IActionResult> GetAllDeletedTodos()
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=Todo;Trusted_Connection=true");
            List<TodoModels> Lst = new List<TodoModels>();
            SqlCommand cmd = new SqlCommand("Select * From Todos where IsDeleted=1", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TodoModels obj = new TodoModels();
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Description = Convert.ToString(dr[1]);
                obj.IsCompleted = Convert.ToBoolean(dr[2]);
                obj.IsDeleted = Convert.ToBoolean(dr[3]);
                obj.CompletedDate = Convert.ToDateTime(dr[6]);
                obj.CreatedDate = Convert.ToDateTime(dr[4]);
                obj.DeletedDate = Convert.ToDateTime(dr[5]);
                obj.CategoryName = Convert.ToString(dr[7]);
                Lst.Add(obj);
            }
            return Ok(Lst);
        }

        [HttpPost]

        public async Task<IActionResult> AddTodo(TodoModels todo)
        {
            todo.CreatedDate = DateTime.Now;
            todo.DeletedDate = DateTime.Now;
            todo.CompletedDate = DateTime.Now;
            todo.Id = Guid.NewGuid();
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=Todo;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("insert into Todos(Id,IsCompleted,IsDeleted,CreatedDate,Description,CategoryName,DeletedDate,CompletedDate) values ('" + todo.Id + "','" + todo.IsCompleted + "','" + todo.IsDeleted + "','" + todo.CreatedDate + "','" + todo.Description + "','" + todo.CategoryName + "','" + todo.DeletedDate + "','" + todo.CompletedDate + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return Ok(todo);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, TodoModels todoUpdateRequest)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=Todo;Trusted_Connection=true");
            List<TodoModels> Lst = new List<TodoModels>();
            SqlCommand cmd = new SqlCommand("Select * From Todos where Id = '" + id + "'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            TodoModels obj = new TodoModels();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Description = Convert.ToString(dr[1]);
                obj.IsCompleted = Convert.ToBoolean(dr[2]);
                obj.IsDeleted = Convert.ToBoolean(dr[3]);
                obj.CompletedDate = Convert.ToDateTime(dr[6]);
                obj.CreatedDate = Convert.ToDateTime(dr[4]);
                obj.DeletedDate = Convert.ToDateTime(dr[5]);
                obj.CategoryName = Convert.ToString(dr[7]);
                Lst.Add(obj);
            }
            var time = DateTime.Now;
            if(obj.IsCompleted == true)
            {
                SqlCommand cmd1 = new SqlCommand("Update Todos set IsCompleted=0 where Id = '" + id + "'", con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                SqlCommand cmd4 = new SqlCommand("Update Todos set CompletedDate='"+time+"' where Id = '" + id + "'", con);
                con.Open();
                cmd4.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("Update Todos set IsCompleted=1 where Id = '" + id + "'", con);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                SqlCommand cmd5 = new SqlCommand("Update Todos set CompletedDate='" + time + "' where Id = '" + id + "'", con);
                con.Open();
                cmd5.ExecuteNonQuery();
                con.Close();
            }
            obj.IsCompleted=!obj.IsCompleted;
            obj.CompletedDate = DateTime.Now;
            return Ok(Lst);
        }


        [HttpPut]
        [Route("undo-deleted-todo/{id:Guid}")]

        public async Task<IActionResult> UndoDeletedTodo([FromRoute] Guid id, TodoModels todoUndoRequest)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=Todo;Trusted_Connection=true");
            List<TodoModels> Lst = new List<TodoModels>();
            SqlCommand cmd = new SqlCommand("Select * From Todos where Id = '" + id + "'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            TodoModels obj = new TodoModels();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Description = Convert.ToString(dr[1]);
                obj.IsCompleted = Convert.ToBoolean(dr[2]);
                obj.IsDeleted = Convert.ToBoolean(dr[3]);
                obj.CompletedDate = Convert.ToDateTime(dr[6]);
                obj.CreatedDate = Convert.ToDateTime(dr[4]);
                obj.DeletedDate = Convert.ToDateTime(dr[5]);
                obj.CategoryName = Convert.ToString(dr[7]);
                Lst.Add(obj);
            }
            SqlCommand cmd1 = new SqlCommand("Update Todos set IsDeleted=0 where Id = '" + id + "'", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            obj.IsDeleted = false;
            return Ok(Lst);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=Todo;Trusted_Connection=true");
            List<TodoModels> Lst = new List<TodoModels>();
            SqlCommand cmd = new SqlCommand("Select * From Todos where Id = '" + id + "'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            TodoModels obj = new TodoModels();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Description = Convert.ToString(dr[1]);
                obj.IsCompleted = Convert.ToBoolean(dr[2]);
                obj.IsDeleted = Convert.ToBoolean(dr[3]);
                obj.CompletedDate = Convert.ToDateTime(dr[6]);
                obj.CreatedDate = Convert.ToDateTime(dr[4]);
                obj.DeletedDate = Convert.ToDateTime(dr[5]);
                obj.CategoryName = Convert.ToString(dr[7]);
                Lst.Add(obj);
            }
            var time = DateTime.Now;
            SqlCommand cmd1 = new SqlCommand("Update Todos set IsDeleted=1 where Id = '" + id + "'", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            SqlCommand cmd4 = new SqlCommand("Update Todos set DeletedDate='" + time + "' where Id = '" + id + "'", con);
            con.Open();
            cmd4.ExecuteNonQuery();
            con.Close();
            obj.IsDeleted = true;
            obj.DeletedDate = DateTime.Now;
            return Ok(Lst);
        }
    }
}
