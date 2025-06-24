using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_Management.Data;
using Task_Management.Models;

namespace Task_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _userContext;
        private readonly MemoryDataStore _dataStore;

        

        public HomeController(ILogger<HomeController> logger,UserContext context,MemoryDataStore dataStore)
        {
            _logger = logger;
            _userContext = context;
            _dataStore = dataStore;

        }

        public IActionResult Index()
        {
            var tasks = _userContext.Users.ToList();
            return View(tasks);
        }

        public IActionResult Team()
        {
            return View();
        }

        //Toggle Task completion -->index tells which task to toggle.
        //If valid, it flips the IsCompleted flag for that task.
        [HttpPost("/home/toggle/{index}")]
        public IActionResult Toggle(int index)
        {
            if (index >= 0 && index < _dataStore.tasks.Count)
            {
                _dataStore.tasks[index].IsCompleted = !_dataStore.tasks[index].IsCompleted;
            }
            return Ok();
        }

        //Delete a Task
        [HttpPost("/home/delete/{index}")]
        public IActionResult Delete(int index)
        {
            try
            {
                if (index >= 0 && index < _dataStore.tasks.Count)
                {
                    _dataStore.tasks.RemoveAt(index);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting task.");
                return StatusCode(500, "An error occurred while deleting the task.");
            }
        }


        //It sends all current tasks (from memory) to the view as a model.
        [HttpGet]
        public IActionResult Task()
        {
            var tasks = _dataStore.tasks;
            return View(tasks); // ✅ passes data to the view
        }

        [HttpPost]
        public IActionResult Task([FromBody] string task)
        {
            Models.Task newTask = new Models.Task(); //empty object
            newTask.Title = task;
            newTask.IsCompleted = false;
            _dataStore.tasks.Add(newTask); //adds a task to memory
            return Json(new { success = true, tasks = _dataStore.tasks });
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
