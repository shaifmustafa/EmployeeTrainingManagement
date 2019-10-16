using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeManagement.Models.EntityModels
{
    public class AdvanceDetails
    {
        public Int64 Id { get; set; }
        public Int64 AdvanceId { get; set; }        
        public virtual Advance Advance { get; set; }

        public Int64 PurposeId { get; set; }
        [ForeignKey("PurposeId")]
        public virtual Purpose Purpose { get; set; }

        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}
