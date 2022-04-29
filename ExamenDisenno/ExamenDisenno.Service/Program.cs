using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenDisenno.Service
{
    internal class Program
    {
        public static Service.BuisinessLogic.Interfase.ICliente Cliente;
        public static Service.BuisinessLogic.Interfase.IBitacora Bitacora;

        static async Task Main(string[] args)
        {
            Cliente = new Service.BuisinessLogic.Cliente();
            Bitacora = new Service.BuisinessLogic.Bitacora();
            int opcion;
            bool noSalir = true;
            do
            {
                mostrarMenu();
                opcion = leerOpcion();
                noSalir = ejecutarAccion(opcion);
            } while (noSalir);


            
           
            
            var respuesta = await Task.Run(() => Cliente.GetTimeDifference(117740749));
            Console.WriteLine(respuesta);

            //Cliente.UpdateStateCliente(new Model.Cliente
            //{
            //   Cedula = 117740749,
            //   Estado = "SEGUIMIENTO"
            //});

            //Cliente.UpdateCliente(new Model.Cliente
            //{
            //    Cedula = 123456789,
            //    Nombre = "Prueba 2",
            //    Apellidos = "de modificaion",
            //    Telefono = 71948699,
            //    Email = "test2@gmail.com"

            //});

            //foreach (var c in clientes)
            //{
            //    Console.WriteLine(c.ToString());
            //}

            


        }

        private static void mostrarMenu()
        {
            Console.WriteLine("1.   Registrar Cliente");
            Console.WriteLine("2.   ObtenerLista de Clientes");
            Console.WriteLine("3.   Actualizar Cliente");
            Console.WriteLine("4.   Cambiar estado Cliente");
            Console.WriteLine("5.   Obtener diferencia de tiempo entre registro y modificacion del usuario");
            Console.WriteLine("6.   Obtener todos los registros de la bitácora");
            Console.WriteLine("7.   Obtener los registros de la bitácora por cliente");
            Console.WriteLine("0.   Salir");
        }

        private static int leerOpcion()
        {
            int opcion;
            Console.WriteLine("Seleccione uan opción para usar el programa:\n");
            opcion = Int32.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            return opcion;
        }

        private static bool ejecutarAccion(int opcion)
        {
            bool noSalir = true;
            switch (opcion)
            {
                case 1:
                    AgregarCliente();
                    break;
                case 2:
                    ObtenerListaClientesAsync();
                    break;
                case 3:
                    ActualizarClienteAsync();
                    break;
                case 4:
                    CambiarEstadoCliente();
                    break;
                case 5:
                    ObtenerDiferenciaDeTiempoAsync();
                    break;
                case 6:
                    ObtenerListBitacoraAsync();
                    break;
                case 7:
                    ObtenerListBitacoraByCedulaAsync();
                    break;
                case 0:
                    noSalir = false;
                    break;

            }
            return noSalir;
        }

        private static async Task ObtenerListBitacoraByCedulaAsync()
        {
            Console.WriteLine("Digite la cedula del usuario revisar bitacora");
            int cedula = Int32.Parse(Console.ReadLine());
            List<Model.Bitacora> listbitacora = await Task.Run(() => Bitacora.GetAllBitacorasById(cedula));
            foreach (var b in listbitacora)
            {
                Console.WriteLine(b.ToString() + "\n");
            }
        }

        private static void ObtenerListBitacoraAsync()
        {
            List<Model.Bitacora> listbitacora = Bitacora.GetAllBitacoras();
            foreach (var b in listbitacora)
            {
                Console.WriteLine(b.ToString() + "\n");
            }
        }

        private static void ObtenerListaClientesAsync()
        {
            List<Model.Cliente> clientes = Cliente.GetClientes();
            foreach (var c in clientes)
            {
                Console.WriteLine(c.ToString()+"\n");
            }
        }

        private static async Task ObtenerDiferenciaDeTiempoAsync()
        {
            Console.WriteLine("Digite la cedula del usuario revisar");
            int cedula = Int32.Parse(Console.ReadLine());
            var respuesta = await Task.Run(() => Cliente.GetTimeDifference(cedula));
            Console.WriteLine(respuesta);
        }

        private static void CambiarEstadoCliente()
        {
            Console.WriteLine("Seleccione el estado para el usuario");
            Console.WriteLine("1.   Pendiente");
            Console.WriteLine("2.   Compro");
            Console.WriteLine("3.   Cancelo");

            int opcion = Int32.Parse(Console.ReadLine());
            string estado = "";
            switch (opcion)
            {
                case 1:
                    estado = "PENDIENTE";
                    break;
                case 2:
                    estado = "COMPRO";
                    break;
                case 3:
                    estado = "CANCELO";
                    break;

                default:
                    Console.WriteLine("Seleccione una opcion valida");
                    break;
            }

            Console.WriteLine("Digite la cedula del usuario a modificar");
            int cedula = Int32.Parse(Console.ReadLine());

            Cliente.UpdateStateCliente(new Model.Cliente
            {
                Cedula = cedula,
                Estado = estado
            });

        }

        private static void ActualizarClienteAsync()
        {

            Console.WriteLine("Digite la cedula del cliente a actualizar");
            int cedula = Int32.Parse(Console.ReadLine());

            Model.Cliente client = Cliente.GetClienteByCedula(cedula);

            Console.WriteLine("Digite el nombre del cliente a actualizar, si no desea actualizarlo presione enter");
            string nombre = Console.ReadLine();
            if(nombre != "")
                client.Nombre = nombre;

            Console.WriteLine("Digite los apellidos del cliente a actualizar, si no desea actualizarlo presione enter");
            string apellidos = Console.ReadLine();
            if (apellidos != "")
                client.Apellidos = apellidos;

            Console.WriteLine("Digite el telefono del cliente a actualizar, si no desea actualizarlo digite 0");
            int telefono = Int32.Parse(Console.ReadLine());
            if (telefono != 0)
                client.Telefono = telefono;

            Console.WriteLine("Digite el email del cliente a actualizar, si no desea actualizarlo presione enter");
            string email = Console.ReadLine();
            if (email != "")
                client.Email = email;


            Cliente.UpdateCliente(client);
        }

        private static void AgregarCliente()
        {
            Console.WriteLine("Digite la cedula del cliente a registrar");
            int cedula = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Digite el nombre del cliente a registrar");
            string nombre = Console.ReadLine();

            Console.WriteLine("Digite los apellidos del cliente a registrar");
            string apellidos = Console.ReadLine();

            Console.WriteLine("Digite el telefono del cliente a registrar");
            int telefono = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Digite el email del cliente a registrar");
            string email = Console.ReadLine();

            Cliente.AddCliente(new Model.Cliente
            {
                Cedula = cedula,
                Nombre = nombre,
                Apellidos = apellidos,
                Telefono = telefono,
                Email = email
            });


        }
    }
}
