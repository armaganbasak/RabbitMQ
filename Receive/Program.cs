using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receive;

public class Program { 
    public static void Main(string[] args)
    {

            ITcpListen tcpListen = new TcpListen();
            
            tcpListen.Listen();



        }
}