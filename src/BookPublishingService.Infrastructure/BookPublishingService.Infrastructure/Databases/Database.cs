using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BookPublishingService.Infrastructure.Databases
{
    public interface IDatabase
    {
        string ConnectionString { get; }
        Task<TResult> QueryValue<TResult>(string sql, object args = null, bool isStoreProcedure = false, int timeout = 30);
        Task<List<TResult>> Query<TResult>(string sql, object args = null, bool isStoreProcedure = false, int timeout = 30);
        Task<int> Execute(string sql, object args = null, bool isStoreProcedure = false, int timeout = 30,string connection = null);
    }

    public class Database : IDatabase
    {
        private  IDatabaseConnection _connection;

        public Database(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public string ConnectionString => _connection.ConnectionString;

        public async Task<TResult> QueryValue<TResult>(string sql, object args = null, bool isStoreProcedure = false, int timeout = 30)
        {
            using (var con = await _connection.Open())
            {
                return await con.ExecuteScalarAsync<TResult>(sql, args, commandType: isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text, commandTimeout: timeout);
            }
        }

        public async Task<List<TResult>> Query<TResult>(string sql, object args = null, bool isStoreProcedure = false, int timeout = 30)
        {
            try
            {
                using (var con = await _connection.Open())
                {
                    return (List<TResult>)await con.QueryAsync<TResult>(sql, args, commandType: isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text, commandTimeout: timeout);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
         
            
        }

        public async Task<int> Execute(string sql, object args = null, bool isStoreProcedure = false, int timeout = 30,string connection = null)
        {
            try
            {
                
                if (connection != null)
                {
                    _connection.ConnectionString = connection;
                }
               
                using (var con = await _connection.Open())
                {
                    return await con.ExecuteAsync(sql, args, commandType: isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text, commandTimeout: timeout);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

    }
}
