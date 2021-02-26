using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeRecord.Models;
using EmployeeRecord.Services;

namespace EmployeeRecord.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeContext _dbContext;
        private readonly EmpRecordService empService;

        public HomeController(ILogger<HomeController> logger, EmployeeContext empContext)
        {
            _logger = logger;
            _dbContext = empContext;
            empService = new EmpRecordService(_dbContext);
        }

        public IActionResult Index()
        {
            //DateTime baseDate = DateTime.Today;
            //var thisWeekStart = baseDate.AddDays(-((int)baseDate.DayOfWeek - 1));
            //var thisWeekEnd = thisWeekStart.AddDays(5).AddSeconds(-1);
            //DateTime dt = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            //DateTime dt = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);

            String monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).Date.ToShortDateString();
            String tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday).Date.ToShortDateString();
            String web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday).Date.ToShortDateString();
            String thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday).Date.ToShortDateString();
            String fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday).Date.ToShortDateString();

            List<EmpTask> monList = empService.getEmpTaskByDate(monday);
            List<EmpTask> tueList = empService.getEmpTaskByDate(tuesday);
            List<EmpTask> webList = empService.getEmpTaskByDate(web);
            List<EmpTask> thuList = empService.getEmpTaskByDate(thu);
            List<EmpTask> friList = empService.getEmpTaskByDate(fri);
            List<Project> proList = empService.getAllProject();

            ViewData["mon"] = monList;
            ViewData["tue"] = tueList;
            ViewData["web"] = webList;
            ViewData["thu"] = thuList;
            ViewData["fri"] = friList;
            ViewData["projects"] = proList;

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

        [HttpPost]
       
        public IActionResult Submit(string taskId,string taskName, 
            string taskDesc,string clientName,string date,string startTime,string endTime,String projId)
        {

            //DateTime chooseDate = new DateTime();
            //string cd = Convert.ToDateTime(date).ToShortDateString();
            //var monday = DateTime.Now.Previous(DayOfWeek.Monday);
            //var sunday = DateTime.Now.Previous(DayOfWeek.Friday);


            string duration = empService.calculateDuration(startTime, endTime).ToString();
            if (taskId == null)
            {
                Boolean flag = empService.SaveNew(taskName, taskDesc,clientName, date, startTime, endTime, duration,projId);

                return Json(flag);
            }
            else
            {
                Boolean flag = empService.Edit(taskId,taskName, taskDesc, clientName, date, startTime, endTime, duration,projId);
                return Json(flag);
                
                
            }
            
        }

        [HttpPost]

        public IActionResult GetDataById(string taskId)
        {
            EmpTask task = new EmpTask();

            task = empService.getEmpTaskById(Int16.Parse(taskId));
            return Json(task);
        }
        public IActionResult Edit(string taskId)
        {
            EmpTask task = new EmpTask();
            List<Project> proList = empService.getAllProject();

            task = empService.getEmpTaskById(Int16.Parse(taskId));
            ViewData["task"] = task;
            ViewData["projects"] = proList;
            return View();
        }
    }
}
