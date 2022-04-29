using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.DataAccess.Interfase
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
