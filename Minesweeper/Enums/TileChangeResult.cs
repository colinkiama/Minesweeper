using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.Enums
{
    public enum TileChangeResult
    {
        AlreadyRevealed,
        Mine,
        Revealed,
        UnFlagged,
        Flagged,
        FlagUnavailable
    }
}
