using System;
using System.Data;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.Config;
using AspNetCore.Dapper.WebApi.Models.Enum;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AspNetCore.Dapper.WebApi.Utils.Factory;

/// <summary>
/// DbConnection factory
/// </summary>
public class DbConnectionFactory(IOptions<AppSettings> configuration) : IDbConnectionFactory
{
    private readonly AppSettings appSettings = configuration.Value;

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
