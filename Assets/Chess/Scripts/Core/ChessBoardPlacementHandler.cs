using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
// sealed = no other class can inherit from this
public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    // The board is made of 8 rows, each row is a GameObject containing 8 tile children
    // This array holds references to those 8 row GameObjects, assigned via the Inspector
    [SerializeField] private GameObject[] _rowsArray;

    // The prefab that gets spawned on a tile to visually highlight it
    [SerializeField] private GameObject _highlightPrefab;

    // A 2D array representing the 8x8 board - each cell holds the tile GameObject at that position
    private GameObject[,] _chessBoard;

    // Singleton instance - lets any script access this class via ChessBoardPlacementHandler.Instance
    internal static ChessBoardPlacementHandler Instance;

    private void Awake()
    {
        // Register this as the single instance when the scene loads
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                // Each row GameObject has 8 child tiles - grab them by index and store in our 2D array
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    // Returns the tile GameObject at the given row and column
    internal GameObject GetTile(int i, int j)
    {
        try
        {
            return _chessBoard[i, j];
        }
        catch (Exception)
        {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    // Spawns a highlight prefab on top of the tile at the given row and column (default color)
    internal void Highlight(int row, int col)
    {
        var tile = GetTile(row, col).transform;
        if (tile == null)
        {
            Debug.LogError("Invalid row or column.");
            return;
        }

        // Instantiate places the highlight prefab at the tile's world position, parented to that tile
        Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
    }

    // Same as Highlight but tints the spawned prefab red - indicates an enemy that can be captured
    internal void HighlightEnemy(int row, int col)
    {
        var tile = GetTile(row, col).transform;
        if (tile == null)
        {
            Debug.LogError("Invalid row or column.");
            return;
        }

        var highlight = Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);

        // Reusing the same prefab - changing its color at runtime instead of making a separate red prefab
        var spriteRenderer = highlight.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;
    }

    // Removes all highlight GameObjects from every tile on the board
    internal void ClearHighlights()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                var tile = GetTile(i, j);
                // Skip tiles that have no children (nothing highlighted there)
                if (tile.transform.childCount <= 0) continue;
                // Destroy every child (highlight object) under this tile
                foreach (Transform childTransform in tile.transform)
                {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }


    #region Highlight Testing

    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion
}