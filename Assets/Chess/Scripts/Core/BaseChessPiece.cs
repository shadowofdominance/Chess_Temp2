using UnityEngine;
using Chess.Scripts.Core;

public abstract class BaseChessPiece : MonoBehaviour
{
    [SerializeField] private PieceTeam _team;
    public PieceTeam Team => _team;

    public int Row { get; protected set; }
    public int Column { get; protected set; }

    protected ChessPlayerPlacementHandler placementHandler;

    protected virtual void Awake()
    {
        placementHandler = GetComponent<ChessPlayerPlacementHandler>();
        Row = placementHandler.row;
        Column = placementHandler.column;
    }

    protected virtual void Start()
    {
        ChessBoardManager.Instance.RegisterPiece(this, Row, Column);
    }

    private void OnMouseDown()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        ShowLegalMoves();
    }

    protected abstract void ShowLegalMoves();

    protected bool IsInBounds(int row, int col) => row >= 0 && row < 8 && col >= 0 && col < 8;
}