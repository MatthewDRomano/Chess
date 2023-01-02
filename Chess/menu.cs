using System;
using System.Windows.Forms;

namespace Chess
{
    public partial class menu : Form
    {
        int timerTime = 5;
        public menu()
        {
            InitializeComponent();
        }
        private void TwoPlayers(object sender, EventArgs e)
        {
            this.Hide();            
            Form1 game = new Form1(timerTime);
            game.ShowDialog();
            this.Close();
        }
        private void Settings(object sender, EventArgs e)
        {
            button1.Visible = false;
            button5.Location = new System.Drawing.Point(50, 40);
            button5.Visible = true;

            button2.Visible = false;
            button6.Location = new System.Drawing.Point(50, 115);
            button6.Visible = true;

            button3.Visible = false;
            button7.Location = new System.Drawing.Point(50, 190);
            button7.Visible = true;

            button4.Visible = false;
            button8.Location = new System.Drawing.Point(50, 265);
            button8.Visible = true;

            backButton.Visible = true;
            button9.Visible = true;
        }
        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Back(object sender, MouseEventArgs e)
        {
            button1.Visible = true;            
            button5.Visible = false;

            button2.Visible = true;
            button6.Visible = false;

            button3.Visible = true;
            button7.Visible = false;

            button4.Visible = true;
            button8.Visible = false;

            backButton.Visible = false;
            button9.Visible = false;
        }
        private void setTime(object sender, MouseEventArgs e)
        {
            String temp = ((Button)sender).Tag.ToString();
            timerTime = Convert.ToInt32(temp[0] - '0');
            if (temp.Length > 1) timerTime += 9;
        }
    }
}
