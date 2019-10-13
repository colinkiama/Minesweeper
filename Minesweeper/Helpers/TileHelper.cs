using Minesweeper.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.Helpers
{
    public class TileHelper
    {
        private static int boardWidth = 9;

        public static void SetBoardWidth(int width)
        {
            boardWidth = width;
        }

        public static (int row, int column) GetPositionLeftOf(int oldRow, int oldColumn)
        {
            int column = oldColumn - 1;
            return (oldRow, column);
        }

        public static (int row, int column) GetPositionRightOf(int oldRow, int oldColumn)
        {
            int column = oldColumn + 1;
            return (oldRow, column);
        }

        public static (int row, int column) GetPositionAbove(int oldRow, int oldColumn)
        {
            int row = oldRow - 1;
            return (row, oldColumn);
        }

        public static (int row, int column) GetPositionBelow(int oldRow, int oldColumn)
        {
            int row = oldRow + 1;
            return (row, oldColumn);
        }

        public static (int row, int column) GetPositionTopLeftOf(int oldRow, int oldColumn)
        {
            int row = oldRow - 1;
            int column = oldColumn - 1;
            return (row, column);
        }

        public static (int row, int column) GetPositionTopRightOf(int oldRow, int oldColumn)
        {
            int row = oldRow - 1;
            int column = oldColumn + 1;
            return (row, column);
        }

        public static (int row, int column) GetPositionBottomLeftOf(int oldRow, int oldColumn)
        {
            int row = oldRow + 1;
            int column = oldColumn - 1;
            return (row, column);
        }

        public static (int row, int column) GetPositionBottomRightOf(int oldRow, int oldColumn)
        {
            int row = oldRow + 1;
            int column = oldColumn + 1;
            return (row, column);
        }

        public static bool CheckIfPositionIsOutOfBounds(int row, int column, PositionOffset positionOffset)
        {
            bool isOutOfBounds = true;
            switch (positionOffset)
            {
                case PositionOffset.Above:
                    isOutOfBounds = row < 0;
                    break;
                case PositionOffset.Below:
                    isOutOfBounds = row >= boardWidth;
                    break;
                case PositionOffset.Left:
                    isOutOfBounds = column < 0;
                    break;
                case PositionOffset.Right:
                    isOutOfBounds = column >= boardWidth;
                    break;
                case PositionOffset.TopLeft:
                    isOutOfBounds = (row < 0) || (column < 0);
                    break;
                case PositionOffset.TopRight:
                    isOutOfBounds = row < 0 || column >= boardWidth;
                    break;
                case PositionOffset.BottomLeft:
                    isOutOfBounds = row >= boardWidth || column < 0;
                    break;
                case PositionOffset.BottomRight:
                    isOutOfBounds = row >= boardWidth || column >= boardWidth;
                    break;

            }
            return isOutOfBounds;
        }
    }
}
