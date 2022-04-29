using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ExamenDisenno.Service.DataAccess
{
    public class ConnectionManager : IConnectionManager
    {
        public const string ConnectionString = "PUNTO_DE_VENTA";
        private readonly IConfiguration configuration = null;

        public ConnectionManager()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            configuration = builder.Build();
        }

        public IDbConnection GetConnection(string key)
        {
            string conn = ConfigurationExtensions.GetConnectionString(configuration, $"{key}");
            return new SqlConnection(conn);
        }
    }
}