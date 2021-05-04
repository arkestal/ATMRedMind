using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATMRedMind;
using System.Collections.Generic;

namespace ATMRedMind.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ListEntriesSuccededTest()
        {
            string expected = "Succeded";

            string actual = Program.ListEntries(1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ListEntriesFailedTest()
        {
            string expected = "Failed";

            string actual = Program.ListEntries(47);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AfterWithdrawalAmountTest()
        {
            int withdraw = 1800;
            int earmarked = 500;

            int expected = 1300;

            int actual = Program.AfterWithdrawalAmount(withdraw, earmarked);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CurrentBalanceTest()
        {
            int[,] notes = new int[3, 2] { { 1000, 3 }, { 500, 7 }, { 100, 16 } };

            int expected = 8100;

            int actual = Program.CurrentBalance(notes);

            Assert.AreEqual(expected, actual);
        }
    }

}
