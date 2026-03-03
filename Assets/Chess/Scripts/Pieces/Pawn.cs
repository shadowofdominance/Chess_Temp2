public sealed class Pawn : BaseChessPiece
{
    protected override void ShowLegalMoves()
    {
        int dir = Team == PieceTeam.White ? 1 : -1;
        int startRow = Team == PieceTeam.White ? 1 : 6;
        int nextRow = Row + dir;

        if (IsInBounds(nextRow, Column) && !ChessBoardManager.Instance.IsOccupied(nextRow, Column))
        {
            ChessBoardPlacementHandler.Instance.Highlight(nextRow, Column);

            if (Row == startRow)
            {
                int doubleRow = Row + 2 * dir;
                if (IsInBounds(doubleRow, Column) && !ChessBoardManager.Instance.IsOccupied(doubleRow, Column))
                    ChessBoardPlacementHandler.Instance.Highlight(doubleRow, Column);
            }
        }

        int[] captureCols = { Column - 1, Column + 1 };
        foreach (int captureCol in captureCols)
        {
            if (IsInBounds(nextRow, captureCol) && ChessBoardManager.Instance.IsEnemy(this, nextRow, captureCol))
                ChessBoardPlacementHandler.Instance.HighlightEnemy(nextRow, captureCol);
        }
    }
}
