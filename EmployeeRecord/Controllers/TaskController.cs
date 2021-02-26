using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeRecord.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecord.Controllers
{
    public class TaskController : Controller
    {
        private readonly EmployeeContext _dbContext;

        public TaskController(EmployeeContext shoppingContext)
        {
            _dbContext = shoppingContext;
            //productService = new ProductService(shoppingContext);
            //_productIds = productIds;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(string taskName, string projName)
        {
            return View();
        }
    }
}