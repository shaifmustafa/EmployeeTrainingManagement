using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels
{
    public class Billed
    {
        public Int64 Id { get; set; }
        public string MemoNo { get; set; }
        public DateTime BillDate { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string BillStatus { get; set; }
        public Int64 AdvanceId { get; set; }
        [ForeignKey("AdvanceId")]
        public virtual Advance Advance { get; set; }
        public decimal GrandTotal { get; set; }
        public virtual ICollection<BilledDetails> BilledDetails { get; set; }
    }
}
