using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeManagement.BLL.Base;
using TraineeManagement.Models.EntityModels;
using TraineeManagement.Models.EntityModels.ViewModels;
using TraineeManagement.Repository.Repositories;

namespace TraineeManagement.BLL.Managers
{
    public class AdvanceManager : Manager<Advance>
    {
        EmployeeManager employeeManager = new EmployeeManager();

        public AdvanceManager() : base(new AdvanceRepository())
        {

        }


        public override bool SaveOrUpdate(Advance entity)
        {
            var exist = Repository.GetById(entity.Id);            
            

            if (exist != null)
            {
                entity.GrandTotal = exist.GrandTotal;
                entity.AdvanceStatus = exist.AdvanceStatus;
            }
                
            else
            {
                entity.AdvanceStatus = "Not Confirmed";         
            }

            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }


        public bool Confrim(Int64 id)
        {
            var exist = Repository.GetById(id);
            exist.AdvanceStatus = "Confirmed";

            Repository.SaveOrUpdate(exist);

            return Repository.Done();
        }


        public bool Delete(Int64 id)
        {
            var exist = Repository.GetById(id);

            Repository.Remove(exist);

            return Repository.Done();
        }

    }
}
