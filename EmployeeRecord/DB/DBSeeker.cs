using EmployeeRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecord.DB
{
    public class DBSeeker
    {
        public DBSeeker(EmployeeContext dbContext)
        {
            // Adding Stationery Data

            Project pro1 = new Project();
            pro1.projectName = "Project1";

            Project pro2 = new Project();
            pro2.projectName = "Project2";

            Project pro3 = new Project();
            pro3.projectName = "Project3";

            dbContext.Add(pro1);
            dbContext.Add(pro2);
            dbContext.Add(pro3);

            /*EmpTask et1 = new EmpTask();
            et1.Task_Name = "Task001";
            et1.Task_Description = "Desc001";
            et1.Client_Name = "Client001";
            et1.project = pro1;

            dbContext.Add(et1);*/
            

            // Saving Changes

            dbContext.SaveChanges();

        }
    }
}
