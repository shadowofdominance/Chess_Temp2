public sealed class Rook : SlidingPiece
{
    protected override void ShowLegalMoves()
    {
        HighlightInDirection(1, 0);
        HighlightInDirection(-1, 0);
        HighlightInDirection(0, 1);
        HighlightInDirection(0, -1);
    }
}
