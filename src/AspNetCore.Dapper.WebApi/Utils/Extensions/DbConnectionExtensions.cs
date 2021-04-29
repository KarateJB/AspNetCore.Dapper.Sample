using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace AspNetCore.Dapper.WebApi.Utils.Extensions
{
    /// <summary>
    /// DbConnection extensions
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Query first record
        /// </summary>
        /// <typeparam name="T">DAO type</typeparam>
        /// <param name="db">IDbConnection</param>
        /// <param name="sp">Sql</param>
        /// <param name="parms">Parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>Query result</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection db, string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).FirstOrDefault();
        }

        /// <summary>
        /// Query by store procedure
        /// </summary>
        /// <typeparam name="T">DAO type</typeparam>
        /// <param name="db">IDbConnection</param>
        /// <param name="sp">Sql</param>
        /// <param name="parms">Parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>Query result</returns>
        public static async Task<List<T>> GetAllAsync<T>(this IDbConnection db, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <typeparam name="T">DAO type</typeparam>
        /// <param name="db">IDbConnection</param>
        /// <param name="sp">Sql</param>
        /// <param name="parms">Parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>The record</returns>
        public static async Task<T> InsertAsync<T>(this IDbConnection db, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                using var tran = db.BeginTransaction();
                try
                {
                    result = (await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Update a recored
        /// </summary>
        /// <typeparam name="T">DAO type</typeparam>
        /// <param name="db">IDbConnection</param>
        /// <param name="sp">Sql</param>
        /// <param name="parms">Parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>The record</returns>
        public static async Task<T> UpdateAsync<T>(this IDbConnection db, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                using var tran = db.BeginTransaction();
                try
                {
                    result = (await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <typeparam name="T">DAO</typeparam>
        /// <param name="db">IDbConnection</param>
        /// <param name="sp">Sql</param>
        /// <param name="parms">Parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>Affected rows count</returns>
        public static async Task<int> DeleteAsync<T>(this IDbConnection db, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            int cnt = 0;
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                using var trans = db.BeginTransaction();

                try
                {
                    cnt = await db.ExecuteAsync(sp, parms, commandType: commandType);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return cnt;
        }
    }
}
