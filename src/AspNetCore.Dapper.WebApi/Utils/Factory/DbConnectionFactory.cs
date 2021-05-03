using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.Config;
using AspNetCore.Dapper.WebApi.Models.Enum;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AspNetCore.Dapper.WebApi.Utils.Factory
{
    /// <summary>
    /// DbConnection factory
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly AppSettings appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        public DbConnectionFactory(IOptions<AppSettings> configuration)
        {
            this.appSettings = configuration.Value;
        }

        /// <summary>
        /// Create DbConnection
        /// </summary>
        /// <param name="dbType">DB type</param>
        /// <param name="dbConnection">DB connection name</param>
        /// <returns>IDbConnection instance</returns>
        public async Task<IDbConnection> CreateAsync(EnumDbType dbType, string dbConnection)
        {
            string connectionString = this.appSettings.ConnectionStrings.GetType().GetProperty(dbConnection).GetValue(this.appSettings.ConnectionStrings, null) as string;
            switch (dbType)
            {
                case EnumDbType.SqlServer:
                    return await Task.FromResult(new SqlConnection(connectionString));
                case EnumDbType.Postgres:
                    return await Task.FromResult(new NpgsqlConnection(connectionString));
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
