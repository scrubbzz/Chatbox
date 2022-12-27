using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HasanSever2
{
    internal class Server
    {
        public IPAddress ipAdress;//The server and the client both need to be using the same values for IP adress and port.
        public int port;
        public bool serverStatus = true;
        public TcpListener tcpListener;//tcpListener basically has functions to listen for and accept incomming connection requests. Either a 
        public Socket clientSocket;//TcpClient or a Socket can be used to connect to it.
        public TcpClient tcpClient;


        public NetworkStream networkStream;//Network stream provides methods for sending and receiving data. Sever and Client both need to use it.
        public StreamReader streamReader;//Has some functions to allow us to read what message a client has sent over.
        public StreamWriter streamWriter;//Lets us send messages.


      
        public Server(IPAddress ipAdress, int port)
        {
            this.ipAdress = ipAdress;
            this.port = port;
        }
        public void ListenForClients()
        {
            /* try
             {
                 //tcpListener has functions to listen for and accept incoming connection requests.
                 tcpListener = new TcpListener(ipAdress, port);
                 tcpListener.Start();
                 Console.WriteLine("TcpListener started");
             }
             catch
             {
                 Console.WriteLine("Could not start, check ListenForClientsFunction");
             }*/
            tcpListener = new TcpListener(ipAdress, port);
            tcpListener.Start();
            if (tcpListener != null)
            {
                Console.WriteLine("TCP WORKS!");
            }
        }

        public void AcceptClient()
        {
            try
            {
                clientSocket = tcpListener.AcceptSocket();
                //tcpClient = tcpListener.AcceptTcpClient();
            }
            catch
            {
                Console.WriteLine("Could not accept client, Check the AcceptClient fucntion.");

            }
            /* clientSocket = tcpListener.AcceptSocket();
             //tcpClient = tcpListener.AcceptTcpClient();
             if (clientSocket != null)
             {
                 Console.WriteLine("We have a new client socket");
             }*/

        }

        //Allows the server to commune with the client.
        public void ClientData()
        {
            networkStream = new NetworkStream(clientSocket);
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }

        public void Disconnect()
        {
            networkStream.Close();
            streamWriter.Close();
            streamReader.Close();
            clientSocket.Close();
        }
    }
}

