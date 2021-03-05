using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.layer;
using DB.Layer.DbOperation;

namespace WithDataBaseApp.Controllers
{
    public class HomeController : Controller
    {
        EmployeeRepository Repository = null;
        public HomeController() // constructor
        {
            Repository = new EmployeeRepository();
        }
        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeModel model)   // for posting data into database used verbs
        {
            if (ModelState.IsValid)
            {
                int id = Repository.AddEmployee(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = "Data Added Successfully";
                }
            }
            return View();
        }

        public ActionResult GetAllRecord()
        {
            var abc = Repository.GetAllEmployees();
            return View(abc);
        }
        public ActionResult Details(int id)
        {
            var Employee = Repository.GetEmployee(id);
            return View(Employee);
        }
      
        public ActionResult Edit(int id) //getting data from database
        {
            var Employee = Repository.GetEmployee(id);
            return View(Employee);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeModel model) //posting data into database
        {
            if (ModelState.IsValid)
            {
                Repository.UpdateEmployee(model.Id, model);
                
            }
            return RedirectToAction("GetAllRecord");  
        }


        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Repository.DeleteEmployee(id);

            return RedirectToAction("GetAllRecord");
        }
    }
}