using ExamenDisenno.Service.DataAccess.Interfase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.DataAccess
{
    public class Bitacora: IBitacora
    {
        private readonly IConnectionManager connnectionManager;

        public Bitacora()
        {
            connnectionManager = new ConnectionManager();
        }

        public List<Model.Bitacora> GetAllBitacoras()
        {
            var list = new List<Model.Bitacora>();
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM BITACORA", (SqlConnection)connection);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Model.Bitacora
                        {
                            Id = reader.GetInt32(0),
                            Cedula_Cliente = reader.GetInt32(1),
                            Estado_Actual = reader.GetString(2),
                            Descripcion_Cambio = reader.GetString(3),
                            Fecha_Cambio = reader.GetDateTime(4)

                        });
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public List<Model.Bitacora> GetAllBitacorasById(int cedula)
        {
            var list = new List<Model.Bitacora>();
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM BITACORA WHERE CEDULA_CLIENTE = {cedula}", (SqlConnection)connection);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Model.Bitacora
                        {
                            Id = reader.GetInt32(0),
                            Cedula_Cliente = reader.GetInt32(1),
                            Estado_Actual = reader.GetString(2),
                            Descripcion_Cambio = reader.GetString(3),
                            Fecha_Cambio = reader.GetDateTime(4)

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
    }
}
