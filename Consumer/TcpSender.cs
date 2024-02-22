using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class TcpSender : ITcpSender
    {
        public void Send(byte[] bytes)
        {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Sunucu IP adresi
                int port = 13000; // Kullanılacak port numarası

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, port);

                if(tcpClient.Connected)
                {
                    Console.WriteLine("Connected. Data is sending.");

                }
                NetworkStream networkStream = tcpClient.GetStream();
                
                
                byte[] buffer = bytes;


                string data = Convert.ToBase64String(bytes);


                networkStream.Write(buffer, 0, buffer.Length);

            Console.WriteLine("Data is sent to receiver : {0}", data);

            tcpClient.Close();


            
        }
    }
}