using MongoDB.Driver;  
using Events.Tenancy.Services.Infra.DB.Config;  
using Events.Tenancy.Services.Domain.Entities;
using System.Collections.Generic;  
using System.Linq;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public class TenantDbService : ITenantDbService
    {
        private readonly IMongoCollection<TenantModel> _tenants;
        public TenantDbService(IDatabaseConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tenants = database.GetCollection<TenantModel>(settings.TenantCollectionName);
        }

        /// <summary>
        /// Method to fetch user data from db
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public TenantModel Get(string username) { 
            return _tenants.Find<TenantModel>(tenant => tenant.Username == username).FirstOrDefault();
        }
    }
}