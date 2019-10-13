using Minesweeper.Enums;
using Minesweeper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        static readonly List<char> availableOptionCharacters = new List<char> { '1', 'q' };
        private static bool hasUserQuitProgram = false;
        public static GameState CurrentState = GameState.None;
        static void Main(string[] args)
        {
            DisplayMainMenuOptions();
            while (!hasUserQuitProgram)
            {
                switch (CurrentState)
                {
                    case GameState.MainMenu:
                        ReadUserOptionInput();
                        break;
                    case GameState.None:
                        DisplayMainMenuOptions();
                        break;
                    default:
                        break;
                }

            }
            Console.WriteLine("See you next time! ;)");
            Thread.Sleep(3000);
        }

        private static void ReadUserOptionInput()
        {
            string optionInput = Console.ReadLine();
            var trimmedInput = optionInput.Trim();
            char parsedOption;
            bool isValidChar = char.TryParse(trimmedInput, out parsedOption);
            if (!isValidChar || !CheckIfValidOption(parsedOption))
            {
                DisplayInvalidOptionMessage();
            }
            else
            {
                HandleOption(char.ToUpper(parsedOption));
            }
        }

        private static void HandleOption(char parsedOption)
        {
            switch (parsedOption)
            {
                case '1':
                    StartGame();
                    break;
                case 'Q':
                    hasUserQuitProgram = true;
                    break;
            }
        }

        private static void StartGame()
        {
            Game currentGame = new Game();
            CurrentState = GameState.InGame;
            currentGame.Start();

            // Game ends
            CurrentState = GameState.None;

        }

        private static void DisplayInvalidOptionMessage()
        {
            Console.WriteLine("You did not enter a valid option, please try again");
            CurrentState = GameState.None;
        }

        private static bool CheckIfValidOption(char parsedOption)
        {
            return availableOptionCharacters.Contains(parsedOption);
        }

        private static void DisplayMainMenuOptions()
        {
            DisplayTitleAndCredits();
            OptionHelper.PrintOption('1', "Start new game");
            OptionHelper.PrintOption('q', "Quit");
            CurrentState = GameState.MainMenu;
        }

        private static void DisplayTitleAndCredits()
        {
            Console.WriteLine("Minesweeper - Created by Colin Kiama, 2019");
            Console.WriteLine("Check out my Github: http://www.github.com/colinkiama");
        }
    }
}
