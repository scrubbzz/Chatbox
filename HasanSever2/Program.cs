using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;

namespace HasanSever2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAdress = IPAddress.Parse("192.168.0.188");
            int port = 3000;

            Server server = new Server(ipAdress, port);


            server.ListenForClients();
            Console.WriteLine("The server started Man");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Waiting for connection");

            //If someone wants to connect, accept it.

            server.AcceptClient();




            Console.WriteLine("Client connected");

            string messageFromClient = "";
            string messageToClient = "";

            try
            {
                server.ClientData();

                while (server.serverStatus)
                {
                    //Only when a client is connected.
                    if (server.clientSocket.Connected)
                    {
                        //The client has to be able to write a message...
                        messageFromClient = server.streamReader.ReadLine();
                        Console.WriteLine("Client Message: " + messageFromClient);

                        if (messageFromClient == "exit")
                        {
                            server.serverStatus = false;

                            server.streamReader.Close();
                            server.streamWriter.Close();
                            server.networkStream.Close();
                            return;
                        }

                        //After the client says something, I can now say something.
                        //Console.WriteLine("Server Turn: ");

                        messageToClient = Console.ReadLine();
                        //Send my message to the client.
                        server.streamWriter.WriteLine("Server Message: " + messageToClient);

                        server.streamWriter.Flush();
                    }
                }
                //After the client says exit, disconnect.
                server.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION MESSAGE in program class : " + e.Message);

            }
        }
    }
}