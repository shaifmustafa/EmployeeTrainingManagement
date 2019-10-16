using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee_Assignment.Repository.Repositories;
using TraineeManagement.BLL.Base;
using TraineeManagement.Models.EntityModels;

namespace TraineeManagement.BLL.Managers
{
    public class SubDistrictManager : Manager<SubDistrict>
    {
        public SubDistrictManager() : base(new SubDistrictRepository())
        {

        }




    }
}
