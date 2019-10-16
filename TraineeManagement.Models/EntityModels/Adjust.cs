using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels
{
    public class Adjust
    {
        public Int64 Id { get; set; }
        public Int64 BillingId { get; set; }
        [ForeignKey("BillingId")]
        public virtual Billed Billed { get; set; }        
        public decimal BillTotal { get; set; }

        public Int64? AdvanceId { get; set; }
    }
}
