using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels.ViewModels
{
    [NotMapped]
    public class BillVM : Billed
    {        
        public string EmployeeName { get; set; }        
        public Int64 EmployeeId { get; set; }
        public Int64 BillDetailsId { get; set; }
        public Int64 PurposeId { get; set; }
        public string PurposeName { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public string BillDateText { get { return BillDate.ToString("yyyy-MM-dd"); } }        
    }
}
