using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using static UNOClient.GameTable;
using System.Text;

namespace UNOClient
{
	class ClientSocket
	{
		public static Socket clientSocket;
		public static Thread recvThread;
		public static string datatype = "";
		

		public enum MessageType
		{
			LOBBYINFO,
			INIT,
			OTHERINFO,
			SETUP,
			UPDATE,
			TURN,
			GETDRAW,
			REVSTACK,
			END,
			MESSAGE,
			UNKNOWN
		}

		public static void Connect(IPEndPoint serverEP)
		{
			clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			clientSocket.Connect(serverEP);
			recvThread = new Thread(readingReturnData) { IsBackground = true };
			recvThread.Start();
		}

		public static void SendMessage(string data)
		{
			string msgstr = $"{datatype};{data}";
			byte[] msg = Encoding.UTF8.GetBytes(msgstr);
			clientSocket.Send(msg);
		}

		public static void readingReturnData()
		{
            byte[] buffer = new byte[1024];

            while (clientSocket.Connected)
            {
                if (clientSocket.Available > 0)
                {
                    string msg = "";

                    while (clientSocket.Available > 0)
                    {
                        int bRead = clientSocket.Receive(buffer);
                        msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                    }

                    AnalyzingReturnMessage(msg);
                }
            }

        }
        public static GameTable gametable;
        public static List<OtherPlayers> otherplayers;
		
        public static void AnalyzingReturnMessage(string msg)
		{
			string[] arrPayload = msg.Split(';');
			MessageType messageType = Enum.TryParse(arrPayload[0], out MessageType result) ? result : MessageType.UNKNOWN;

			switch (messageType)
			{
				case MessageType.LOBBYINFO:
					ConnectMenu.lobby.DisplayConnectedPlayer(arrPayload[1]);
					break;

				case MessageType.INIT:
					InitPlayerData(arrPayload);
					break;

				case MessageType.OTHERINFO:
					AddOtherPlayer(arrPayload);
					break;

				case MessageType.SETUP:
					gametable.InitDisplay();
					break;

				case MessageType.UPDATE:
					UpdateGameData(arrPayload);
					break;

				case MessageType.TURN:
					HandleTurn(arrPayload);
					break;

				case MessageType.GETDRAW:
					ProcessBoBai(arrPayload);
					break;

				case MessageType.REVSTACK:
					ReviseStack(arrPayload);
					break;

				case MessageType.END:
					EndGame(arrPayload);
					break;

                case MessageType.MESSAGE:
                    if (arrPayload.Length >= 3)
                    {
                        string sender = arrPayload[1];
                        string content = arrPayload[2];
                        gametable?.DisplayChatMessage(sender, content);
                    }
                    break;

                default:
					break;
			}
		}

        

        private static void InitPlayerData(string[] arrPayload)
		{
			ThisPlayer.Turn = int.Parse(arrPayload[2]);
			ThisPlayer.NumOfCards = int.Parse(arrPayload[3]);
            for (int i = 4; i <= 10; i++)
            {
                ThisPlayer.Cards.Add(arrPayload[i]);
            }

            gametable = new GameTable();
			otherplayers = new List<OtherPlayers>();
			ConnectMenu.lobby.Invoke((MethodInvoker)delegate()
			{
				gametable.faceUpCard = arrPayload[11];
				gametable.InitCardFetch();
				gametable.DisplayFaceUp();
				gametable.Show();
			});
		}

		private static void AddOtherPlayer(string[] arrPayload)
		{
			var otherplayer = new OtherPlayers
			{
				Name = arrPayload[1],
				Turn = int.Parse(arrPayload[2]),  
				NumOfCards = int.Parse(arrPayload[3]) 
			};
			otherplayers.Add(otherplayer);
		}

		private static void UpdateGameData(string[] arrPayload)
		{
			gametable.UpdateNumOfCards(arrPayload[1], arrPayload[2]);
			if (arrPayload.Length > 3)
			{
				gametable.faceUpCard = arrPayload[3];
				gametable.DisplayFaceUp();
			}
		}

		private static void HandleTurn(string[] arrPayload)
		{
			if (arrPayload[1] == ThisPlayer.Name)
				CheckForPotentialCards();

			gametable.UndoHighlightTurn();
			gametable.HighlightTurn(arrPayload[1]);
		}

		private static void ProcessBoBai(string[] arrPayload)
		{
			gametable.Invoke((MethodInvoker)delegate()
			{
				for (int i = 0; i < gametable.drawCards; i++)
				{
					gametable.FetchDrawCard(arrPayload[2]);
				}
			});
		}

		private static void ReviseStack(string[] arrPayload)
		{
			for (int i = 3; i < arrPayload.Length; i++)
			{
				if (arrPayload[i] == "r" || arrPayload[i] == "g" || arrPayload[i] == "b" || arrPayload[i] == "y")
				{
					gametable.faceUpCard = arrPayload[i];
				}
				else
				{
					gametable.FetchDrawCard(arrPayload[i]);
				}
			}
			CheckForPotentialCards();
		}

		private static void EndGame(string[] arrPayload)
		{
			if (ThisPlayer.Name == arrPayload[1])
			{
				EndForm ef = new EndForm();
				ef.Show();
			}
			else
			{
				Form2 loseForm = new Form2();
				loseForm.Show();
			}
		}

		private static void UpdateOtherPlayerCount(string[] arrPayload)
		{
			if (arrPayload.Length < 3)
			{
				throw new ArgumentException("Invalid payload: Not enough data to update player count.");
			}

			foreach (var player in otherplayers)
			{
				if (player.Name == arrPayload[1])
				{
					player.NumOfCards = int.Parse(arrPayload[2]); 
				}
			}
		}




		public static void CheckForPotentialCards()
		{
			gametable.EnableDrawBtn();

			foreach (var row in gametable.CardBtns)
			{
				foreach (var bt in row)
				{
					string checknum = new String(gametable.faceUpCard.Where(Char.IsDigit).ToArray());
					string getnum = new String(bt.id.Where(Char.IsDigit).ToArray());

					if (checknum != "" && checknum == getnum)
					{
						bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
						bt.btn.Enabled = true;
						gametable.EnableDiscardBtn();
						continue;
					}

					if (gametable.faceUpCard.Contains("r"))
					{
						if (bt.id.Contains("r"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (gametable.faceUpCard.Contains("y"))
					{
						if (bt.id.Contains("y"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (gametable.faceUpCard.Contains("b"))
					{
						if (bt.id.Contains("b"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (gametable.faceUpCard.Contains("g"))
					{
						if (bt.id.Contains("g"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (gametable.faceUpCard.Contains("s"))
					{
						if (bt.id.Contains("s"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (gametable.faceUpCard.Contains("Rv"))
					{
						if (bt.id.Contains("Rv"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (gametable.faceUpCard.Contains("dt"))
					{
						if (bt.id.Contains("dt"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}


					if (bt.id.Contains("wd"))
					{
						if (!gametable.faceUpCard.Contains("dt") || !gametable.faceUpCard.Contains("df"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}

					if (bt.id.Contains("df"))
					{
						if (!gametable.faceUpCard.Contains("dt"))
						{
							bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
							bt.btn.Enabled = true;
							gametable.EnableDiscardBtn();
							continue;
						}
					}
					bt.btn.FlatAppearance.BorderColor = Color.Red;
				}
			}
		}
    }
}
