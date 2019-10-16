using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeManagement.Repository.Base;

namespace TraineeManagement.BLL.Base
{
    public class Manager<T> where T : class
    {
        protected Repository<T> Repository;


        public Manager(Repository<T> repository)
        {
            Repository = repository;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual T GetById(Int64 id)
        {
            return Repository.GetById(id);
        }

        public virtual T GetByMemo(string id)
        {
            return Repository.GetByMemo(id);
        }

        public virtual bool Save(T entity)
        {
            return Repository.Save(entity);
        }

        public virtual bool Update(T entity)
        {
            return Repository.Update(entity);
        }
        public bool Remove(T entity)
        {
            return Repository.Remove(entity);
        }

        public virtual bool SaveOrUpdate(T entity)
        {
            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }

        public virtual bool SaveOrUpdateDelete(T entity)
        {
            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }
        public virtual void SaveOrUpdateList(params T[] entity)
        {
            Repository.SaveOrUpdate(entity);
        }
        public virtual bool Done()
        {
            return Repository.Done();
        }

        public virtual bool Edit(T entity)
        {
            return true;
        }
    }
}
