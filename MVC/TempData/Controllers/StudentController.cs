using Microsoft.AspNetCore.Mvc;
using TempData.Models;
namespace TempData.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] ="Student created successfully";
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}
