using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeRecord.Models;
using EmployeeRecord.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecord.Controllers
{
    public class TimeSheetController : Controller
    {
        private readonly EmployeeContext _dbContext;
        private readonly EmpRecordService empService;

        public TimeSheetController(EmployeeContext empContext)
        {
           
            _dbContext = empContext;
            empService = new EmpRecordService(_dbContext);
        }

        public IActionResult Index()
        {
            String monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).Date.ToShortDateString();
            String tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday).Date.ToShortDateString();
            String web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday).Date.ToShortDateString();
            String thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday).Date.ToShortDateString();
            String fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday).Date.ToShortDateString();

            var list = empService.getTimeSheetSummary(monday,tuesday,web,thu,fri);
            ViewData["list"] = list;
            return View();
        }

        public IActionResult GetDataByWeek(String flag)
        {
            List<TaskDTO> list = new List<TaskDTO>();
            if (flag == "last")
            {
                list = lastWeekData();
                List<ChartData> list1 = new List<ChartData>();
                //list1 = getChartData();
                //return Json(list1);
            }
            else if(flag == "next")
            {
                list = nextWeekData();
            }
            else
            {
                list = currentWeekData();
            }

            return Json(list);
        }
        public List<TaskDTO> currentWeekData()
        {
            String monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).Date.ToShortDateString();
            String tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday).Date.ToShortDateString();
            String web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday).Date.ToShortDateString();
            String thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday).Date.ToShortDateString();
            String fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday).Date.ToShortDateString();

            var list = empService.getTimeSheetSummary(monday, tuesday, web, thu, fri);

            return list;
        }

        public void calculateLastWeek()
        {
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            monday = monday.AddDays(-7);
            DateTime tue = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
            tue = tue.AddDays(-7);

        }

        public List<TaskDTO> lastWeekData()
        {
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
            DateTime web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
            DateTime thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
            DateTime fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);


            String m = monday.AddDays(-7).ToShortDateString();
            String t = tuesday.AddDays(-7).ToShortDateString();
            String w = web.AddDays(-7).ToShortDateString();
            String th = thu.AddDays(-7).ToShortDateString();
            String f = fri.AddDays(-7).ToShortDateString();

            List<TaskDTO> list = empService.getTimeSheetSummary(m, t, w, th, f);

            return list;


        }

        public List<TaskDTO> nextWeekData()
        {
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
            DateTime web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
            DateTime thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
            DateTime fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);


            String m = monday.AddDays(+7).ToShortDateString();
            String t = tuesday.AddDays(+7).ToShortDateString();
            String w = web.AddDays(+7).ToShortDateString();
            String th = thu.AddDays(+7).ToShortDateString();
            String f = fri.AddDays(+7).ToShortDateString();

            List<TaskDTO> list = empService.getTimeSheetSummary(m, t, w, th, f);

            return list;


        }

        [HttpGet]
        public JsonResult PopulationChart()
        {
            String monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).Date.ToShortDateString();
            String tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday).Date.ToShortDateString();
            String web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday).Date.ToShortDateString();
            String thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday).Date.ToShortDateString();
            String fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday).Date.ToShortDateString();

            var populationList = empService.getTimeSheetSummaryForChart(monday, tuesday, web, thu, fri);
            List<ChartData> cd = new List<ChartData>();

            return Json(populationList);
            
        }
        public IActionResult GetChartDataByWeek(String flag)
        {
            List<ChartData> list = new List<ChartData>();
            if (flag == "last")
            {
                list = getLastWeekChartData();
                
                //list1 = getChartData();
                //return Json(list1);
            }
            else if (flag == "next")
            {
                list = getNextWeekChartData();
            }
            else
            {
                list = getCurrentWeekChartData();
            }

            return Json(list);
        }

        public List<ChartData> getCurrentWeekChartData()
        {
            String monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).Date.ToShortDateString();
            String tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday).Date.ToShortDateString();
            String web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday).Date.ToShortDateString();
            String thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday).Date.ToShortDateString();
            String fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday).Date.ToShortDateString();

            var populationList = empService.getTimeSheetSummaryForChart(monday, tuesday, web, thu, fri);
            List<ChartData> cd = new List<ChartData>();

            return populationList;

        }
        public List<ChartData> getLastWeekChartData()
        {
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
            DateTime web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
            DateTime thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
            DateTime fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);


            String m = monday.AddDays(-7).ToShortDateString();
            String t = tuesday.AddDays(-7).ToShortDateString();
            String w = web.AddDays(-7).ToShortDateString();
            String th = thu.AddDays(-7).ToShortDateString();
            String f = fri.AddDays(-7).ToShortDateString();

            var populationList = empService.getTimeSheetSummaryForChart(m, t, w, th, f);
            List<ChartData> cd = new List<ChartData>();

            return populationList;

        }

        public List<ChartData> getNextWeekChartData()
        {
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday);
            DateTime web = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday);
            DateTime thu = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday);
            DateTime fri = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday);


            String m = monday.AddDays(+7).ToShortDateString();
            String t = tuesday.AddDays(+7).ToShortDateString();
            String w = web.AddDays(+7).ToShortDateString();
            String th = thu.AddDays(+7).ToShortDateString();
            String f = fri.AddDays(+7).ToShortDateString();

            var populationList = empService.getTimeSheetSummaryForChart(m, t, w, th, f);
            List<ChartData> cd = new List<ChartData>();

            return populationList;

        }


    }
}