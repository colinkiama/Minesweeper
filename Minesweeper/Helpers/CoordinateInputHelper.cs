using Minesweeper.Enums;
using Minesweeper.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.Helpers
{
    public class CoordinateInputHelper
    {
        public static InputParseResult ParseInput(string input)
        {
            InputParseResult result = new InputParseResult
            {
                ErrorResult = InputParseError.None,
                ParsedInput = int.MaxValue
            };

            var treatedInput = input.Trim();
            int parsedNumber;
            bool isNumber = int.TryParse(treatedInput, out parsedNumber);
            if (!isNumber)
            {
                result.ErrorResult = InputParseError.NotANumber;
            }

            else
            {
                if (!CheckIfNumberIsInRange(parsedNumber))
                {
                    result.ErrorResult = InputParseError.NumberOutOfRange;
                }
                else
                {
                    result.ParsedInput = parsedNumber;
                }
            }

            return result;
        }

        private static bool CheckIfNumberIsInRange(int parsedNumber)
        {
            return parsedNumber > 0 && parsedNumber < 10;
        }
    }
}
