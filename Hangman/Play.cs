using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using System.IO;

namespace Hangman
{
    [Activity(Label = "Play", ScreenOrientation = ScreenOrientation.Portrait)]
    class Play: AppCompatActivity
    {
        //Declare object to set stage
        public TextView txtquiz;
        public ImageView imgstatus;
        Button bA, bB, bC, bD, bE, bF, bG, bH, bI, bJ, bK, bL, bM, bN, bO, bP, bQ, bR, bS, bT, bU, bV, bW, bX, bY, bZ;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.play_game);
            setstage();
            txtquiz = FindViewById<TextView>(Resource.Id.txtDisplay);
            imgstatus = FindViewById<ImageView>(Resource.Id.imgHangman);
            setQuiz();
            generateRandomWords();
            setGameWord();
            showPlaceholder();
            displayOnBoard();
            setScore();
            CheckGame.Attempt = 1;
            imgstatus.SetImageResource(CheckGame.ImageLookup[CheckGame.Attempt]);

           

        }
        public void setstage()
        {
            //Initialize keyboard buttons and assciate it with appropriate event handler
            bA = FindViewById<Button>(Resource.Id.btnA);
            bB = FindViewById<Button>(Resource.Id.btnB);
            bC = FindViewById<Button>(Resource.Id.btnC);
            bD = FindViewById<Button>(Resource.Id.btnD);
            bE = FindViewById<Button>(Resource.Id.btnE);
            bF = FindViewById<Button>(Resource.Id.btnF);
            bG = FindViewById<Button>(Resource.Id.btnG);
            bH = FindViewById<Button>(Resource.Id.btnH);
            bI = FindViewById<Button>(Resource.Id.btnI);
            bJ = FindViewById<Button>(Resource.Id.btnJ);
            bK = FindViewById<Button>(Resource.Id.btnK);
            bL = FindViewById<Button>(Resource.Id.btnL);
            bM = FindViewById<Button>(Resource.Id.btnM);
            bN = FindViewById<Button>(Resource.Id.btnN);
            bO = FindViewById<Button>(Resource.Id.btnO);
            bP = FindViewById<Button>(Resource.Id.btnP);
            bQ = FindViewById<Button>(Resource.Id.btnQ);
            bR = FindViewById<Button>(Resource.Id.btnR);
            bS = FindViewById<Button>(Resource.Id.btnS);
            bT = FindViewById<Button>(Resource.Id.btnT);
            bU = FindViewById<Button>(Resource.Id.btnU);
            bV = FindViewById<Button>(Resource.Id.btnV);
            bW = FindViewById<Button>(Resource.Id.btnW);
            bX = FindViewById<Button>(Resource.Id.btnX);
            bY = FindViewById<Button>(Resource.Id.btnY);
            bZ = FindViewById<Button>(Resource.Id.btnZ);
            bA.Click += checkButton;
            bB.Click += checkButton;
            bC.Click += checkButton;
            bD.Click += checkButton;
            bE.Click += checkButton;
            bF.Click += checkButton;
            bG.Click += checkButton;
            bH.Click += checkButton;
            bI.Click += checkButton;
            bJ.Click += checkButton;
            bK.Click += checkButton;
            bL.Click += checkButton;
            bM.Click += checkButton;
            bN.Click += checkButton;
            bO.Click += checkButton;
            bP.Click += checkButton;
            bQ.Click += checkButton;
            bR.Click += checkButton;
            bS.Click += checkButton;
            bT.Click += checkButton;
            bU.Click += checkButton;
            bV.Click += checkButton;
            bW.Click += checkButton;
            bX.Click += checkButton;
            bY.Click += checkButton;
            bZ.Click += checkButton;
        }
        public int checkScoreLetter(char letter)
        {
            // Check Letter score based upon user input
            switch (letter)
            {
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                case 'L':
                case 'N':
                case 'S':
                case 'T':
                case 'R':
                    return 1;

                case 'D':
                case 'G':
                    return 2;

                case 'B':
                case 'C':
                case 'M':
                case 'P':
                    return 3;

                case 'F':
                case 'H':
                case 'V':
                case 'W':
                case 'Y':
                    return 4;

                case 'K':
                    return 5;

                case 'J':
                case 'X':
                    return 8;

                case 'Q':
                case 'Z':
                    return 10;
                case '-':
                    return 0;

            }

            return 0;
        }
        private void checkButton(object sender, EventArgs e)
        {
           // Disable button once user select it 
            Button dbutton = sender as Button;
            dbutton.Enabled = false;
            dbutton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#A9A9A9"));
            verifyChar(Convert.ToChar(dbutton.Text.ToUpper()));
            displayOnBoard();
            imgstatus.SetImageResource(CheckGame.ImageLookup[CheckGame.Attempt]);
            checkFinish();

        }
        // Check game finished or not
        public void checkFinish()
        {
            if (limit())
            {
                CheckGame.WordGuessed = false;
                StartActivity(typeof(FinalScreen));
                Finish();

            }
            if (correctWord())
            {
                CheckGame.WordGuessed = true;
                StartActivity(typeof(FinalScreen));
                Finish();
            }
        }
       //Read data from the asset file and add all quiz words in the collection
        public void setQuiz()
        {
            if(CheckGame.WordList.Count==0)
            {
                int cnt = 0;
                try
                {
                    var res = Assets;
                    using (StreamReader sr = new StreamReader(res.Open("quiz.txt"))) 
                    {
                        while (!sr.EndOfStream)
                        {
                            string data = sr.ReadLine();
                            cnt++;
                            CheckGame.WordList.Add(data);
                        }

                    }
                }
                catch(Exception e)
                {
                    Toast.MakeText(this, "Unable to load data ", ToastLength.Short);

                }
            }
        }
        // Generate Random words based upon the difficulty level set by user.
        public void generateRandomWords()
        {
            string quiz = CheckGame.WordList[randomNo(CheckGame.WordList.Count)];
            if(CheckGame.Easy && getScore(quiz)<=CheckGame.EasyWordScoreCap )
            {
                CheckGame.GameWord = quiz;
            }
            else if(CheckGame.Normal && getScore(quiz) > CheckGame.EasyWordScoreCap && getScore(quiz)<=CheckGame.MediumWordScoreCap)
            {
                CheckGame.GameWord = quiz;
            }
            else if(CheckGame.Difficult && getScore(quiz)>CheckGame.MediumWordScoreCap)
            {
                CheckGame.GameWord = quiz;
            }
            else
            {
                generateRandomWords();
            }
        }
        public int randomNo(int No)
        {
            int rnd;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            rnd = random.Next(1, No);
            return rnd;
        }
        public void setGameWord()
        {
            CheckGame.GameWordCharArray = CheckGame.GameWord.ToCharArray();
        }
        //Show - placeholder for word guess
        public void showPlaceholder()
        {
            CheckGame.DisplayCharArray = CheckGame.GameWord.ToCharArray();
            for (int count = 0; count < CheckGame.DisplayCharArray.Length; count++)
            {
                if (CheckGame.DisplayCharArray[count] != '-')
                {
                    CheckGame.DisplayCharArray[count] = '_';
                }
            }
        }
        public void displayOnBoard()
        {
            CheckGame.DisplayString = "";
            foreach (char letter in CheckGame.DisplayCharArray)
            {
                CheckGame.DisplayString += letter + " ";
            }
            txtquiz.Text = CheckGame.DisplayString;
        }
        public void verifyChar(char l)
        {
            if (isAvailable(l))
            {
                updatedisplay(l);
            }
            else
            {
                CheckGame.Attempt++;
            }
        }
        //Check entered character matched in guess word
        public bool isAvailable(char t)
        {
            foreach (char w in CheckGame.GameWordCharArray)
            {
                if (t == w)
                {
                    return true;
                }
            }
            return false;
        }

        public void updatedisplay(char nl)
        {
            int i = 0;
            foreach (char wl in CheckGame.GameWordCharArray)
            {
                if (nl == wl)
                {
                    CheckGame.DisplayCharArray[i] = wl;
                }
                i++;
            }
        }

        public bool limit()
        {
            if (CheckGame.Attempt >= 14)
            {
                return true;
            }
            return false;
        }


        public bool correctWord()
        {
            foreach (char l in CheckGame.DisplayCharArray)
            {
                if (l == '_')
                {
                    return false;
                }

            }
            return true;
        }
        //Calculate score 
        public int getScore(string w)
        {
            int score = 0;
            foreach(char l in w)
            {
                score += checkScoreLetter(l);

            }
            return score;
        }
       
        public void setScore()
        {
            CheckGame.WordScore = 0;
            foreach(char l in CheckGame.GameWord)
            {
                CheckGame.WordScore += checkScoreLetter(l);
            }
        }
    }
}