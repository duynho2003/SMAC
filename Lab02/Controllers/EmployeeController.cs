using Lab02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers
{
    public class EmployeeController : Controller
    {
        private DatabaseContext _db;
        public EmployeeController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Employees.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employees employees, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string path = Path.Combine("wwwroot/images", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    employees.Photo="images/" + file.FileName;

                    _db.Employees.Add(employees);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Fail...");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
    }
}
