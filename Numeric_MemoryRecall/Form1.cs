using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
namespace CritiCall_MemoryRecall
{
    public partial class Form1 : Form
    {
        public string phoneNumber;
        public int correct = 0;
        public int wrong = 0;
        public int tryCounter = 0;

        SpeechSynthesizer reader = new SpeechSynthesizer();
        

        public Form1()
        {
            InitializeComponent();
            btnVerify.Enabled = false;
            txtPhone.Enabled = false;

            lblCorrect.Text = correct.ToString();
            lblWrong.Text = wrong.ToString();
            lblNumber.Text = "";
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            if(btnBegin.Text == "Reset")
            {
                lblCorrect.Text = "0";
                lblWrong.Text = "0";
                correct = 0;
                wrong = 0;
                tryCounter = 0;
                lblNumber.Text = "";
                txtPhone.Text = "";
                btnBegin.Text = "Begin";
            }
            else
            {
                btnBegin.Enabled = false;
                btnBegin.Text = "Reset";
                RunTest();
                btnBegin.Enabled = true;
            }
        }
        public void RunTest()
        {
            
            phoneNumber = GenerateNumber();
            
            reader.Rate = -2;
            lblNumber.Text = "";
            reader.Speak(phoneNumber);
            btnVerify.Enabled = true;
            txtPhone.Enabled = true;
            txtPhone.Focus();
            btnVerify.Text = "Verify";
        }
        public string GenerateNumber()
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 8; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }

        public char LetterGradeFromNumber(double marks)
        {
            if (marks >= 90)
                return 'A';
            else if (marks >= 80)
                return 'B';
            else if (marks >= 70)
                return 'C';
            else if (marks >= 60)
                return 'D';
            else if (marks >= 0)
                return 'F';
            else
                return 'U'; // unclassified
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (tryCounter == 10)
            {
                txtPhone.Text = "";
                txtPhone.Enabled = false;
                btnVerify.Enabled = false;
                txtPhone.Enabled = false;

                double avg = (double)correct / (double)10;

                double finalscore = avg * (double)100;
                char score = LetterGradeFromNumber(finalscore);

                string message = "Your Score is: " + finalscore.ToString() +"%\nYour grade is: " + score + "\nYou had " + wrong.ToString() + " incorrect"; 

                MessageBox.Show(message,"Final Results", MessageBoxButtons.OK,MessageBoxIcon.Information);


            }
            else
            {
                if (btnVerify.Text == "Next")
                {
                    txtPhone.Text = "";
                    txtPhone.Enabled = false;
                    btnVerify.Enabled = false;
                    RunTest();
                }
                else
                {
                    if (txtPhone.Text == phoneNumber)
                    {


                        lblNumber.Text = phoneNumber;
                        correct += 1;
                        tryCounter += 1;
                        System.Media.SystemSounds.Question.Play();
                        MessageBox.Show("Correct");

                        lblCorrect.Text = correct.ToString();
                        lblWrong.Text = wrong.ToString();
                        txtPhone.Enabled = false;
                        btnVerify.Text = "Next";
                    }
                    else
                    {
                        lblNumber.Text = phoneNumber;
                        wrong += 1;
                        tryCounter += 1;
                        System.Media.SystemSounds.Exclamation.Play();
                        MessageBox.Show("Incorrect");
                        lblCorrect.Text = correct.ToString();
                        lblWrong.Text = wrong.ToString();
                        txtPhone.Enabled = false;
                        btnVerify.Text = "Next";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
