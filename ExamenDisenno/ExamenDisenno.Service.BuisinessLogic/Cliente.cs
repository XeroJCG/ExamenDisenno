using ExamenDisenno.Service.BuisinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.BuisinessLogic
{
    public class Cliente : ICliente
    {
        public static DataAccess.Interfase.ICliente DACliente;

        public Cliente()
        {
            DACliente = new DataAccess.Cliente();
        }

        public void AddCliente(Model.Cliente cliente)
        {
            DACliente.AddCliente(cliente);
        }

        public Model.Cliente GetClienteByCedula(int cedula)
        {
            return DACliente.GetClienteByCedula(cedula);
        }

        public List<Model.Cliente> GetClientes()
        {
            return DACliente.GetClientes();
        }

        public async Task<string> GetTimeDifference(int cedula)
        {
            return await DACliente.GetTimeDifference(cedula);
        }

        public void UpdateCliente(Model.Cliente cliente)
        {
            DACliente.UpdateCliente(cliente);
        }

        public void UpdateStateCliente(Model.Cliente cliente)
        {
            DACliente.UpdateStateCliente(cliente);
        }
    }
}
