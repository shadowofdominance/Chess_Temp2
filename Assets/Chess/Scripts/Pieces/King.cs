public sealed class King : BaseChessPiece
{
    private static readonly int[] RowOffsets = { -1, -1, -1,  0, 0,  1, 1, 1 };
    private static readonly int[] ColOffsets = { -1,  0,  1, -1, 1, -1, 0, 1 };

    protected override void ShowLegalMoves()
    {
        for (int i = 0; i < RowOffsets.Length; i++)
        {
            int r = Row + RowOffsets[i];
            int c = Column + ColOffsets[i];

            if (!IsInBounds(r, c)) continue;
            if (ChessBoardManager.Instance.IsFriendly(this, r, c)) continue;

            if (ChessBoardManager.Instance.IsEnemy(this, r, c))
                ChessBoardPlacementHandler.Instance.HighlightEnemy(r, c);
            else
                ChessBoardPlacementHandler.Instance.Highlight(r, c);
        }
    }
}
