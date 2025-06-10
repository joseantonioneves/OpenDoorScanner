using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace PortScanner
{
    class Program
    {
        static bool stop = false;
        static int startPort;
        static int endPort;

        static List<int> openPorts = new List<int>();

        static object consoleLock = new object();

        static int waitingForResponses;

        static int maxQueriesAtOneTime = 100;

        static void Main(string[] args)
        {
            Console.WriteLine(
                "================================================================================"
                );
            Console.WriteLine
                (
                "OpenDoorScanner - Varredura de conexão TCP entre Host's"
                );
            Console.WriteLine
                (
                "v. 1.0.0   27-10-2017   HN Negocios e Serviços by epharma"
                );
            Console.WriteLine(
                "================================================================================"
                );
            Console.WriteLine("");
            begin:
            Console.Write("Entre com o endereço ip do host: ");
            string ip = Console.ReadLine();

            IPAddress ipAddress;

            if (!IPAddress.TryParse(ip, out ipAddress))
                goto begin;

            startP:

            Console.Write("Entre com o numero da porta de início  da varredura: ");
            string sp = Console.ReadLine();

            if (!int.TryParse(sp, out startPort))
                goto startP;

            endP:

            Console.Write("Entre com o numero da porta de término da varredura: ");
            string ep = Console.ReadLine();

            if (!int.TryParse(ep, out endPort))
                goto endP;

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Pressione qualquer tecla para parar a varredura...");

            Console.WriteLine("");
            Console.WriteLine("");

            ThreadPool.QueueUserWorkItem(StartScan, ipAddress);

            Console.ReadKey();

            stop = true;

            Console.WriteLine("Pressione qualquer tecla para sair ...");
            Console.ReadKey();
        }

        static void StartScan(object o)
        {
            IPAddress ipAddress = o as IPAddress;

            for (int i = startPort; i < endPort; i++)
            {
                lock (consoleLock)
                {
                    int top = Console.CursorTop;

                    Console.CursorTop = 12;
                    Console.WriteLine("Varrendo port: {0}    ", i);

                    //Console.CursorTop = top;
                }

                while (waitingForResponses >= maxQueriesAtOneTime)
                    Thread.Sleep(0);

                if (stop)
                    break;

                try
                {
                    Socket s = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream, ProtocolType.Tcp);

                    s.BeginConnect(new IPEndPoint(ipAddress, i), EndConnect, s);

                    Interlocked.Increment(ref waitingForResponses);
                }
                catch (Exception)
                {

                }
            }
        }

        static void EndConnect(IAsyncResult ar)
        {
            try
            {
                DecrementResponses();

                Socket s = ar.AsyncState as Socket;

                s.EndConnect(ar);

                if (s.Connected)
                {
                    int openPort = Convert.ToInt32(s.RemoteEndPoint.ToString().Split(':')[1]);

                    openPorts.Add(openPort);

                    lock (consoleLock)
                    {
                        
                        Console.WriteLine("TCP conectado na porta: {0}", openPort);
                    }

                    s.Disconnect(true);
                }
            }
            catch (Exception)
            {

            }
        }

        static void IncrementResponses()
        {
            Interlocked.Increment(ref waitingForResponses);

            PrintWaitingForResponses();
        }

        static void DecrementResponses()
        {
            Interlocked.Decrement(ref waitingForResponses);

            PrintWaitingForResponses();
        }

        static void PrintWaitingForResponses()
        {
            lock (consoleLock)
            {
                int top = Console.CursorTop;

                Console.CursorTop = 13;
                Console.WriteLine("Aguardando respostas de {0} sockets. ", waitingForResponses);

                Console.CursorTop = top;
            }
        }
    }
}