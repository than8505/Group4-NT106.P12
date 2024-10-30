using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace UNOClient
{
    public partial class ConnectMenu : Form
    {
        public static Lobby lobby;
        public ConnectMenu()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), 11000);
            ClientSocket.datatype = "CONNECT";
            ClientSocket.Connect(serverEP);
            lobby = new Lobby();
            ClientSocket.SendMessage(textBoxName.Text);

            ThisPlayer.Name = textBoxName.Text;

            lobby.FormClosed += new FormClosedEventHandler(lobby_FormClosed);
            lobby.ShowStartButton();
            this.Hide();
            lobby.Show();
        }

        void lobby_FormClosed(object sender, EventArgs e)
        {
            ClientSocket.datatype = "DISCONNECT";
            ClientSocket.SendMessage(ThisPlayer.Name);
            ClientSocket.clientSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            ClientSocket.clientSocket.Close();
            this.Show();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), 11000);
            ClientSocket.datatype = "CONNECT";
            ClientSocket.Connect(serverEP);
            lobby = new Lobby();
            ClientSocket.SendMessage(textBoxName.Text);

            ThisPlayer.Name = textBoxName.Text;

            lobby.FormClosed += new FormClosedEventHandler(lobby_FormClosed);
            this.Hide();
            lobby.Show();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.unorules.com/");
        }

        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
