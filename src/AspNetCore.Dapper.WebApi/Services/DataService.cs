using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.DAO;
using AspNetCore.Dapper.WebApi.Utils.Factory;

namespace AspNetCore.Dapper.WebApi.Services
{
    public class DataService
    {
        private readonly IDbConnectionFactory dbConnectioFactory;

        public DataService(
            IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectioFactory = dbConnectionFactory;
        }

        private async Task<IEnumerable<User>> GetUsersAsync()
        {
            string sql = @"";
            throw new NotImplementedException();
        }
        private async Task MergeUsersAsync()
        { 
            throw new NotImplementedException();
        }

        internal async Task<int> SyncAsync()
        {
            throw new NotImplementedException();
        }
    }
}
