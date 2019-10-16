using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels.ViewModels
{
    [NotMapped]
    public class AdjustVM : Adjust
    {        
        public string AdvanceMemo { get; set; }
        public decimal AdvanceTotal { get; set; }                
        public decimal Reimbursement { get { return AdvanceTotal - BillTotal; } }
        public Int64 EmployeeId { get; set; }
        public string EmployeeName { get; set; }

    }
}
