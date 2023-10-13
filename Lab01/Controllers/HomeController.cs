using Lab01.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Lab01.Controllers
{
    public class HomeController : Controller
    {
        private SmacdbContext _db;
        public HomeController(SmacdbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Students.ToList());
        }
        [HttpGet]
        public JsonResult Details(int id)
        {
            var student = _db.Students.SingleOrDefault(x => x.Id == id);
            JsonResponseViewModel model = new JsonResponseViewModel();
            if(student != null)
            {
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(student);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No records";
            }
            return Json(model);
        }
        [HttpPost]
        public JsonResult InsertStudent(IFormCollection formCollection)
        {
            Student student= new Student();
            student.Name= formCollection["Name"];
            student.Email= formCollection["Email"];
            student.Phone = formCollection["Phone"];
            JsonResponseViewModel model = new JsonResponseViewModel();
            if (student!=null)
            {
                model.ResponseCode= 0;
                model.ResponseMessage = JsonConvert.SerializeObject(student);
                _db.Students.Add(student);
                _db.SaveChangesAsync();
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No records";
            }
            return Json(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}