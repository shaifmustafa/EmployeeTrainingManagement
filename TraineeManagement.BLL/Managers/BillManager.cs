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
    public class BillManager : Manager<Billed>
    {
        public BillManager() : base(new BilledRepository())
        {

        }


        public override bool SaveOrUpdate(Billed entity)
        {
            AdjustManager adjustManager = new AdjustManager();

            Adjust adjustInfo = new Adjust();

            adjustInfo.AdvanceId = entity.AdvanceId;

            var adjustExist = adjustManager.GetAll().Where(x => x.AdvanceId == adjustInfo.AdvanceId).LastOrDefault();

            

            var exist = Repository.GetById(entity.Id);            


            if (exist != null)
            {
                entity.GrandTotal = exist.GrandTotal;
                entity.BillStatus = exist.BillStatus;
            }

            else
            {
                entity.BillStatus = "Not Confirmed";
            }


            Repository.SaveOrUpdate(entity);
            Repository.Done();

            if (adjustExist == null)
            {
                adjustInfo.AdvanceId = entity.AdvanceId;
                adjustInfo.BillingId = entity.Id;
                adjustInfo.BillTotal = entity.GrandTotal;
            }

            else
            {
                adjustInfo.Id = adjustExist.Id;
                adjustInfo.AdvanceId = adjustExist.AdvanceId;
                adjustInfo.BillingId = adjustExist.BillingId;

                adjustInfo.BillTotal = adjustExist.BillTotal + entity.GrandTotal;
            }

            adjustManager.SaveOrUpdate(adjustInfo);
            Repository.Done();

            return true;
        }


        public bool Confrim(Int64 id)
        {
            var exist = Repository.GetById(id);
            exist.BillStatus = "Confirmed";

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
