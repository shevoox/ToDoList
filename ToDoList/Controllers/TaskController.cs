using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            return View(_context.Tasks.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoTask task)
        {
            _context.Add(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var todoTask = _context.Tasks.Find(id);
            return View(todoTask);
        }
        [HttpPost]
        public IActionResult Edit(TodoTask task)
        {
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var todoTask = _context.Tasks.Find(id);
            if (todoTask != null)
            {
                _context.Tasks.Remove(todoTask);
                _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var todoTask = _context.Tasks.Find(id);
            if (todoTask == null)
            {
                return NotFound();
            }

            todoTask.IsCompleted = !todoTask.IsCompleted;
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
