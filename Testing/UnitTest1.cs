using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hangman;
using System.Collections.Generic;
using System.Linq;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Check if the correct score evaluates according to the character entered.
            Score s = new Score();
            int score=s.checkScoreLetter('W');
            Assert.AreEqual(4, score);
          
        }
    }
}
