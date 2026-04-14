using System.Data;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.Enum;

namespace AspNetCore.Dapper.WebApi.Utils.Factory;

/// <summary>
/// DB connection factory
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Create a DB connection
    /// </summary>
    /// <param name="dbType">DB type</param>
    /// <param name="dbConnection">DB connection name</param>
    /// <returns>IDbConnection</returns>
    Task<IDbConnection> CreateAsync(EnumDbType dbType, string dbConnection);
}
