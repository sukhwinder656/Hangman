using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Environment = Android.OS.Environment;

namespace Hangman
{
    //Model based on this class table will be created
   public class Player
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public String PlayerName { get; set; }
        public DateTime LastPlayDate { get; set; }
        public int BestStreak { get; set; }
        public int BestScore { get; set; }
        public DateTime BestScoreDate { get; set; }
        public string HardestWord { get; set; }
        public int HardestWordScore { get; set; }
        public DateTime HardestWordDate { get; set; }
        public int CurrentStreak { get; set; }
        public Player() { }

    }
}