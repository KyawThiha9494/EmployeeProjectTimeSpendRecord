using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecord.Models
{
    public class EmpTask
    {
        /*taskName
        taskDesc
        projName
        clientName
        date
        startTime
        endTime
        duration*/
        [Key]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(100)]
        public String Task_Name { get; set; }
        [Required]
        [MaxLength(100)]
        public String Task_Description { get; set; }
        [Required]
        [MaxLength(100)]
        public String Client_Name { get; set; }
        public String Date { get; set; }
        public String Start_Time { get; set; }
        public String End_Time { get; set; }
        public String Duration { get; set; }

        public virtual Project project { get; set; }
    }
}
