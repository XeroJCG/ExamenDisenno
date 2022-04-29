using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.BuisinessLogic.Interfase
{
    public interface IBitacora
    {
        List<Model.Bitacora> GetAllBitacoras();
        List<Model.Bitacora> GetAllBitacorasById(int cedula);
    }
}
