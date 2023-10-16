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
        public IActionResult Index(string fname)
        {
            var model = _db.Employees.ToList();
            if (string.IsNullOrEmpty(fname))
            {
                return View(model);
            }
            else
            {
                model=model.Where(e => e.FirstName!.Contains(fname)).ToList();
                return View(model);
            }
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
        [HttpGet]
        public IActionResult EditOrDelete(int employeeid)
        {
            var employee=_db.Employees.Find(employeeid);
            return View(employee);
        }
        [HttpPost]
        public IActionResult EditOrDelete(string submits, Employees employees, IFormFile file)
        {
            try
            {
                var model = _db.Employees.Find(employees.EmployeeID);
                if (model!=null && ModelState.IsValid)
                {
                    if (submits.Equals("Update"))
                    {
                        if(file!=null)
                        {
                            string path = Path.Combine("wwwroot/images", file.FileName);
                            var stream = new FileStream(path, FileMode.Create);
                            file.CopyTo(stream);
                        }
                        employees.Photo = file != null ? "images/" + file.FileName : model.Photo;

                        model.LastName = employees.LastName;
                        model.FirstName = employees.FirstName;
                        model.BirthDate = employees.BirthDate;
                        model.Photo = employees.Photo;
                        model.Skills = employees.Skills;
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _db.Employees.Remove(model); 
                        _db.SaveChanges(); 
                        return RedirectToAction("Index");
                    }
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
