using System;
using System.Collections.Generic;
namespace Yahtzeeish
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            //Roll 5 dice, ask user which they want to keep
            //roll dice again, ask user which they want to keep
            //then roll remaining dice
            //add up number of dice that was rolled the most (ex: 5,4,3,4,4 would be 3 as 3 4's were rolled)
            //computer rolls 5 dice 3 times, and takes the highest number of dice rolled
            //compare computer rolls to user rolls and print the winner with the tie going to user.

            // Generate Initial Roll
            int[] firstRoll = new int[5];
            firstRoll = RollDice(5);

            Console.WriteLine("Yahtzee!!");
            DisplayDiceRoll(firstRoll);

            Console.WriteLine("Which of the 5 dice do you want to keep? (separate by comma) ");
            string userChoice = Console.ReadLine();

            // Keep user's selected dice (the value at those indexes)
            int[] userKetpDice = ConvertUserChoiceToKeptDice(firstRoll, userChoice);

            // Roll Remaining Dice into "secondRoll"
            int[] secondRoll = RollRemainingDice(userKetpDice);

            // Combine Rolled Dice and Kept dice
            int[] secondKeptAndRolled = CombineTwoIntArrays(userKetpDice, secondRoll);

            DisplayDiceRoll(secondKeptAndRolled);

            Console.WriteLine("Which of these dice do you want to keep? (separate by comma) ");
            string secondUserChoice = Console.ReadLine();

            // Keep user's selected dice (the value at those indexes)
            int[] userKeptDice2 = ConvertUserChoiceToKeptDice(secondKeptAndRolled, secondUserChoice);

            // Roll Remaining Dice into "thirdRoll"
            int[] thirdRoll = RollRemainingDice(userKeptDice2);

            // Combine Rolled Dice and Kept dice
            int[] thirdKeptAndRolled = CombineTwoIntArrays(userKeptDice2, thirdRoll);

            DisplayDiceRoll(thirdKeptAndRolled);

            int playerScore = DuplicateCount(thirdKeptAndRolled);

            // Roll 3 times for the Computer
            int[] computerRoll1 = RollDice(5);
            int[] computerRoll2 = RollDice(5);
            int[] computerRoll3 = RollDice(5);

            // Find computer's highest score
            int[] computerScores = { DuplicateCount(computerRoll1),
                                DuplicateCount(computerRoll2),
                                DuplicateCount(computerRoll3) };
            int computerHighestScore = CalculateComputerScore(computerScores);

            DisplayWinner(playerScore, computerHighestScore);

            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}" +
                $"{Environment.NewLine}Press Enter to Exit.");
            Console.ReadLine();
        }

        private static int[] RollRemainingDice(int[] userKetpDice)
        {
            int remainingDice1 = 5 - userKetpDice.Length;
            int[] secondRoll = RollDice(remainingDice1);

            return secondRoll;
        }

        private static int[] ConvertUserChoiceToKeptDice(int[] diceRolls, string userChoice)
        {
            string[] keptDiceString = userChoice.Split(',');
            int[] userKetpDice = new int[keptDiceString.Length];
            for (int i = 0; i < keptDiceString.Length; i++)
            {
                int keptDice1 = int.Parse(keptDiceString[i]) - 1;
                userKetpDice[i] = diceRolls[keptDice1];
            }

            return userKetpDice;
        }

        private static int[] RollDice(int numberOfRolls)
        {
            int[] rollDice = new int[numberOfRolls];

            for (int i = 0; i < numberOfRolls; i++)
            {

                rollDice[i] = rand.Next(1, 6);

            }
            return rollDice;
        }

        private static void DisplayDiceRoll(int[] diceRolls)
        {
            Console.WriteLine("Here is your roll:");

            for (int i = 0; i < diceRolls.Length; i++)
            {
                Console.Write(i + 1 + ": ");
                Console.WriteLine(diceRolls[i]);
            }
        }

        private static int[] CombineTwoIntArrays(int[] diceRolls1, int[] diceRolls2)
        {
            int[] combinedArray = new int[diceRolls1.Length + diceRolls2.Length];
            diceRolls1.CopyTo(combinedArray, 0);
            diceRolls2.CopyTo(combinedArray, diceRolls1.Length);

            return combinedArray;
        }

        private static int DuplicateCount(int[] rolledDice)
        {
            var diceDict = new Dictionary<int, int>();

            int maxValue = 0;

            foreach (var value in rolledDice)
            {
                if (diceDict.ContainsKey(value))
                    diceDict[value]++;
                else
                    diceDict[value] = 1;
            }
            foreach (var key in diceDict)
            {
                if (key.Value > maxValue)
                {
                    maxValue = key.Value;
                }
            }

            return maxValue;
        }

        private static int CalculateComputerScore(int[] computerScores)
        {
            int computerHighestScore = 0;
            for (int i = 0; i < 3; i++)
            {
                if (computerScores[i] > computerHighestScore)
                {
                    computerHighestScore = computerScores[i];

                }
            }

            return computerHighestScore;
        }

        private static void DisplayWinner(int playerScore, int computerHighestScore)
        {
            if (playerScore >= computerHighestScore)
            {
                Console.WriteLine($"Computer Scored: {computerHighestScore}");
                Console.WriteLine($"You Scored: {playerScore}");
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine($"Computer Scored: {computerHighestScore}");
                Console.WriteLine($"You Scored: {playerScore}");
                Console.Write("Computer Wins!");
            }
        }
    }
}
