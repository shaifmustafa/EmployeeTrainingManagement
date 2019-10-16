using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeManagement.Models.EntityModels;
using TraineeManagement.Repository.Base;
using TraineeManagement.Repository.DatabaseContext;

namespace Trainee_Assignment.Repository.Repositories
{
    public class SubDistrictRepository : Repository<SubDistrict>
    {
        public SubDistrictRepository() : base(new TraineeManagementDbContext())
        {
        }
    }
}
