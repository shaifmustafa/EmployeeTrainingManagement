using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeManagement.BLL.Base;
using TraineeManagement.Models.EntityModels;
using TraineeManagement.Repository.Repositories;

namespace TraineeManagement.BLL.Managers
{
    public class DistrictManager : Manager<District>
    {
        public DistrictManager() : base(new DistrictRepository())
        {

        }




    }
}
