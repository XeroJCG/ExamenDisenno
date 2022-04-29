using ExamenDisenno.Service.BuisinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.BuisinessLogic
{
    public class Bitacora : IBitacora
    {
        public static DataAccess.Interfase.IBitacora DABitacora;

        public Bitacora()
        {
            DABitacora = new DataAccess.Bitacora();
        }

        public List<Model.Bitacora> GetAllBitacoras()
        {
            return DABitacora.GetAllBitacoras();
        }

        public List<Model.Bitacora> GetAllBitacorasById(int cedula)
        {
            return DABitacora.GetAllBitacorasById(cedula);
        }
    }
}
