using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenDisenno.Service.Model
{
    public class Bitacora
    {
        public int Id { get; set; }
        public int Cedula_Cliente { get; set; }
        public string Estado_Actual { get; set; }
        public string Descripcion_Cambio { get; set; }
        public DateTime Fecha_Cambio { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
