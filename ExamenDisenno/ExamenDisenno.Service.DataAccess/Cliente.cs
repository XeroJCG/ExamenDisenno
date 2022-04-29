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
    public class Cliente : ICliente
    {
        private readonly IConnectionManager connnectionManager;

        public Cliente()
        {
            connnectionManager = new ConnectionManager();
        }

        public void AddCliente(Model.Cliente cliente)
        {
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_CrearCliente", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CEDULA", cliente.Cedula));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", cliente.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@APELLIDOS", cliente.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONO", cliente.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", cliente.Email));

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Model.Cliente GetClienteByCedula(int cedula)
        {
            Model.Cliente cliente = null;
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM CLIENTE WHERE CEDULA = {cedula}", (SqlConnection)connection);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cliente = new Model.Cliente()
                        {
                            Cedula = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellidos = reader.GetString(2),
                            Estado = reader.GetString(3),
                            Telefono = reader.GetInt32(4),
                            Email = reader.GetString(5),
                            Fecha_Creado = reader.GetDateTime(6)

                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cliente;
        }

        public List<Model.Cliente> GetClientes()
        {
            var list = new List<Model.Cliente>();
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CLIENTE", (SqlConnection)connection);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Model.Cliente
                        {
                            Cedula = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellidos = reader.GetString(2),
                            Estado = reader.GetString(3),
                            Telefono = reader.GetInt32(4),
                            Email = reader.GetString(5),
                            Fecha_Creado = reader.GetDateTime(6)

                        });
                    }
                }
            }catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public async Task<string> GetTimeDifference(int cedula)
        {
            string respuesta = "";
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT  dbo.fn_ObtenerDiferenciaDeTiempo({cedula})", (SqlConnection)connection);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta = reader.GetString(0);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }

        public void UpdateCliente(Model.Cliente cliente)
        {
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarUsuario", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CEDULA", cliente.Cedula));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", cliente.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@APELLIDOS", cliente.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONO", cliente.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", cliente.Email));

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStateCliente(Model.Cliente cliente)
        {
            try
            {
                using (var connection = this.connnectionManager.GetConnection(ConnectionManager.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_CambiarEstadoCliente", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CEDULA", cliente.Cedula));
                    cmd.Parameters.Add(new SqlParameter("@ESTADO", cliente.Estado));
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
