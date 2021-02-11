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

namespace Hangman
{
   public class Score
    {
        public int checkScoreLetter(char letter)
        {

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
    }
}