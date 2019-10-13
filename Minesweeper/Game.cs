using Minesweeper.Enums;
using Minesweeper.Helpers;
using Minesweeper.Model;
using Minesweeper.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Minesweeper
{
    public class Game
    {
        int gameBoardSize = 9;
        public const int MineValue = -1;
        public const int BlankTileValue = 0;

        bool hasGameEnded = false;
        Board gameBoard;

        TileChangeResult lastTileChange = TileChangeResult.Revealed;
        int lastRow = 0;
        int lastColumn = 0;

        // Program will create this object then start a game with it. When game ends, object is destroyed 
        // => Context returns to program, which will switch the state back to none 
        internal void Start()
        {
            gameBoard = new Board(gameBoardSize);
            gameBoard.FillBoard();
            while (!hasGameEnded)
            {
                Console.Clear();
                gameBoard.DisplayBoard();
                if (lastTileChange == TileChangeResult.AlreadyRevealed)
                {
                    DisplayAlreadyRevealedTileMessage(lastRow, lastColumn);
                    SetDefaultLastRevealResult();
                    
                }
                else if (lastTileChange == TileChangeResult.FlagUnavailable)
                {
                    DisplayCannotFlagRevealedTileMessage();
                    SetDefaultLastRevealResult();
                }
                else if (lastTileChange == TileChangeResult.Mine)
                {
                    
                    hasGameEnded = true;
                    DisplayGameOverMessage();
                    SetDefaultLastRevealResult();
                    continue;
                }
                else
                {
                    bool hasPlayerWon = gameBoard.CheckIfPlayerHasWon();
                    if (hasPlayerWon)
                    {
                        hasGameEnded = true;
                        DisplayGameWinMessage();
                        continue;
                    }

                }
                RequestUserInput();
            }

            Console.Clear();
        }

        private void DisplayCannotFlagRevealedTileMessage()
        {
            Console.WriteLine($"Tile on row {lastRow}, column {lastColumn} has already been revealed.");
            Console.WriteLine("You can't flag a tile that has already been revealed");
        }

        private void SetDefaultLastRevealResult()
        {
            lastTileChange = TileChangeResult.Revealed;
        }

        private void DisplayGameWinMessage()
        {
            Console.WriteLine("You've flagged all the mines and have revealed all the other tiles. You win!");
            Thread.Sleep(3000);
        }

        private void DisplayGameOverMessage()
        {
            Console.WriteLine("BOOOOOOOOM!");
            Console.WriteLine($"You triggered a mine at row {lastRow}, column {lastColumn}. Game Over!");
            Thread.Sleep(3000);
        }

        private void DisplayAlreadyRevealedTileMessage(int lastRow, int lastColumn)
        {
            Console.WriteLine($"You have already revealed the tile on row {lastRow}, column {lastColumn}.");
        }

        private void RequestUserInput()
        {
            BoardOption boardOption = PickBoardOption();
            int inputRow = InputLoop("Enter row number (Vertical):");
            int inputColumn = InputLoop("Enter Column Number (Horizontal): ");
            lastRow = inputRow;
            lastColumn = inputColumn;
            // Takeaway 1 from inputs because the board array is zero-indexed
            HandlePositionInput(inputRow - 1, inputColumn - 1, boardOption);
        }

        private BoardOption PickBoardOption()
        {
            bool isValidOption = false;
            BoardOption boardOption = BoardOption.Flag;
            while (!isValidOption)
            {
                OptionHelper.PrintOption('r', "Reveal Tile");
                OptionHelper.PrintOption('f', "Flag Tile");
                string input = Console.ReadLine();
                bool isChar = char.TryParse(input.Trim(), out char inputChar);
                if (isChar)
                {
                    if (char.ToUpper(inputChar) == 'R')
                    {
                        isValidOption = true;
                        boardOption = BoardOption.Reveal;
                    }
                    else if (char.ToUpper(inputChar) == 'F')
                    {
                        isValidOption = true;
                        boardOption = BoardOption.Flag;
                    }
                }
                if (!isValidOption)
                {
                    Console.WriteLine("You didn't pick one of the listed options. Please try again.");
                }
            }

            return boardOption;

        }

        private void HandlePositionInput(int rowNumber, int columnNumber, BoardOption boardOption)
        {
            switch (boardOption)
            {
                case BoardOption.Flag:
                    lastTileChange = gameBoard.FlagTile(rowNumber, columnNumber);
                    break;
                case BoardOption.Reveal:
                    lastTileChange = gameBoard.RevealTile(rowNumber, columnNumber);
                    break;
            }

        }

        private int InputLoop(string inputMessage)
        {
            int validInput = int.MaxValue;
            bool wasCorrectInput = false;

            while (!wasCorrectInput)
            {
                Console.WriteLine(inputMessage);
                string input = Console.ReadLine();
                InputParseResult parseResult = CoordinateInputHelper.ParseInput(input);
                if (parseResult.ErrorResult != Enums.InputParseError.None)
                {
                    DisplayErrorMessage(parseResult.ErrorResult, input);
                }
                else
                {
                    validInput = parseResult.ParsedInput;
                    wasCorrectInput = true;
                }
            }

            return validInput;
        }



        private void DisplayErrorMessage(InputParseError errorResult, string input)
        {
            switch (errorResult)
            {
                case InputParseError.NumberOutOfRange:
                    Console.WriteLine($"The value you entered: \"{input}\" is outside of the range between " +
                        $"1 and {gameBoardSize}");
                    break;
                case InputParseError.NotANumber:
                    Console.WriteLine($"The value you entered: \"{input}\" is not an Integer number");
                    break;
                default:
                    break;
            }
        }


    }
}
