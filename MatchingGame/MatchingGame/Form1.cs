using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        
        
        
        Label firstClicked = null;

        Label secondClicked = null;

        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "x", "x", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        int totalTime = 60;
        int countDownTime;
        private int score = 0;
        


        private void AssignIconsToSquares()
        {
            List<string> iconsCopy = new List<string>(icons);
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(iconsCopy.Count);
                    iconLabel.Text = iconsCopy[randomNumber];
                    iconsCopy.RemoveAt(randomNumber);
                    iconLabel.ForeColor = iconLabel.BackColor;

                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
            lblScore.Text = "Times won: 0";

            tableLayoutPanel1.Enabled = false;
            
        }

        private void RestartGame()
        {
            GameTimer.Start();
            countDownTime = totalTime;
            
            
            AssignIconsToSquares();


        }

       

        private void time_left(object sender, EventArgs e)
        {
            countDownTime--;

            lblTimerLeft.Text = ("Time left: " + countDownTime);

            if (countDownTime < 1)
            {
                GameOver("Out of time! ");
                return;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;

                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Stop();

            
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            
            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {
            
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        
                        return;
                }
            }
            GameTimer.Stop();
            countDownTime = 60;
            score++;
            lblScore.Text = "Times won: " + score.ToString();

            MessageBox.Show("You matched all the icons!", "Congratulations");
            
        }
        private void GameOver(string msg)
        {
            GameTimer.Stop();
            
            MessageBox.Show(msg + "Click Restart to try again.");
        }

        private void RestartGameEvent(object sender, EventArgs e)
        {
            RestartGame();
            tableLayoutPanel1.Enabled = true;
        }

        private void GameQuitEvent(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

       
    }
}
