using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecord.Models
{
    public class Project
    {
        [Key]
        public int projectId { get; set; }
        [Required]
        [MaxLength(100)]
        public String projectName { get; set; }
        
        public virtual List<EmpTask> EmpTask { get; set; }
    }
}
