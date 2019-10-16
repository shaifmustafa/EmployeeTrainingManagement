using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels.ViewModels
{
    [NotMapped]
    public class GradeVM : Employee
    {        
        public string GradeName { get; set; }
    }
}
