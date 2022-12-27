using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HasanClient2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string myIp = "192.168.0.188";
            int port = 3000;

            Client client = new Client(myIp, port);

            client.ConnectToServer();
            Console.WriteLine("Connected to server");

            Thread.Sleep(1000);
            Console.Clear();

            client.ServerData();

            try
            {
                string messageToServer = "";
                string messageFromServer = "";

                while (client.clientStatus)
                {
                    //The message will be whatever we type on the keyboard;
                    messageToServer = Console.ReadLine();

                    if (messageToServer == "exit")
                    {
                        client.clientStatus = false;
                        client.streamWriter.WriteLine("Bye");
                        client.streamWriter.Flush();
                    }
                    if (messageToServer != "exit")
                    {
                        client.streamWriter.WriteLine(/*"Client Message " +*/ messageToServer);//Send to the server whatever the client wrote.
                        client.streamWriter.Flush();

                        messageFromServer = client.streamReader.ReadLine();
                        Console.WriteLine(/*"Server Message: " +*/ messageFromServer);

                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Problem/s reading from server.");
            }

            client.Disconnect();
        }
    }
}