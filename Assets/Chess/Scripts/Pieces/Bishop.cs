public sealed class Bishop : SlidingPiece
{
    protected override void ShowLegalMoves()
    {
        HighlightInDirection(1, 1);
        HighlightInDirection(1, -1);
        HighlightInDirection(-1, 1);
        HighlightInDirection(-1, -1);
    }
}
