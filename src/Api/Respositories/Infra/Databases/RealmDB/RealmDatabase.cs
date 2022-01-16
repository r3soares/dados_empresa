using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace src.Respositories.Infra.Databases.RealmDB
{
    class RealmDatabase<T> : IRepository<T>
        where T : RealmObject
        
    {
        static bool isCompactado = false;
        private readonly RealmConfigurationBase _configuration;
        private async Task<Realm> Database() { return await Realm.GetInstanceAsync(_configuration); }
        public RealmDatabase(string databaseName, bool persist = true)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Databases");
            string path = Path.Combine(folder, databaseName);
            Directory.CreateDirectory(folder);
            _configuration = persist ? new RealmConfiguration(path) : new InMemoryConfiguration(databaseName);
            if(!isCompactado && persist)
            {                
                isCompactado = Realm.Compact(_configuration);
            }
        }

        public async Task<object> Delete(object id)
        {
            var data = await GetById(id);
            var realm = await Database();
            try
            {
                await realm.WriteAsync((r) => r.Remove(data));
                return true;
            }
            catch
            {

                return false;
            }
            
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return (await Database()).All<T>();
        }

        public async Task<T> GetById(object id) => id switch
        {
            long d      => (await Database()).Find<T>(d),
            int d       => (await Database()).Find<T>(d),
            string d    => (await Database()).Find<T>(d),
            Guid d      => (await Database()).Find<T>(d),
            _ => null
        };

        public async Task<object> Save(T data)
        {
            var realm = await Database();
            try
            {
                await realm.WriteAsync((r) => r.Add(data, true));
                return true;
            }
            catch
            {

                return false;
            }
            
        }

        public async Task<object> Update(T data)
        {
            var realm = await Database();
            try
            {
                await realm.WriteAsync((r) => r.Add(data, true));
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
