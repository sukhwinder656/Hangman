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
using Android.Text;

namespace Hangman
{
    [Activity(Label = "Final Screen", ScreenOrientation = ScreenOrientation.Portrait)]
    class FinalScreen: AppCompatActivity
    {
        ImageView imgstatus;
        Button btnquit,btnplay;
        TextView txtname, txtscore;
        DatabaseManager dbc;
        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);
            SetContentView(Resource.Layout.final_screen);
            txtname = FindViewById<TextView>(Resource.Id.txtplayerName);
            txtscore = FindViewById<TextView>(Resource.Id.txtscore);
            imgstatus = FindViewById<ImageView>(Resource.Id.imgfinal);
         //   imgstatus.SetImageResource(CheckGame.ImageLookup[CheckGame.Attempt]);
            btnquit = FindViewById<Button>(Resource.Id.btnquit);
            btnplay = FindViewById<Button>(Resource.Id.btnplayagain);
            btnplay.Click += Btnplay_Click;
            btnquit.Click += Btnquit_Click;
            dbc = new DatabaseManager();
            // Display image based on result of the game
            if(CheckGame.WordGuessed)
            {
                imgstatus.SetImageResource(Resource.Drawable.trophyw);
                Won();
                
            }
            else
            {
                imgstatus.SetImageResource(Resource.Drawable.tryagainl);
                Loose();
                
            }
            FindViewById<TextView>(Resource.Id.txtplayerName).SetText(Html.FromHtml(CheckGame.DisplayString),TextView.BufferType.Normal);
        }

        private void Btnquit_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Btnplay_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Play));
            Finish();
        }

        public void Won()
        {
            Display(true);
            txtscore.Text = "Hurray ! You won Score = " + CheckGame.WordScore;
            updatewinboard();
        }
        public void Loose()
        {
            Display(false);
            txtscore.Text = "You lost! Better luck next time";
            updatelossboard();

        }
        //Store player score data
        public void updatewinboard()
        {
            CheckGame.CurrentPlayer.LastPlayDate = DateTime.Now;
            CheckGame.CurrentPlayer.CurrentStreak += CheckGame.WordScore;
            if(CheckGame.CurrentPlayer.BestStreak<CheckGame.CurrentPlayer.CurrentStreak)
            {
                CheckGame.CurrentPlayer.BestStreak = CheckGame.CurrentPlayer.CurrentStreak;
            }
            if(CheckGame.WordScore>CheckGame.CurrentPlayer.HardestWordScore)
            {
                CheckGame.CurrentPlayer.HardestWordScore = CheckGame.WordScore;
                CheckGame.CurrentPlayer.HardestWord = CheckGame.GameWord;
                CheckGame.CurrentPlayer.HardestWordDate = DateTime.Today;
            }
            dbc.dbconn.Update(CheckGame.CurrentPlayer);

        }
        public void updatelossboard()
        {
            CheckGame.CurrentPlayer.LastPlayDate = DateTime.Today;
            if(CheckGame.CurrentPlayer.BestStreak<CheckGame.CurrentPlayer.CurrentStreak)
            {
                CheckGame.CurrentPlayer.BestStreak = CheckGame.CurrentPlayer.CurrentStreak;
            }
            CheckGame.CurrentPlayer.CurrentStreak = 0;
            dbc.dbconn.Update(CheckGame.CurrentPlayer);
        }
      //Show correct word
        public void Display(bool status)
        {
            CheckGame.DisplayString = "";
            int i = 0;
            if(status)
            {
                foreach(char l in CheckGame.DisplayCharArray)
                {
                    if(l=='_')
                    {
                        CheckGame.DisplayString += CheckGame.GameWordCharArray[i]+" ";
                    }
                    else
                    {
                        CheckGame.DisplayString += l + " ";
                    }
                    i++;
                }
            }
            else
            {
                foreach(char l in CheckGame.GameWordCharArray)
                {
                    CheckGame.DisplayString += l + " ";
                }
            }
        }
    }
}