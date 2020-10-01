using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cliente
{
    class Program
    {
        const int porta = 7000;
        const string ip_servidor = "127.0.0.1";
        static void Main(string[] args)
        {
            TcpClient cliente = new TcpClient();
            Console.WriteLine("Conectando ao servidor...");
            try{
            cliente.Connect(ip_servidor, porta);
            Console.WriteLine("Ok!");
            }catch (Exception)
            {
            Console.WriteLine("Falhou."); 
            }

            byte[] bytes = new byte[1024];
            NetworkStream stream = cliente.GetStream();

            Console.WriteLine("Envie uma mensagem. P/ sair, pressione ENTER.");
            Console.Write("> ");
            string mensagem = Console.ReadLine();

            while (mensagem.Length > 0) {
                bytes = Encoding.ASCII.GetBytes(mensagem);
                stream.Write(bytes, 0, bytes.Length);

                Console.Write("> ");
                string msg = Console.ReadLine();
            }

            stream.Close();
            Console.WriteLine("Desconectado");
            cliente.Close();
        }
    }
}
