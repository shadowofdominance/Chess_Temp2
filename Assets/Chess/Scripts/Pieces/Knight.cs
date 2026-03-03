public sealed class Knight : BaseChessPiece
{
    private static readonly int[] RowOffsets = { -2, -2, -1, -1, 1, 1, 2, 2 };
    private static readonly int[] ColOffsets = { -1,  1, -2,  2, -2, 2, -1, 1 };

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
