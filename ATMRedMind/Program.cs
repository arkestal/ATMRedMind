using System;
using System.Collections.Generic;

namespace ATMRedMind
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[,] notes = new int[3, 2] { { 1000, 2 }, { 500, 3 }, { 100, 5 } };
            List<Withdraw> withdrawLists = new();
            int counter = 0;
            bool isRunning = true;
            while (isRunning && counter < 7)
            {
                counter++;
                switch (counter)
                {
                    case 1:
                        WithdrawalSequence("1500", withdrawLists, notes);
                        break;
                    case 2:
                        WithdrawalSequence("700", withdrawLists, notes);
                        break;
                    case 3:
                        WithdrawalSequence("400", withdrawLists, notes);
                        break;
                    case 4:
                        WithdrawalSequence("1100", withdrawLists, notes);
                        break;
                    case 5:
                        WithdrawalSequence("1000", withdrawLists, notes);
                        break;
                    case 6:
                        WithdrawalSequence("700", withdrawLists, notes);
                        break;
                    case 7:
                        WithdrawalSequence("300", withdrawLists, notes);
                        break;
                    default:
                        break;
                }
            }
            int totalAmount;
            Console.Clear();
            ListOfWithdrawals(withdrawLists);
            Console.WriteLine($"\nMoney left in the ATM: {totalAmount = CurrentBalance(notes)}:-\nPress [Enter] to exit.");
            Console.ReadLine();
        }

        public static int[,] WithdrawalSequence(string value, List<Withdraw> withdrawLists, int[,] notes)
        {
            int totalAmount = CurrentBalance(notes);
            int withdraw = 0;
            int _1000CounterTemp = 0;
            int _500CounterTemp = 0;
            int _100CounterTemp = 0;

            Console.WriteLine("\n----------------------------------------------\n");
            Console.Write($"Welcome to the RedMind ATM.\nYour current balance is {totalAmount}:-\nEnter [list] to see earlier withdrawals\nOtherwise, please enter the amount you would like to withdraw: ");
            string input = value; //Can be swapped for a Console.ReadLine();
            Console.WriteLine();

            if (input == "list")
            {
                if (withdrawLists.Count == 0)
                {
                    Console.WriteLine("There are no withdrawals so far!");
                }
                else
                {
                    ListOfWithdrawals(withdrawLists);
                }
            }
            else
            {
                bool acceptedInput = true;
                while (acceptedInput)
                {
                    try
                    {
                        withdraw = int.Parse(input);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please only use digits for the amount you would like to withdraw!");
                        break;
                    }

                    var listEntry = new Withdraw();

                    if (withdraw % 100 != 0)
                    {
                        Console.WriteLine("Please select an amount that is divisible by 100.");
                        AddingListItem(withdrawLists, withdraw, listEntry, 2);
                    }
                    else if (withdraw > totalAmount)
                    {
                        Console.WriteLine("Insufficient funds. Please select a lower amount.");
                        AddingListItem(withdrawLists, withdraw, listEntry, 2);
                    }
                    else
                    {
                        int earmarked = 0;
                        int originWithdrawal = withdraw;

                        while (withdraw >= 1000 && notes[0, 1] > 0)
                        {
                            earmarked += 1000;
                            withdraw = AfterWithdrawalAmount(withdraw, 1000);
                            _1000CounterTemp++;
                            notes[0, 1]--;
                        }
                        while (withdraw >= 500 && notes[1, 1] > 0)
                        {
                            earmarked += 500;
                            withdraw = AfterWithdrawalAmount(withdraw, 500);
                            _500CounterTemp++;
                            notes[1, 1]--;
                        }
                        while (withdraw >= 100 && notes[2, 1] > 0)
                        {
                            earmarked += 100;
                            withdraw = AfterWithdrawalAmount(withdraw, 100);
                            _100CounterTemp++;
                            notes[2, 1]--;
                        }
                        if (originWithdrawal != earmarked)
                        {
                            Console.WriteLine("The ATM can't process your withdrawal request due to lack of bills.");
                            notes[0, 1] += _1000CounterTemp;
                            notes[1, 1] += _500CounterTemp;
                            notes[2, 1] += _100CounterTemp;

                            AddingListItem(withdrawLists, originWithdrawal, listEntry, 2);
                        }
                        else
                        {
                            totalAmount -= originWithdrawal;
                            Console.WriteLine($"Please collect your money: {earmarked}:-\n\t{_1000CounterTemp} X 1000\n\t{_500CounterTemp} X 500\n\t{_100CounterTemp} X 100");

                            AddingListItem(withdrawLists, originWithdrawal, listEntry, 1);
                        }
                    }
                    acceptedInput = false;
                }
            }
            return notes;
        }

        public static int CurrentBalance(int[,] notes)
        {
            return (notes[0, 0] * notes[0, 1]) + (notes[1, 0] * notes[1, 1]) + (notes[2, 0] * notes[2, 1]);
        }

        private static void ListOfWithdrawals(List<Withdraw> withdrawLists)
        {
            int counter = 1;
            foreach (var transaction in withdrawLists)
            {
                Console.WriteLine($"{counter++}.\tAmount: {transaction.Amount}:-\tResult: {transaction.Result}");
            }
        }

        public static void AddingListItem(List<Withdraw> withdrawLists, int withdraw, Withdraw listEntry, int value)
        {
            listEntry.Amount = withdraw;
            listEntry.Result = ListEntries(value);
            withdrawLists.Add(listEntry);
        }

        public static string ListEntries(int value)
        {
            string result;
            if (value == 1)
            {
                result = "Succeded";
            }
            else
            {
                result = "Failed";
            }
            return result;
        }

        public static int AfterWithdrawalAmount(int withdraw, int earmarked)
        {
            withdraw -= earmarked;
            return withdraw;
        }
    }
}
