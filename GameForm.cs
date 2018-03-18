// Daniel Hamar July 31, 2017
// Big Honking Guessing Game

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessingGame
{
    // Guessing game form
    public partial class gameForm : Form
    {
        int rndNum;
        int savedGuess = 0;

        public gameForm ( )
        {
            rndNum = randNum ( );  // initialize the first random number
            InitializeComponent ( );
        } // end gameForm()

        private void btnSubmit_Click ( object sender, EventArgs e )
        {
            // Check to see if the guess was right
            if ( savedGuess != rndNum )
            {
                if (savedGuess == 0)
                {
                    // set savedGuess value if 0
                    savedGuess = int.Parse(inputBox.Text);
                    this.BackColor = Color.Khaki;
                    closeGuess();
                    coolWarmLabel.Text = "Guess again.";
                } // end if
                else
                {
                    // change background color to indicate colder or warmer
                    if (Math.Abs(rndNum - int.Parse(inputBox.Text)) < Math.Abs(rndNum - savedGuess))
                        guessWarmer();
                    else if (Math.Abs(rndNum - int.Parse(inputBox.Text)) > Math.Abs(rndNum - savedGuess))
                        guessCooler();
                    else if (Math.Abs(rndNum - int.Parse(inputBox.Text)) == Math.Abs(rndNum - savedGuess))
                        coolWarmLabel.Text = "No Change";

                    // check for high, low, or correct and show in label
                    closeGuess();
                    //set the value for savedGuess for the next round
                    savedGuess = int.Parse(inputBox.Text);
                } // end else
            } // end if
            else
            {
                // if the number is correct, call guessRight(), and disable the submit and input box
                guessRight();
                closenessLabel.Text = "Awesome! You got it!";
                btnSubmit.Enabled = false;
                inputBox.Enabled = false;
            } // end else
        } // end btnSubmit_Click

        private void btnReset_Click ( object sender, EventArgs e )
        {
            // on clicking the Start New Game button
            rndNum = randNum(); // re-initialize a random number
            btnSubmit.Enabled = true; // enable the submit button
            inputBox.Enabled = true; // enable the input box
            closenessLabel.Text = ""; // remove the text from the label
            coolWarmLabel.Text = ""; // remove the text from the label
            inputBox.Text = ""; // remove the text from the text box
            this.BackColor = SystemColors.Control; // reset the background color
            savedGuess = 0; // reset the savedGuess button
        } // end btnReset_Click

        private static int randNum ( )
        {
            // Instantiate class Random to obtain a value between 1 and 1000
            Random rnd = new Random ( );
            int value = rnd.Next ( 1, 1000 );
            return value;
        } // end randNum()

        private void guessCooler()
        {
            // when guess is farther away than the last guess
            this.BackColor = Color.LightBlue; // set back ground color
            coolWarmLabel.Text = "Colder"; // set the label text            
        } // end guessCooler()

        private void guessWarmer()
        {
            // when guess is closer than the last guess
            this.BackColor = Color.Red; // set back ground color
            coolWarmLabel.Text = "Warmer"; // set the label text
        } // end guessWarmer()

        private void guessRight()
        {
            // when guess is correct
            this.BackColor = Color.Green; // set back ground color
            coolWarmLabel.Text = "Just Right"; // set the label text
        } // end guessRight()

        private void closeGuess()
        {
            // check to see if the guess is too low or too high
            if (int.Parse(inputBox.Text) < rndNum)
                closenessLabel.Text = "Too Low.  Try again";
            else if (int.Parse(inputBox.Text) > rndNum)
                closenessLabel.Text = "Too High.  Try again";
        } // end closeGuess()
    } // end class GameForm
} // end namespace GuessingGame
