using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNOClient
{
    public partial class GameTable : Form
    {
        public int drawCards = 0;
        public string faceUpCard = "";
        public List<List<CardButton>> CardBtns;
        public List<Label> lbnames;
        public List<TextBox> tbnums;
        public int row = 0;




        public class CardButton
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string id { get; set; }
            public Button btn = new Button();
        }

        public GameTable()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            btnDiscard.Enabled = false;
            btnDraw.Enabled = false;
            panelColors.Visible = false;

            btnDiscardPileCard.Enabled = false;
            btnDiscardPileCard.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard.BackgroundImageLayout = ImageLayout.Stretch;

            CardBtns = new List<List<CardButton>>();
            lbnames = new List<Label>();
            tbnums = new List<TextBox>();
        }

        public void EnableDiscardBtn()
        {
            btnDiscard.Enabled = true;
        }

        public void EnableDrawBtn()
        {
            btnDraw.Enabled = true;
        }
        

        public void InitCardFetch()
        {
            CardBtns.Add(new List<CardButton>());
            int startX = 124;
            int startY = 424;
            int i = 0;

            foreach (var cd in ThisPlayer.Cards)
            {
                CardButton cardbtn = new CardButton();
                cardbtn.id = cd;
                cardbtn.btn.Tag = cd;
                cardbtn.btn.FlatStyle = FlatStyle.Flat;
                cardbtn.btn.FlatAppearance.BorderSize = 2;
                cardbtn.btn.BackgroundImageLayout = ImageLayout.Stretch;
                cardbtn.btn.Size = new Size(80, 120);
                cardbtn.btn.Location = new Point(X + i * 84, Y);
                cardbtn.X = X + i * 84;
                cardbtn.Y = Y;
                cardbtn.btn.Click += new EventHandler(cardBtn_Click);
                FetchImg(cardbtn.btn, cd);
                CardBtns[row].Add(cardbtn);
                Controls.Add(cardbtn.btn);
                i++;
            }

            CardsIdle();
        }



        public void FetchImg(Button btn, string WhatCard)
        {
            if (WhatCard == "r0")
            {
                btn.BackgroundImage = Properties.Resources.r0;
            }
            else if (WhatCard == "r1" || WhatCard == "r1_")
            {
                btn.BackgroundImage = Properties.Resources.r1;
            }
            else if (WhatCard == "r2" || WhatCard == "r2_")
            {
                btn.BackgroundImage = Properties.Resources.r2;
            }
            else if (WhatCard == "r3" || WhatCard == "r3_")
            {
                btn.BackgroundImage = Properties.Resources.r3;
            }
            else if (WhatCard == "r4" || WhatCard == "r4_")
            {
                btn.BackgroundImage = Properties.Resources.r4;
            }
            else if (WhatCard == "r5" || WhatCard == "r5_")
            {
                btn.BackgroundImage = Properties.Resources.r5;
            }
            else if (WhatCard == "b0")
            {
                btn.BackgroundImage = Properties.Resources.b0;
            }
            else if (WhatCard == "b1" || WhatCard == "b1_")
            {
                btn.BackgroundImage = Properties.Resources.b1;
            }
            else if (WhatCard == "b2" || WhatCard == "b2_")
            {
                btn.BackgroundImage = Properties.Resources.b2;
            }
            else if (WhatCard == "b3" || WhatCard == "b3_")
            {
                btn.BackgroundImage = Properties.Resources.b3;
            }
            else if (WhatCard == "b4" || WhatCard == "b4_")
            {
                btn.BackgroundImage = Properties.Resources.b4;
            }
            else if (WhatCard == "b5" || WhatCard == "b5_")
            {
                btn.BackgroundImage = Properties.Resources.b5;
            }
            else if (WhatCard == "y0")
            {
                btn.BackgroundImage = Properties.Resources.y0;
            }
            else if (WhatCard == "y1" || WhatCard == "y1_")
            {
                btn.BackgroundImage = Properties.Resources.y1;
            }
            else if (WhatCard == "y2" || WhatCard == "y2_")
            {
                btn.BackgroundImage = Properties.Resources.y2;
            }
            else if (WhatCard == "y3" || WhatCard == "y3_")
            {
                btn.BackgroundImage = Properties.Resources.y3;
            }
            else if (WhatCard == "y4" || WhatCard == "y4_")
            {
                btn.BackgroundImage = Properties.Resources.y4;
            }
            else if (WhatCard == "y5" || WhatCard == "y5_")
            {
                btn.BackgroundImage = Properties.Resources.y5;
            }
            else if (WhatCard == "g0")
            {
                btn.BackgroundImage = Properties.Resources.g0;
            }
            else if (WhatCard == "g1" || WhatCard == "g1_")
            {
                btn.BackgroundImage = Properties.Resources.g1;
            }
            else if (WhatCard == "g2" || WhatCard == "g2_")
            {
                btn.BackgroundImage = Properties.Resources.g2;
            }
            else if (WhatCard == "g3" || WhatCard == "g3_")
            {
                btn.BackgroundImage = Properties.Resources.g3;
            }
            else if (WhatCard == "g4" || WhatCard == "g4_")
            {
                btn.BackgroundImage = Properties.Resources.g4;
            }
            else if (WhatCard == "g5" || WhatCard == "g5_")
            {
                btn.BackgroundImage = Properties.Resources.g5;
            }
            else if (WhatCard == "wd" || WhatCard == "wd_X" || WhatCard == "wd_Y" || WhatCard == "wd_Z")
            {
                btn.BackgroundImage = Properties.Resources.wd;
            }
            else if (WhatCard == "df" || WhatCard == "df_X" || WhatCard == "df_Y" || WhatCard == "df_Z")
            {
                btn.BackgroundImage = Properties.Resources.d4;
            }
            else if (WhatCard == "Rv_r" || WhatCard == "Rv_r_X")
            {
                btn.BackgroundImage = Properties.Resources.rrv;
            }
            else if (WhatCard == "Rv_b" || WhatCard == "Rv_b_X")
            {
                btn.BackgroundImage = Properties.Resources.brv;
            }
            else if (WhatCard == "Rv_y" || WhatCard == "Rv_y_X")
            {
                btn.BackgroundImage = Properties.Resources.yrv;
            }
            else if (WhatCard == "Rv_g" || WhatCard == "Rv_g_X")
            {
                btn.BackgroundImage = Properties.Resources.grv;
            }
            else if (WhatCard == "s_r" || WhatCard == "s_r_X")
            {
                btn.BackgroundImage = Properties.Resources.rs;
            }
            else if (WhatCard == "s_b" || WhatCard == "s_b_X")
            {
                btn.BackgroundImage = Properties.Resources.bs;
            }
            else if (WhatCard == "s_y" || WhatCard == "s_y_X")
            {
                btn.BackgroundImage = Properties.Resources.ys;
            }
            else if (WhatCard == "s_g" || WhatCard == "s_g_X")
            {
                btn.BackgroundImage = Properties.Resources.gs;
            }
            else if (WhatCard == "dt_r" || WhatCard == "dt_r_X")
            {
                btn.BackgroundImage = Properties.Resources.rd2;
            }
            else if (WhatCard == "dt_b" || WhatCard == "dt_b_X")
            {
                btn.BackgroundImage = Properties.Resources.bd2;
            }
            else if (WhatCard == "dt_y" || WhatCard == "dt_y_X")
            {
                btn.BackgroundImage = Properties.Resources.yd2;
            }
            else if (WhatCard == "dt_g" || WhatCard == "dt_g_X")
            {
                btn.BackgroundImage = Properties.Resources.gd2;
            }
        }


        private void SetupPlayerPanel(string playerName, TextBox textBox, Label label)
        {
            label.Text = playerName;
            textBox.Tag = playerName;
            textBox.Text = "7"; 
            lbnames.Add(label);
            tbnums.Add(textBox);
        }

        public void InitDisplay()
        {
            ClientSocket.otherplayers.Sort((x, y) => x.Turn.CompareTo(y.Turn));
            SetupPlayerPanel(ThisPlayer.Name, textBoxNum, labelName);

            switch (ClientSocket.otherplayers.Count)
            {
                case 1:
                    panelPlayerL.Visible = false;
                    panelPlayerR.Visible = false;
                    SetupPlayerPanel(ClientSocket.otherplayers[0].Name, textBoxNumU, labelNameU);
                    break;
                case 2:
                    panelPlayerU.Visible = false;
                    if (ThisPlayer.Turn == 2)
                    {
                        SetupPlayerPanel(ClientSocket.otherplayers[1].Name, textBoxNumL, labelNameL);
                        SetupPlayerPanel(ClientSocket.otherplayers[0].Name, textBoxNumR, labelNameR);
                    }
                    else
                    {
                        SetupPlayerPanel(ClientSocket.otherplayers[0].Name, textBoxNumL, labelNameL);
                        SetupPlayerPanel(ClientSocket.otherplayers[1].Name, textBoxNumR, labelNameR);
                    }
                    break;
                case 3:
                    int[] turnOrder = ThisPlayer.Turn == 1 && ThisPlayer.Turn == 4 ? new[] { 0, 1, 2 } : ThisPlayer.Turn == 2 ? new[] { 1, 2, 0 } : new[] { 2, 0, 1 };
                    SetupPlayerPanel(ClientSocket.otherplayers[turnOrder[0]].Name, textBoxNumL, labelNameL);
                    SetupPlayerPanel(ClientSocket.otherplayers[turnOrder[1]].Name, textBoxNumU, labelNameU);
                    SetupPlayerPanel(ClientSocket.otherplayers[turnOrder[2]].Name, textBoxNumR, labelNameR);
                    break;
            }
        }


        public void DisplayFaceUp()
        {
            FetchImg(btnDiscardPileCard, faceUpCard);
        }

        public string tempturnname = "";
        public void HighlightTurn(string name)
        {
            tempturnname = name;
            foreach (var n in lbnames)
            {
                if (n.Text == name)
                {
                    n.Font = new Font(n.Font, FontStyle.Bold);
                    n.ForeColor = Color.Red;
                    break;
                }
            }
        }

        public void UndoHighlightTurn()
        {
            foreach (var n in lbnames)
            {
                if (n.Text == tempturnname)
                {
                    n.Font = new Font(n.Font, FontStyle.Regular);
                    n.ForeColor = Color.Black;
                    break;
                }
            }
        }

        
        private int i;
        private int X = 124;
        private int Y = 424;
        public void FetchDrawCard(string cd)
        {

            CardButton cardbtn = new CardButton();
            cardbtn.id = cd;
            cardbtn.btn.Tag = cd;
            cardbtn.btn.FlatStyle = FlatStyle.Flat;
            cardbtn.btn.FlatAppearance.BorderSize = 2;
            cardbtn.btn.BackgroundImageLayout = ImageLayout.Stretch;
            cardbtn.btn.Size = new Size(80, 120);
            FetchImg(cardbtn.btn, cd);
            if (CardBtns[row].Count == 7)
            {
                i = 0;
                row++;
                CardBtns.Add(new List<CardButton>());
                cardbtn.X = X;
                cardbtn.Y = Y;
                cardbtn.btn.Location = new Point(X, Y);
            }
            else
            {
                cardbtn.X = X + i * 84;
                cardbtn.Y = Y;
                cardbtn.btn.Location = new Point(cardbtn.X, cardbtn.Y);
            }
            i++;
            CardBtns[row].Add(cardbtn);
            cardbtn.btn.Visible = false;
            Controls.Add(cardbtn.btn);
        }

        public void UpdateNumOfCards(string name, string n)
        {
            foreach (var tb in tbnums)
            {
                if (tb.Tag.ToString() == name)
                {
                    tb.Text = n;
                }
            }
        }
        

        public void CardsIdle()
        {
            foreach (var row in CardBtns)
            {
                foreach (var cdbtn in row)
                {
                    cdbtn.btn.FlatAppearance.BorderColor = Color.Black;
                    cdbtn.btn.Enabled = false;
                }
            }
        }

        
        public void DisplayCardsTemp()
        {
            foreach (var card in ThisPlayer.Cards)
            {
                textBoxDisplayText.Text += card + " ";
            }
        }

        public string selectedCardId = ""; // To assign after btn card clicked

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            ThisPlayer.NumOfCards--;
            ClientSocket.datatype = "DISCARD";
            if (selectedCardId.Contains("wd") || selectedCardId.Contains("df"))
            {
                panelColors.Visible = true;
            }
            else
            {
                string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards + ";" + selectedCardId;
                ClientSocket.SendMessage(makemsg);
            }

            btnDiscard.Enabled = false;
            btnDraw.Enabled = false;

            faceUpCard = selectedCardId;
            DisplayFaceUp();

            foreach (var cd in CardBtns[currentdisplayrow])
            {
                if (cd.btn.Tag.ToString() == selectedCardId)
                {
                    cd.btn.Visible = false;
                }
            }

            foreach (var tb in tbnums)
            {
                if (tb.Tag.ToString() == ThisPlayer.Name)
                {
                    tb.Text = ThisPlayer.NumOfCards.ToString();
                    break;
                }
            }

            CardsIdle();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        { 
            if(faceUpCard.Contains("dt"))
            {
                drawCards = 2;
                ThisPlayer.NumOfCards += 2;
                ClientSocket.datatype = "DRAWSTACK";
                string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
                ClientSocket.SendMessage(makemsg);
            }
            else if (faceUpCard.Contains("df"))
            {
                drawCards = 4;
                ThisPlayer.NumOfCards += 4;
                ClientSocket.datatype = "DRAWSTACK";
                string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
                ClientSocket.SendMessage(makemsg);
            }
            else
            {
                drawCards = 1;
                ThisPlayer.NumOfCards += 1;
                ClientSocket.datatype = "DRAW";
                string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
                ClientSocket.SendMessage(makemsg);
            }
          
            
           
            
            foreach (var tb in tbnums)
            {
                if (tb.Tag.ToString() == ThisPlayer.Name)
                {
                    tb.Text = ThisPlayer.NumOfCards.ToString();
                    break;
                }
            }

            btnDraw.Enabled = false;
            CardsIdle();
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
            if (selectedCardId.Contains("df")|| selectedCardId.Contains("wd"))
            {
                makemsg += ";" + selectedCardId;
            }
            makemsg += ";r";
            ClientSocket.SendMessage(makemsg);
            panelColors.Visible = false;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
            if (selectedCardId.Contains("df")|| selectedCardId.Contains("wd"))
            {
                makemsg += ";" + selectedCardId;
            }
            
            makemsg += ";y";
            ClientSocket.SendMessage(makemsg);
            panelColors.Visible = false;
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
            if (selectedCardId.Contains("df")|| selectedCardId.Contains("wd"))
            {
                makemsg += ";" + selectedCardId;
            }
            makemsg += ";g";
            ClientSocket.SendMessage(makemsg);
            panelColors.Visible = false;
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            string makemsg = ThisPlayer.Name + ";" + ThisPlayer.NumOfCards;
            if (selectedCardId.Contains("df") || selectedCardId.Contains("wd"))
            {
                makemsg += ";" + selectedCardId;
            }
            makemsg += ";b";
            ClientSocket.SendMessage(makemsg);
            panelColors.Visible = false;
        }

        private int currentdisplayrow = 0;

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentdisplayrow < CardBtns.Count - 1)
            {
                HideCurrentRow();
                currentdisplayrow++;
                ShowCurrentRow();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentdisplayrow > 0)
            {
                HideCurrentRow();
                currentdisplayrow--;
                ShowCurrentRow();
            }
        }

        private void HideCurrentRow()
        {
            foreach (var cd in CardBtns[currentdisplayrow])
            {
                cd.btn.Visible = false;
            }
        }

        private void ShowCurrentRow()
        {
            foreach (var cd in CardBtns[currentdisplayrow])
            {
                cd.btn.Visible = true;
                FetchImg(cd.btn, cd.id);
            }
        }
        void cardBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            selectedCardId = btn.Tag.ToString();
        }

        private void btnDiscardPileCard_Click(object sender, EventArgs e)
        {

        }

        private void GameTable_Load(object sender, EventArgs e)
        {

        }
        
        public void ReceiveMessage(string message)
        {
            string[] parts = message.Split(new[] { ';' }, 3);
            if (parts.Length == 3 && parts[0] == "MESSAGE")
            {
                string sender = parts[1].Trim();
                string content = parts[2].Trim();
                DisplayChatMessage(sender, content);
            }
            else
            {
                rtbChatBox.AppendText(message + Environment.NewLine);
            }
        }

        public void DisplayChatMessage(string sender, string messageContent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayChatMessage(sender, messageContent)));
            }
            else
            {
                rtbChatBox.AppendText($"{sender}: {messageContent}\n");
                rtbChatBox.ScrollToCaret();
            }
        }

       
        

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtmessageBox.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                ClientSocket.datatype = "MESSAGE"; 
                string chatMessage = $"{ThisPlayer.Name};{message}"; 
                ClientSocket.SendMessage(chatMessage);
                txtmessageBox.Clear(); 
            }
        }

        
    }
}
