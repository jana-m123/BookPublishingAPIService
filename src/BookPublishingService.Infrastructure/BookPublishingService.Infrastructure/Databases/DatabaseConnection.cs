using BookPublishingService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BookPublishingService.Infrastructure.Databases
{
    public interface IDatabaseConnection
    {
        string ConnectionString { get; set; }
        Task<IDbConnection> Open();
    }

    public class DatabaseConnection : IDatabaseConnection
    {
        public string ConnectionString { get; set; }

        public SqlConnection _conection;       
        public IOptions<AppSettings> settings;

        public DatabaseConnection(string connectionString)
        {
            ConnectionString = connectionString;           
        }

        public DatabaseConnection(IOptions<AppSettings> settings)
            : this(settings.Value.SqlConnectionString)
        { }


        public async Task<IDbConnection> Open()
        {
            try
            {
                _conection = new SqlConnection(ConnectionString);

                await _conection.OpenAsync();
                return _conection;

            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message, ex.InnerException);
                throw new Exception(ex.Message, ex.InnerException);

            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _conection?.Dispose();
        }
    }
}
