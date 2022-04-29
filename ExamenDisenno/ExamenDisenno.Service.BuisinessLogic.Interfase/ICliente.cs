using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.BuisinessLogic.Interfase
{
    public interface ICliente
    {
        List<Model.Cliente> GetClientes();
        void AddCliente(Model.Cliente cliente);
        void UpdateCliente(Model.Cliente cliente);
        void UpdateStateCliente(Model.Cliente cliente);
        Task<string> GetTimeDifference(int cedula);
        Model.Cliente GetClienteByCedula(int cedula);
    }
}
