using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_Management.Data;
using Task_Management.DataAccess;
using Task_Management.Models;


namespace Task_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _userContext;

        public HomeController(ILogger<HomeController> logger, UserContext context)
        {
            _logger = logger;
            _userContext = context;
        }

        public IActionResult Index()
        {
            return View(); // If you want to show tasks, replace with Tasks list
        }

        public IActionResult Team()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Task()
        {
            var tasks = _userContext.Tasks.ToList(); // 🟢 Get from DB now

            // ✅ Get username from claims (if user is authenticated)
            //#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var username = (User?.Identity != null && User.Identity.IsAuthenticated)
                ? User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value
                : "Guest";
            //#pragma warning restore CS8602 // Dereference of a possibly null reference.

            ViewBag.Username = username; // Send username to view
            return View(tasks);
        }

        [HttpPost]
        public IActionResult Task([FromBody] string task)
        {
            var newTask = new Task_Management.Models.Task
            {
                Title = task,
                IsCompleted = false
            };

            _userContext.Tasks.Add(newTask);
            _userContext.SaveChanges();

            var updatedTasks = _userContext.Tasks.ToList();
            return Json(new { success = true, tasks = updatedTasks });
        }

        [HttpPost]
        public IActionResult Toggle(int id)
        {
            var task = _userContext.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _userContext.SaveChanges();
            }
            return Ok();
        }

        //[HttpPost("/home/delete/{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var task = _userContext.Tasks.FirstOrDefault(t => t.Id == id);
        //    if (task != null)
        //    {
        //        _userContext.Tasks.Remove(task);
        //        _userContext.SaveChanges();
        //    }
        //    return Ok();
        //}

        [HttpPost("/home/delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var task = _userContext.Tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    _userContext.Tasks.Remove(task);
                    _userContext.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting task with ID {id}");
                return StatusCode(500, "An error occurred while deleting the task.");
            }
        }

        //Contact action
        [HttpPost]
        public IActionResult SubmitContact(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userContext.Contacts.Add(contact);
                    _userContext.SaveChanges();
                    TempData["Success"] = "Your message has been sent successfully!";
                }
                else
                {
                    TempData["Error"] = "Please fill all fields correctly.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting contact form");
                TempData["Error"] = "Something went wrong. Please try again later.";
            }

            return RedirectToAction("Index", "Home");
}


[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}













//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using Task_Management.Data;
//using Task_Management.Models;

//namespace Task_Management.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly UserContext _userContext;
//        private readonly MemoryDataStore _dataStore;



//        public HomeController(ILogger<HomeController> logger,UserContext context,MemoryDataStore dataStore)
//        {
//            _logger = logger;
//            _userContext = context;
//            _dataStore = dataStore;

//        }

//        public IActionResult Index()
//        {
//            var tasks = _userContext.Users.ToList();
//            return View(tasks);
//        }

//        public IActionResult Team()
//        {
//            return View();
//        }

//        //Toggle Task completion -->index tells which task to toggle.
//        //If valid, it flips the IsCompleted flag for that task.
//        [HttpPost("/home/toggle/{index}")]
//        public IActionResult Toggle(int index)
//        {
//            if (index >= 0 && index < _dataStore.tasks.Count)
//            {
//                _dataStore.tasks[index].IsCompleted = !_dataStore.tasks[index].IsCompleted;
//            }
//            return Ok();
//        }

//        //Delete a Task
//        [HttpPost("/home/delete/{index}")]
//        public IActionResult Delete(int index)
//        {
//            try
//            {
//                if (index >= 0 && index < _dataStore.tasks.Count)
//                {
//                    _dataStore.tasks.RemoveAt(index);
//                }
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while deleting task.");
//                return StatusCode(500, "An error occurred while deleting the task.");
//            }
//        }


//        //It sends all current tasks (from memory) to the view as a model.
//        [HttpGet]
//        public IActionResult Task()
//        {
//            var tasks = _dataStore.tasks;
//            return View(tasks); // ✅ passes data to the view
//        }

//        [HttpPost]
//        public IActionResult Task([FromBody] string task)
//        {
//            Models.Task newTask = new Models.Task(); //empty object
//            newTask.Title = task;
//            newTask.IsCompleted = false;
//            _dataStore.tasks.Add(newTask); //adds a task to memory
//            return Json(new { success = true, tasks = _dataStore.tasks });
//        }


//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
