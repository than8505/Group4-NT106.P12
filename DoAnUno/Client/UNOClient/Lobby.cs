using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNOClient
{
    public partial class Lobby : Form
    {
		private Dictionary<int, Label> playerNames;
		private Dictionary<int, PictureBox> playerIcons;
		private int connectedPlayerCount;
		public Lobby()
        {
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

			// Khởi tạo từ điển cho tên người chơi và biểu tượng
			playerNames = new Dictionary<int, Label>
			{
				{ 1, labelP1 },
				{ 2, labelP2 },
				{ 3, labelP3 },
				{ 4, labelP4 }
			};

			playerIcons = new Dictionary<int, PictureBox>
			{
				{ 1, pictureBoxP1 },
				{ 2, pictureBoxP2 },
				{ 3, pictureBoxP3 },
				{ 4, pictureBoxP4 }
			};

			btnStart.Visible = false;
			connectedPlayerCount = 0;
		}

		public void ShowStartButton()
        {
            btnStart.Visible = true;
        }

        // Just a test - to be removed later !
       

        public void DisplayConnectedPlayer(string name)
        {
			if (connectedPlayerCount < 4)
			{
				connectedPlayerCount++;
				UpdatePlayerName(connectedPlayerCount, name);
			}
			else
			{
				// Có thể thông báo cho người dùng rằng số lượng người chơi đã đầy
				MessageBox.Show("Maximum number of players reached.");
			}
		}
		private void UpdatePlayerName(int playerIndex, string name)
		{
			if (playerNames.TryGetValue(playerIndex, out Label label))
			{
				label.Text = name;
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
        {
            ClientSocket.datatype = "START";
            ClientSocket.SendMessage("");        
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void pictureBoxP1_Click(object sender, EventArgs e)
		{

		}

		private void Lobby_Load(object sender, EventArgs e)
		{

		}
	}
}
