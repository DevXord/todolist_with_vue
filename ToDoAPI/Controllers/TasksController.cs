using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.Web.Http.Cors;
using ToDoAPI.Models;
using static ToDoAPI.Models.TasksModel;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    [EnableCors(origins: "https://localhost:7235/api/", headers: "*", methods: "*")]
    public class TasksController : Controller
    {
        [HttpGet]
        public IActionResult GetTasks()
        {

            var tasks = TasksModel.GetTasks();

            if (tasks.Count != 0)
                return Ok(tasks);
            else
                return NotFound();

        }
        [HttpDelete("{ID_Task}")]
        public IActionResult DeleteTask(int ID_Task)
        {

            TasksModel.DeleteTask(ID_Task);

            return Ok();
     

        }
        [HttpPut]
        public IActionResult UpdateTask([FromBody]TaskToDo task)
        {
            TasksModel.UpdateTask(task);

            return Ok();
        }
        [HttpPut]
        public IActionResult CreateTask([FromBody]TaskToDo task)
        {
            TasksModel.CreateNewTask(task);
            return Ok();
        }




    }
}
