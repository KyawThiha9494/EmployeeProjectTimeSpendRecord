using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecord.Models
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { 
            
        }

        public DbSet<EmpTask> EmpTask { get; set; }
        public DbSet<Project> project { get; set; }


    }
}
