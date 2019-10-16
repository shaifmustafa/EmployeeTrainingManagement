using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels
{
    public class Employee
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }        
        public string Code { get; set; }

        public Int64 GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

    }
}
