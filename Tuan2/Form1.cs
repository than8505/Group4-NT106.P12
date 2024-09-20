using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MyCalculator
{

    public partial class Form1 : Form
    {
        double num1, num2;
        double result;
        string option;
        public Form1()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            result = 0;
            num1 = 0;
            num2 = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            option = "*";
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + button19.Text;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "1";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "2";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "3";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "4";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "5";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "6";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "7";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "8";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "9";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            option = "/";
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            option = "-";
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            option = "+";
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
                textBox1.Text = textBox1.Text + ".";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            num2 = double.Parse(textBox1.Text);
            if (option == "+") result = num1 + num2;
            if (option.Equals("-")) result = num1 - num2;
            if (option.Equals("*")) result = num1 * num2;
            if (option.Equals("/"))
            {
                if (num2 != 0)  result = num1 / num2;
                else  MessageBox.Show("Null");
            }
            textBox1.Text = result.ToString();
        }
    }
}
