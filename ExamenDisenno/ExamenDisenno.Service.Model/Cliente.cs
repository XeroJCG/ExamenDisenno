using Newtonsoft.Json;
using System;

namespace ExamenDisenno.Service.Model
{
    public class Cliente
    {
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Estado { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public DateTime Fecha_Creado { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
