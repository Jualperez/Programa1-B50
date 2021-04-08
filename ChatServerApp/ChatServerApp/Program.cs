using ChatServerApp.comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Int32.Parse(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor puerto: {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true) { 
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Esperando Clientes....");
                if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion establecida");
                        Console.WriteLine("S: Hola Mundo Cliente");
                        servidor.Escribir("Hola Mundo Cliente");
                        string mensaje = servidor.Leer();
                        Console.WriteLine("C:{0}", mensaje);
                        servidor.CerrarConexion();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es posible iniciar ");
                Console.ReadKey();
            }
        }
    }
}
