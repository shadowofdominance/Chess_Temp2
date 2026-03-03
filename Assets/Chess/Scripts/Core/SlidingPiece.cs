public abstract class SlidingPiece : BaseChessPiece
{
    protected void HighlightInDirection(int rowDir, int colDir)
    {
        int r = Row + rowDir;
        int c = Column + colDir;

        while (IsInBounds(r, c))
        {
            if (ChessBoardManager.Instance.IsFriendly(this, r, c)) break;

            if (ChessBoardManager.Instance.IsEnemy(this, r, c))
            {
                ChessBoardPlacementHandler.Instance.HighlightEnemy(r, c);
                break;
            }

            ChessBoardPlacementHandler.Instance.Highlight(r, c);
            r += rowDir;
            c += colDir;
        }
    }
}
