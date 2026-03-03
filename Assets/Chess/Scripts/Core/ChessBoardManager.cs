using UnityEngine;

public sealed class ChessBoardManager : MonoBehaviour
{
    internal static ChessBoardManager Instance { get; private set; }

    private BaseChessPiece[,] _boardState;

    private void Awake()
    {
        Instance = this;
        _boardState = new BaseChessPiece[8, 8];
    }

    internal void RegisterPiece(BaseChessPiece piece, int row, int col)
    {
        _boardState[row, col] = piece;
    }

    internal bool IsOccupied(int row, int col) => _boardState[row, col] != null;

    internal bool IsEnemy(BaseChessPiece piece, int row, int col)
    {
        var other = _boardState[row, col];
        return other != null && other.Team != piece.Team;
    }

    internal bool IsFriendly(BaseChessPiece piece, int row, int col)
    {
        var other = _boardState[row, col];
        return other != null && other.Team == piece.Team;
    }
}
