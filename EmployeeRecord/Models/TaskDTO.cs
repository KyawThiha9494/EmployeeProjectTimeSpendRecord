using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecord.Models
{
    public class TaskDTO
    {
        public String projectName { get; set; }
        public String monDuration { get; set; }
        public String tueDuration { get; set; }
        public String webDuration { get; set; }
        public String thuDuration { get; set; }
        public String friDuration { get; set; }
        public String total { get; set; }

    }
}
