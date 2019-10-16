using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels.ViewModels
{
    [NotMapped]
    public class AdvanceVM : Advance
    {        
        public Int64 BillId { get; set; }
        public string BillMemo { get; set; }
        public DateTime BillDate { get; set; }
        public string BillDateText { get { return BillDate.ToString("yyyy-MM-dd"); } }
        public string BillVendor { get; set; }
        public string BillDescription { get; set; }

        public Int64 BillDetailsId { get; set; }
        public Int64 BillPurposeId { get; set; }

        public string BillPurposeName { get; set; }
        public decimal BillQty { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillTotal { get; set; }
        public decimal BillGrandTotal { get; set; }

        public decimal Reimbursement { get { return GrandTotal - BillGrandTotal; } }

        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public Int64 GradeId { get; set; }
        public string EmployeeGrade { get; set; }
        public string DistrictName { get; set; }
        public string SubDistrictName { get; set; }
        public Int64 AdvanceDetailsId { get; set; }
        public Int64 PurposeId { get; set; }
        public string PurposeName { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }        
        public string FromDateText { get { return FromDate.ToString("yyyy-MM-dd"); } }
        public string ToDateText { get { return ToDate.ToString("yyyy-MM-dd"); } }

    }
}
