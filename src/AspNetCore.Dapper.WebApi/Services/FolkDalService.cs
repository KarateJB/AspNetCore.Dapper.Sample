using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.Config;
using AspNetCore.Dapper.WebApi.Models.DAO;
using AspNetCore.Dapper.WebApi.Models.Enum;
using AspNetCore.Dapper.WebApi.Utils.Extensions;
using AspNetCore.Dapper.WebApi.Utils.Factory;
using Dapper;

namespace AspNetCore.Dapper.WebApi.Services
{
    public class FolkDalService
    {
        private readonly IDbConnectionFactory dbConnectioFactory;

        public FolkDalService(
            IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectioFactory = dbConnectionFactory;
        }

        private async Task<IEnumerable<Folk>> GetAsync(string startDate, string endDate)
        {
            var db = await this.dbConnectioFactory.CreateAsync(EnumDbType.SqlServer, nameof(ConnectionStringOptions.Demo_MSSQL));
            string sql = SqlFactory.CreateQueryFolk(EnumDbType.SqlServer);

            var parms = new DynamicParameters();
            parms.Add("@StartDate", startDate ??= DateTime.UtcNow.ToString("yyyy-MM-dd"), DbType.String, ParameterDirection.Input);
            parms.Add("@EndDate", endDate ??= DateTime.MaxValue.ToString("yyyy-MM-dd"), DbType.String, ParameterDirection.Input);

            var folks = await db.GetAllAsync<Folk>(sql, parms, CommandType.Text);
            return folks;
        }

        private async Task<int> MergeAsync(IEnumerable<Folk> folks)
        {
            if (folks is null)
                return 0;

            int totalMergedCnt = 0;
            var db = await this.dbConnectioFactory.CreateAsync(EnumDbType.Postgres, nameof(ConnectionStringOptions.Demo_PG));
            string sql = SqlFactory.CreateMergeFolk(EnumDbType.Postgres);

            foreach (var folk in folks)
            {
                var parms = new DynamicParameters();

                // Use reflection to set SQL parameters.
                //foreach (var prop in folk.GetType().GetProperties())
                //{
                //    var propName = prop.Name;
                //    var propValue = folk.GetType().GetProperty(propName).GetValue(folk);
                //    parms.Add($"@{propName}", propValue, DbType.String, ParameterDirection.Input);
                //}

                parms.Add($"@{nameof(Folk.Id)}", folk.Id, DbType.Guid, ParameterDirection.Input);
                parms.Add($"@{nameof(Folk.Name)}", folk.Name, DbType.String, ParameterDirection.Input);
                parms.Add($"@{nameof(Folk.Phone)}", folk.Phone, DbType.String, ParameterDirection.Input);
                parms.Add($"@{nameof(Folk.Address)}", folk.Address, DbType.String, ParameterDirection.Input);
                parms.Add($"@{nameof(Folk.Birthday)}", folk.Birthday, DbType.DateTimeOffset, ParameterDirection.Input);
                parms.Add($"@{nameof(Folk.EmploymentOn)}", folk.EmploymentOn, DbType.DateTimeOffset, ParameterDirection.Input);

                try
                {
                    int mergedCnt = await db.ExecuteAsync(sql, parms, commandType: CommandType.Text);
                    totalMergedCnt += mergedCnt;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return totalMergedCnt;
        }

        internal async Task<int> SyncAsync(string startDate, string endDate)
        {
            var folks = await this.GetAsync(startDate, endDate);
            var totalMergedCnt = await this.MergeAsync(folks);
            return totalMergedCnt;
        }
    }
}
