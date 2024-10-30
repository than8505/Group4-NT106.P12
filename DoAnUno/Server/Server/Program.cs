using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;


namespace Server
{
    
    
    class Program
    {
        private static Socket serverSocket;
        private static Socket clientSocket;
        private static Thread clientThread;
        private static List<Player> connectedPlayers = new List<Player>();
        private static int currentTurn = 1;
        private static bool clockWise = true;
        private static int drawStack = 0;
        private static string wildColor = "";


        

        static void Main(string[] args)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);

            serverSocket.Bind(serverEP);
            serverSocket.Listen(4);
            Console.WriteLine("[ Waiting for connection from players ... ]");

            while (true)
            {
                clientSocket = serverSocket.Accept();
                Console.WriteLine(">> Connection from " + clientSocket.RemoteEndPoint);
                clientThread = new Thread(() => readingClientSocket(clientSocket));
                clientThread.Start();
            }
        }

        public static void readingClientSocket(Socket client)
        {
            Player p = new Player();
            p.pSocket = client;
            connectedPlayers.Add(p);

            byte[] buffer = new byte[1024];
           

            while (p.pSocket.Connected)
            {
                if (p.pSocket.Available > 0)
                {
                    string msg = "";

                    while (p.pSocket.Available > 0)
                    {
                        int bRead = p.pSocket.Receive(buffer);
                        msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                    }

                    Console.WriteLine(p.pSocket.RemoteEndPoint + ": " + msg);
                    AnalyzingMsg(msg, p);
                }
            }
            
        }

        public static void AnalyzingMsg(string msg, Player player)
        {
            string[] payLoad = msg.Split(';');

            switch(payLoad[0])
            {
                case "CONNECT":
                    {
                        player.idGame = payLoad[1];
                        foreach(var p in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes("LOBBYINFO;" + p.idGame);
                            player.pSocket.Send(buffer);
                            Thread.Sleep(100);
                        }

                        foreach(var p in connectedPlayers)
                        {
                            if(p.pSocket != player.pSocket)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes("LOBBYINFO;" + player.idGame);
                                p.pSocket.Send(buffer);
                                Thread.Sleep(100);
                            }
                            
                        }
                    }
                    break;

                case "DISCONNECT":
                    {
                        var playersToRemove = new List<Player>();
                        foreach(var p in connectedPlayers)
                        {
                            p.pSocket.Shutdown(SocketShutdown.Both);
                            p.pSocket.Close();
                            playersToRemove.Add(p);
                        }
                        foreach(var player1 in playersToRemove)
                        {
                            connectedPlayers.Remove(player1);
                        }
                    }
                    break;
                case "START":
                    {
                        RandomizePlayerTurn();
                        connectedPlayers.Sort((x, y) => x.turn.CompareTo(y.turn));
                        PileSuffle();
                        Card.faceUpCard = FaceupCard();
                        foreach (var p in connectedPlayers)
                        {
                            string makemsg = "INIT;" + p.idGame + ";" + p.turn + ";" + p.numOfCard + ";" + InitialCardDeal() + Card.faceUpCard;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            p.pSocket.Send(buffer);
                            Console.WriteLine("Send back:" + makemsg);
                            Thread.Sleep(100);
                        }

                         foreach (var p in connectedPlayers)
                        {
                            foreach (var p2 in connectedPlayers)
                            {
                                if(p.idGame != p2.idGame)
                                {
                                    string makemsg = "OTHERINFO;" + p2.idGame + ";" + p2.turn + ";" + p2.numOfCard;
                                    byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                    p.pSocket.Send(buffer);// xem lai cho nay
                                    Console.WriteLine("Send Back: " + makemsg);
                                    Thread.Sleep(100);
                                }
                            }
                         }

                         foreach(var p in connectedPlayers)
                        {
                            string makemsg = "SETUP;" + p.idGame;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            p.pSocket.Send(buffer);
                            Console.WriteLine("Send back: " + makemsg);
                            Thread.Sleep(100);
                        }

                        foreach (var p in connectedPlayers)
                        {
                            string makemsg2 = "TURN;" + connectedPlayers[currentTurn - 1].idGame;
                            byte[] buffer2 = Encoding.UTF8.GetBytes(makemsg2);
                            p.pSocket.Send(buffer2);
                            Console.WriteLine("Send back: " + makemsg2);
                            Thread.Sleep(100);
                        }
                    }
                 
            
                    break;

                case "DRAW":
                    {
                        connectedPlayers[currentTurn - 1].numOfCard = int.Parse(payLoad[2]);
                        string makemsg = "GETDRAW;" + payLoad[1] + ";" + Card.idCard[0];
                        Card.idCard = Card.idCard.Where(val => val != Card.idCard[0]).ToArray();
                        byte[] bf = Encoding.UTF8.GetBytes(makemsg);
                        connectedPlayers[currentTurn-1].pSocket.Send(bf);

                        foreach (var p in connectedPlayers)
                        {
                            if (p.turn != currentTurn)
                            {
                                string makemsg2 = "UPDATE;" + payLoad[1] + ";" + payLoad[2];
                                byte[] buffer = Encoding.UTF8.GetBytes(makemsg2);
                                p.pSocket.Send(buffer);
                                Console.WriteLine("Send back: " + makemsg2);
                                Thread.Sleep(100);
                            }
                        }

                        if (clockWise == true)
                        {
                            currentTurn++;
                        }
                        else
                        {
                            currentTurn--;
                        }

                        if(currentTurn > connectedPlayers.Count)
                        {
                            currentTurn = 1;
                        }

                        if (currentTurn < 1)
                        {
                            currentTurn = connectedPlayers.Count;
                        }

                        foreach(var p in connectedPlayers)
                        {
                            string makemsg2 = "TURN;" + connectedPlayers[currentTurn - 1].idGame;
                            byte[] buffer2 = Encoding.UTF8.GetBytes(makemsg2);
                            p.pSocket.Send(buffer2);
                            Console.WriteLine("Send back: " + makemsg2);
                            Thread.Sleep(100);
                        }
                    }
                    break;

                case "DISCARD":
                    {
                        Card.faceUpCard = payLoad[3];
                        Discard.disCard.Add(payLoad[3]);
                        connectedPlayers[currentTurn - 1].numOfCard = int.Parse(payLoad[2]);

                        if (connectedPlayers[currentTurn - 1].numOfCard == 0)
                        {
                            foreach(var p in connectedPlayers)
                            {
                                string makemsg = "END;" + payLoad[1] + ";" + payLoad[2] + ";" + payLoad[3];
                                byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                p.pSocket.Send(buffer);
                                Console.WriteLine("Send back: " + makemsg);
                                Thread.Sleep(100);
                            }
                        }
                        else
                        {
                            foreach (var p in connectedPlayers)
                            {
                                if(p.turn != currentTurn)
                                {
                                    string makemsg = "UPDATE;" + payLoad[1] + ";" + payLoad[2] + ";" + payLoad[3];
                                    if (payLoad[3].Contains("df"))
                                    {
                                        makemsg += ";" + payLoad[4];
                                    }
                                    byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                    p.pSocket.Send(buffer);
                                    Console.WriteLine("Send back: " + makemsg);
                                    Thread.Sleep(100);
                                }
                            }

                            if (payLoad[3].Contains("dt"))
                            {
                                drawStack += 2;
                            }

                            if (payLoad[3].Contains("df"))
                            {
                                wildColor =payLoad[4];
                                drawStack += 4;
                            }

                            if (payLoad[3].Contains("rv"))
                            {
                               if(clockWise == true)
                               {
                                    clockWise = false;
                               }
                               else
                                {
                                    clockWise=true;
                                }
                               
                            }

                            if(clockWise)
                            {
                                if (payLoad[3].Contains("s"))
                                {
                                    if(currentTurn == connectedPlayers.Count)
                                    {
                                        currentTurn = 2;
                                    }
                                    else
                                    {
                                        currentTurn += 2;
                                    }
                                }
                                else
                                {
                                    currentTurn++;
                                }
                            }
                            else
                            {
                                if (payLoad[3].Contains("s"))
                                {
                                    if (currentTurn == 1)
                                    {
                                        currentTurn = connectedPlayers.Count-1;
                                    }
                                    else
                                    {
                                        currentTurn = currentTurn - 2;
                                    }
                                }
                                else
                                {
                                    currentTurn--;
                                }
                            }

                            if(currentTurn > connectedPlayers.Count)
                            {
                                currentTurn = 1;
                            }

                            if(currentTurn < 1)
                            {
                                currentTurn = connectedPlayers.Count;
                            }    

                            foreach(var p in connectedPlayers)
                            {
                                string makemsg2 = "TURN;" + connectedPlayers[currentTurn - 1].idGame;
                                byte[] buffer2 = Encoding.UTF8.GetBytes(makemsg2);
                                p.pSocket.Send(buffer2);
                                Console.WriteLine("Send back: "+ makemsg2);
                                Thread.Sleep(100);
                            }

                        }

                    }
                    break;

                case "DRAWSTACK":
                    {
                        connectedPlayers[currentTurn - 1].numOfCard += drawStack;
                        string cardstack = "REVSTACK;" + connectedPlayers[currentTurn - 1].idGame + ";" + connectedPlayers[currentTurn - 1].numOfCard + ";";
                        for (int i = 0; i < drawStack; i++)
                        {
                            cardstack += Card.idCard[0] + ";";
                            Card.idCard = Card.idCard.Where(val => val != Card.idCard[0]).ToArray();
                        }

                        if (payLoad[2] == "wd") 
                        {
                            cardstack += wildColor;
                        }

                        byte[] bf = Encoding.UTF8.GetBytes(cardstack);
                        connectedPlayers[currentTurn - 1].pSocket.Send(bf);
                        drawStack = 0; 
                        Console.WriteLine("Send back: " + cardstack);

                        foreach(var p in connectedPlayers)
                        {
                            if(p.turn != currentTurn)
                            {
                                string makemsg = "UPDATE;" + payLoad[1] + ";" + connectedPlayers[currentTurn - 1].numOfCard;
                                if(payLoad[2] == "wd")
                                {
                                    makemsg += ";" + wildColor;
                                }

                                byte[] buffer = Encoding .UTF8.GetBytes(makemsg);
                                p.pSocket.Send(buffer);
                                Console.WriteLine("Send back: " + makemsg);
                                Thread.Sleep(100);
                            }
                        }

                        wildColor = "";

                        if (clockWise == true)
                        {
                            currentTurn++;
                        }
                        else
                        {
                            currentTurn--;
                        }

                        if (currentTurn > connectedPlayers.Count)
                        {
                            currentTurn = 1;
                        }
                        if (currentTurn < 1)
                        {
                            currentTurn = connectedPlayers.Count;
                        }

                        foreach (var p in connectedPlayers)
                        {
                            string makemsg2 = "TURN;" + connectedPlayers[connectedPlayers.Count - 1].idGame;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg2);
                            p.pSocket.Send(buffer);
                            Console.WriteLine("Send back: " + makemsg2);
                            Thread.Sleep(100);
                        }
                    }
                    break;

                case "MESSAGE":
                    {
                        string sender = player.idGame;
                        string messageContent = payLoad[2]; 
                        foreach (var p in connectedPlayers)
                        {
                            if (p.pSocket != player.pSocket) 
                            {
                                string messageToSend = $"MESSAGE;{sender};{messageContent}";
                                byte[] buffer = Encoding.UTF8.GetBytes(messageToSend);
                                p.pSocket.Send(buffer);
                            }
                        }

                        Console.WriteLine($"{sender}: {messageContent}"); 
                    }
                    break;

                default:
                    break;
            }

            
        }

        private static string InitialCardDeal()
        {
            Random random = new Random();
            string sevenCards = "";
            for (int i = 0; i < 7; i++)
            {
                int pick = random.Next(Card.idCard.Length);
                sevenCards += Card.idCard[pick] + ";";
                Card.idCard = Card.idCard.Where(val => val != Card.idCard[pick]).ToArray();
            }
            return sevenCards;            
        }

       

        private static string FaceupCard()
        {
            string temp = Card.idCard[0];
            Card.idCard = Card.idCard.Where(val => val != Card.idCard[0]).ToArray();
            Discard.disCard.Add(temp);
            return temp;
        }

        public static void PileSuffle()
        {
            Random rand = new Random();
           Card.idCard = Card.idCard.OrderBy(x => rand.Next()).ToArray();
        }

        public static void RandomizePlayerTurn()
        {
            int[] turns = new int[connectedPlayers.Count];

            for (int i = 1; i <= connectedPlayers.Count; i++)
            {
                turns[i - 1] = i;
            }

            Random rand = new Random();
            foreach (var player in connectedPlayers)
            {
                int pick = rand.Next(turns.Length);
                player.turn = turns[pick];
                turns = turns.Where(val => val != turns[pick]).ToArray();

                player.numOfCard = 7;
            }
        }

        public static void BroadcastBack(string type, string msg)
        {
            foreach (var player in connectedPlayers)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(type + msg);
                player.pSocket.Send(buffer);
            }
        }

        public static bool CheckPileEmpty()
        {
            if (Card.idCard.Length == 0)
                return true;
            return false;
        }

        

    }
}
