using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Venkat.Models;

namespace Venkat.Controllers
{
    // [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<HomeController> logger;

        public HomeController(IEmployeeRepository employeeRepository,ILogger<HomeController> logger)
        {
            _employeeRepository=employeeRepository;
            this.logger = logger;
        }
        
        // [Route("")]
        // [Route("Index")]
        // [Route("~/")]

        public ViewResult Index()
        {
            var model=_employeeRepository.GetAllEmployee();
            return View(model);
        }

        // [Route("[action]/{id?}")]

        public ViewResult Details(int id=1)
        {
            logger.LogTrace("Trace log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error log");
            logger.LogCritical("Critical log");
            Employee model=_employeeRepository.GetEmployee(id);
            // ViewData["Employee"]=model;
            ViewBag.Employee=model;
            return View(model);
        }
        
        
        // public JsonResult Details(int id=1)
        // {
        //     Employee model=_employeeRepository.GetEmployee(id);
        //     return Json(model);
        // }
        
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.AddEmployee(employee);
                return RedirectToAction("details",new{id=newEmployee.Id});
            }
            return View();
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            Employee employee=_employeeRepository.GetEmployee(id);
            
            Employee editEmployee=new Employee
            {
                Id=employee.Id,
                Name=employee.Name,
                Department=employee.Department,
                Email=employee.Email
            };
            return View(editEmployee);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Employee model)
        {            
            if(ModelState.IsValid)
            {
                // Employee employee = _employeeRepository.GetEmployee(model.Id);
                // Employee editEmployee=new Employee
                // {
                //     Id=employee.Id,
                //     Name=employee.Name,
                //     Department=employee.Department,
                //     Email=employee.Email
                // };
                _employeeRepository.Update(model);
                return RedirectToAction("index");
            }
            
            return View();
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
