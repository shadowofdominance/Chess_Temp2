# Chess Possible Moves Calculator

## Primary Goal
Highlight all legal moves for a chess piece when clicked, and highlight enemy pieces in **red** if they can be captured.

---

## Architecture

### Class Hierarchy
```
MonoBehaviour
└── BaseChessPiece          (abstract) — click handler, shared state
    ├── Pawn
    ├── Knight
    ├── King
    └── SlidingPiece        (abstract) — directional ray casting
        ├── Rook
        ├── Bishop
        └── Queen
```

### Core Classes
| Class | Role |
|---|---|
| `ChessBoardPlacementHandler` | Singleton — manages tile references, spawns highlight prefabs |
| `ChessBoardManager` | Singleton — tracks all `BaseChessPiece` positions on an `[8,8]` grid |
| `ChessPlayerPlacementHandler` | Component — stores `row/column`, positions piece on the board at `Start` |
| `PieceTeam` | Enum — `White` / `Black` team identity |

---

## How It Works

1. **Piece Registration** — Each piece reads its `row/column` from `ChessPlayerPlacementHandler` in `Awake`, then registers itself into `ChessBoardManager`'s board state in `Start`.

2. **Click Detection** — `OnMouseDown` in `BaseChessPiece` clears existing highlights then calls the abstract `ShowLegalMoves()`.

3. **Move Calculation** — Each piece implements `ShowLegalMoves()` with its own rules. `IsInBounds` guards every candidate square.

4. **Highlight Logic**
   - Empty valid square → `ChessBoardPlacementHandler.Highlight()` (default color)
   - Enemy piece square → `ChessBoardPlacementHandler.HighlightEnemy()` (red tint via `SpriteRenderer.color`)
   - Friendly piece square → blocked, no highlight

5. **Sliding Pieces** — `SlidingPiece.HighlightInDirection(rowDir, colDir)` walks a ray one step at a time, stopping on a friendly (blocked) or highlighting red then stopping on an enemy.

---

## Design Patterns Used
- **Singleton** — `ChessBoardPlacementHandler`, `ChessBoardManager`
- **Template Method** — `BaseChessPiece.OnMouseDown` → `ShowLegalMoves()`
- **Inheritance / Polymorphism** — `SlidingPiece` shared ray-cast logic reused by `Rook`, `Bishop`, `Queen`
