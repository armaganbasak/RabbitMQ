using Consumer;
using Microsoft.AspNetCore.Hosting.Server;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Receive
{
    public class TcpListen : ITcpListen
    {
        TcpListener server = null;
        string key = "12345678901234567890123456789a!1";
        public void Listen()
        {
            try
            {
                TcpClient tcpClient = new TcpClient();
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                var server = new TcpListener(localAddr, port);

                server.Start();


                byte[] bytes = new byte[16];
                String data = null;

                while (true)
                {
                    Console.WriteLine("Waiting for connection..");

                    using TcpClient client = server.AcceptTcpClient();

                    if (client != null)
                    {
                        Console.Write("Connected.\n");
                    }

                    NetworkStream stream = client.GetStream();

                    int i = 0;

                    MemoryStream ms = new MemoryStream();

                    int bytesRead;

                    while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        ms.Write(bytes, 0, bytesRead);

                        //string x = Convert.ToBase64String(bytes,0,bytesRead);
                        //byte[] bytest = Encoding.ASCII.GetBytes(x);

                        string result =  Decryption.DecryptAes(bytes, key);

                        Console.WriteLine("Data received from consumer: {0}",result);

                    }



                    //byte[] _bytes = ms.ToArray();
                    //string descryptedText = Decryption.DecryptAes(_bytes, key);

                    //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    //{
                    //    data = Convert.ToBase64String(bytes,0,i);
                    //    Console.WriteLine("Data received from consumer : {0}",data);
                    //}
                }
            }
            catch(SocketException e) 
            {
                Console.WriteLine("{0}",e);

            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("Press enter to stop.");
            Console.Read();

        }
    }
}
