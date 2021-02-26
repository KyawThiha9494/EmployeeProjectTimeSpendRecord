using EmployeeRecord.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeRecord.Services
{
    public class TimeSheetService
    {
        private readonly EmployeeContext _dbContext;

        public TimeSheetService(EmployeeContext employeeContext)
        {
            this._dbContext = employeeContext;
        }

        
        public List<TaskDTO> getTimeSheetSummary(String monday,String tuesday,String web,String thu,String fri)
        {
            List<TaskDTO> dtoList = new List<TaskDTO>();
            List<EmpTask> empTaskList = new List<EmpTask>();
            List<Project> projList = new List<Project>();

            string m = changeDateFormat(monday);
            string t = changeDateFormat(tuesday);
            string w = changeDateFormat(web);
            string th = changeDateFormat(thu);
            string f = changeDateFormat(fri);

            List<Project> pp = new List<Project>();


            pp = _dbContext.project.ToList();

            foreach(Project p in pp)
            {
                TaskDTO tDto = new TaskDTO();
                var total = new TimeSpan(0, 0, 0);
                var flag = false;
                empTaskList = _dbContext.EmpTask.Where(x =>
                (x.Date == m || x.Date == t || x.Date == w || x.Date == th || x.Date == f) && (x.project.projectId == p.projectId)).ToList();
                foreach(EmpTask et in empTaskList)
                {
                    
                    flag = true;
                    tDto.projectName = et.project.projectName;
                    if (et.Date == m)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.monDuration = tt.ToString();
                    }
                    else if (et.Date == t)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.tueDuration = tt.ToString();
                    }
                    else if (et.Date == w)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.webDuration = tt.ToString();
                    }
                    else if (et.Date == th)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.thuDuration = tt.ToString();
                    }
                    else if (et.Date == f)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.friDuration = tt.ToString();
                    }
                    //dtoList.Add(tDto);
                }
                if (flag)
                {
                  tDto.total = total.ToString();
                  dtoList.Add(tDto);
                }
                
            }

            return dtoList;
        }

        public EmpTask getEmpTaskById(int id)
        {
            EmpTask task = new EmpTask();
            task = _dbContext.EmpTask.Where(x => x.TaskId == id).FirstOrDefault();
            return task;
        }

        public TimeSpan calculateDuration(String startt,String endt)
        {
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            start = Convert.ToDateTime(startt);
            end = Convert.ToDateTime(endt);
            TimeSpan span = end.Subtract(start);
            //string duration = span.ToString();
            return span;
        }

        public String changeDateFormat(String date)
        {
            string d = "";
            string[] temp = date.Split("/");
            if(temp[0].Length < 2 && temp[1].Length <2){

                d = temp[2] + "-0" + temp[1] + "-0" + temp[0];

            } else if (temp[1].Length < 2)
            {
                d = temp[2] + "-0" + temp[1] + "-" + temp[0];
            }else if (temp[2].Length < 2)
            {
                d = temp[2] + "-" + temp[1] + "-0" + temp[0];
            }
            else
            {
                d = temp[2] + "-" + temp[1] + "-" + temp[0];
            }

            return d;
        }

        

        public List<PopulationModel> GetCityPopulationList()
        {
            var list = new List<PopulationModel>();
            list.Add(new PopulationModel { CityName = "PURI", PopulationYear2020 = 28000, PopulationYear2010 = 45000, PopulationYear2000 = 22000, PopulationYear1990 = 50000 });
            list.Add(new PopulationModel { CityName = "Bhubaneswar", PopulationYear2020 = 30000, PopulationYear2010 = 49000, PopulationYear2000 = 24000, PopulationYear1990 = 39000 });
            list.Add(new PopulationModel { CityName = "Cuttack", PopulationYear2020 = 35000, PopulationYear2010 = 56000, PopulationYear2000 = 26000, PopulationYear1990 = 41000 });
            list.Add(new PopulationModel { CityName = "Berhampur", PopulationYear2020 = 37000, PopulationYear2010 = 44000, PopulationYear2000 = 28000, PopulationYear1990 = 48000 });
            list.Add(new PopulationModel { CityName = "Odisha", PopulationYear2020 = 40000, PopulationYear2010 = 38000, PopulationYear2000 = 30000, PopulationYear1990 = 54000 });

            return list;

        }

        public List<ChartData> getTimeSheetSummaryForChart(String monday, String tuesday, String web, String thu, String fri)
        {
            List<ChartData> dtoList = new List<ChartData>();
            List<EmpTask> empTaskList = new List<EmpTask>();
            List<Project> projList = new List<Project>();

            string m = changeDateFormat(monday);
            string t = changeDateFormat(tuesday);
            string w = changeDateFormat(web);
            string th = changeDateFormat(thu);
            string f = changeDateFormat(fri);

            List<Project> pp = new List<Project>();


            pp = _dbContext.project.ToList();

            foreach (Project p in pp)
            {
                ChartData tDto = new ChartData();
                var total = new TimeSpan(0, 0, 0);
                var flag = false;
                empTaskList = _dbContext.EmpTask.Where(x =>
                (x.Date == m || x.Date == t || x.Date == w || x.Date == th || x.Date == f) && x.project.projectId == p.projectId).ToList();
                foreach (EmpTask et in empTaskList)
                {

                    flag = true;
                    tDto.projectName = et.project.projectName;
                    if (et.Date == m)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.monDuration = ToInt(tt);
                    }
                    else if (et.Date == t)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.tueDuration = ToInt(tt);
                    }
                    else if (et.Date == w)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.webDuration = ToInt(tt);
                    }
                    else if (et.Date == th)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.thuDuration = ToInt(tt);
                    }
                    else if (et.Date == f)
                    {
                        TimeSpan tt = calculateDuration(et.Start_Time, et.End_Time);
                        total = total.Add(tt);
                        tDto.friDuration = ToInt(tt);
                    }
                    //dtoList.Add(tDto);
                }
                if (flag)
                {
                    //tDto.total = total.ToString();
                    dtoList.Add(tDto);
                }

            }

            return dtoList;
        }

        public int ToInt(TimeSpan span)
        {
            decimal spanSecs = (span.Hours * 3600) + (span.Minutes * 60) + span.Seconds;
            decimal spanPart = spanSecs / 86400M;
            decimal result = span.Days + spanPart;
            int d = Convert.ToInt32(Math.Ceiling(result));
            return d;
        }
    }
}
