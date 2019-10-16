using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeManagement.Models.EntityModels;
using TraineeManagement.Repository.Base;
using TraineeManagement.Repository.DatabaseContext;

namespace TraineeManagement.Repository.Repositories
{
    public class AdvanceDetailsRepository : Repository<AdvanceDetails>
    {
        public AdvanceDetailsRepository() : base(new TraineeManagementDbContext())
        {
        }
    }
}
