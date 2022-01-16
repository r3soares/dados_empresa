using Realms;
using src.Respositories.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Respositories
{
    public interface IRepository<T>
    { 
        public Task<T> GetById(object id);
        public Task<IQueryable<T>> GetAll();
        public Task<object> Save(T data);
        public Task<object> Update(T data);
        public Task<object> Delete(object id);
    }
}
