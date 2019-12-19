using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RSAProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int input;
            input = int.Parse(Choice.Text);
            if(input == 1)
            {
                FileStream f = new FileStream("DIV.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(f);
                string first = FirstNum.Text;
                string second = SecondNum.Text;
                Tuple<string, string> Answer = BigInt.DIV(first , second);
                Result.Text = (Answer.Item1).ToString(); //BigInt.DIV(first, second).ToString();
                sw.WriteLine(Result.Text);
                sw.Close();
                f.Close();
            }
            else if(input == 2)
            {
                FileStream f = new FileStream("SUB.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(f);
                string first = FirstNum.Text;
                string second = SecondNum.Text;
                Result.Text = BigInt.SUB(first, second).ToString();
                sw.WriteLine(Result.Text);
                sw.Close();
                f.Close();
            }
            else if(input == 3)
            {
                FileStream f = new FileStream("MUL.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(f);
                string first = FirstNum.Text;
                string second = SecondNum.Text;
                Result.Text = BigInt.MUL(first, second).ToString();
                sw.WriteLine(Result.Text);
                sw.Close();
                f.Close();
            }
            else if (input == 4)
            {
                string first = FirstNum.Text;
                string second = SecondNum.Text;
                string third = ThirdNum.Text;
                Result.Text = BigInt.ModOfPower(first, second , third).ToString();
            }
            else
            {
                MessageBox.Show("Invalid Choice");
            }
            Choice.Clear();
            FirstNum.Clear();
            SecondNum.Clear();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
