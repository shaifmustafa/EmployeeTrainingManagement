using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels
{
    public class Advance
    {
        public Int64 Id { get; set; }
        public string AdvanceType { get; set; } // Tarvelling or Training
        public string MemoNo { get; set; } // Auto-Generate
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public Int64 EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public decimal GrandTotal { get; set; }

        public Int64? SubDistrictId { get; set; }
        [ForeignKey("SubDistrictId")]
        public virtual SubDistrict SubDistrict { get; set; }

        public Int64? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        public string Trainer { get; set; }
        public string Topic { get; set; }
        public string AdvanceStatus { get; set; }
        public virtual ICollection<AdvanceDetails> AdvanceDetails { get; set; }
    }
}
