using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp.comunicacion
{
    public class ServerSocket
    {
        private int puerto;
        private Socket servidor;
        private Socket comunicacionCliente;
        private StreamReader reader;
        private StreamWriter writer;
        
        public ServerSocket(int puerto)
        {
            this.puerto = puerto; 
        }
        public bool Iniciar()
        {
            try
            {
                this.servidor = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

                servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));

                this.servidor.Listen(10);

                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }
        public bool ObtenerCliente()
        {
            try
            {
                this.comunicacionCliente = this.servidor.Accept();
                Stream stream = new NetworkStream(this.comunicacionCliente);
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();


                return true;
            }catch(IOException ex)
            {
                return false;
            }
        }
        public string Leer()
        {
            try
            {

                return this.reader.ReadLine().Trim();
            }
            catch (IOException)
            {
                return null;
            }
        }
        public void CerrarConexion()
        {
            this.comunicacionCliente.Close();
        }
    }
}
