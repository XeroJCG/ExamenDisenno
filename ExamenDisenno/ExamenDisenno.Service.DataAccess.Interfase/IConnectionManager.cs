using System;
using System.Data;

namespace ExamenDisenno.Service.DataAccess
{
    public interface IConnectionManager
    {
        IDbConnection GetConnection(string key); 
    }
}