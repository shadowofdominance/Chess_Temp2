using UnityEngine;

// sealed = no other class can inherit from this
public sealed class ChessBoardManager : MonoBehaviour
{
    // Singleton instance - lets any script access this class via ChessBoardManager.Instance
    internal static ChessBoardManager Instance { get; private set; }

    // Mirrors the physical board - each cell holds whichever piece is sitting on that square
    // null means the square is empty
    private BaseChessPiece[,] _boardState;

    private void Awake()
    {
        // Register this as the single instance when the scene loads
        Instance = this;

        // Initialize the board as completely empty (all nulls)
        _boardState = new BaseChessPiece[8, 8];
    }

    // Called by each piece in its Start() - records which square it is sitting on
    internal void RegisterPiece(BaseChessPiece piece, int row, int col)
    {
        _boardState[row, col] = piece;
    }

    // Returns true if ANY piece (friendly or enemy) is on this square
    internal bool IsOccupied(int row, int col) => _boardState[row, col] != null;

    // Returns true if the square has a piece belonging to the OPPOSITE team
    internal bool IsEnemy(BaseChessPiece piece, int row, int col)
    {
        var other = _boardState[row, col];
        // other.Team != piece.Team means they are on opposing sides
        return other != null && other.Team != piece.Team;
    }

    // Returns true if the square has a piece belonging to the SAME team
    internal bool IsFriendly(BaseChessPiece piece, int row, int col)
    {
        var other = _boardState[row, col];
        // other.Team == piece.Team means they are on the same side
        return other != null && other.Team == piece.Team;
    }
}
