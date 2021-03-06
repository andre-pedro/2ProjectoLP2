﻿using System;
using System.Threading;

namespace Felli
{
    /// <summary>
    /// Class Gamanager.
    /// </summary>
    internal class GameManager
    {
        /// <summary>
        /// Represents the game board.
        /// </summary>
        private readonly Square[,] gameGrid;

        /// <summary>
        /// Represents player 1.
        /// </summary>
        private Player p1;

        /// <summary>
        /// Represents player 2.
        /// </summary>
        private Player p2;

        /// <summary>
        /// Represents the current player playing.
        /// </summary>
        private Player turn;

        /// <summary>
        /// Represents current Piece.
        /// </summary>
        private Piece cPiece;

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public GameManager()
        {
            // Instanciates the grid
            gameGrid = new Square[5, 5];

            // Spawns the pieces
            SpawnPieces();

            // Sets all possible movements
            PossibleMoves();
        }

        /// <summary>
        /// Game loop method.
        /// </summary>
        public void GameLoop()
        {
            // Get the players and respective colors
            GetPlayers();

            // Make player 1 play
            turn = p1;

            // Update the Blocked Pieces
            UpdateBlockedPieces();

            // Play a turn
            while (true)
            {
                // Player chooses a piece to play
                ChoosePiece();

                // Chooses the direction to move the piece and moves the piece
                MoveDirection();

                // If a player wins, clear the board and show the winner.
                if (Win())
                {
                    // Clears the console
                    Console.Clear();

                    // Renders the game board
                    UI.Render(gameGrid);

                    // Shows who winned the match
                    UI.Win(turn);

                    // Breaks the game loop
                    break;
                }

                // Changes the player who is playing
                ChangeTurn();

                // Thread sleep
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Chooses the direction to where the piece should go and move.
        /// </summary>
        private void MoveDirection()
        {
            // Empry string
            string c = string.Empty;

            // Movement Tuple
            (bool, int, int, bool) mov = (false, 0, 0, false);

            while (!mov.Item1)
            {
                // Makes the player choose a direction between 0 - 7
                while (c != "0" && c != "1" && c != "2" && c != "3" && c != "4"
                    && c != "5" && c != "6" && c != "7")
                {
                    // Clear the window
                    Console.Clear();

                    // Render the grid
                    UI.Render(gameGrid);

                    // Show the possible directions
                    UI.ShowPossibleDirections(
                        gameGrid[cPiece.Row, cPiece.Col].PossibleMovements,
                        cPiece);

                    // Gets the player choice
                    c = Console.ReadLine();

                    // Checks if piece can be moved in that direction
                    mov = CheckMovement(c);

                    // If an unavaliable movement is chosen
                    // display an appropriate message
                    if (!mov.Item1)
                    {
                        // Clears the player input
                        c = string.Empty;

                        Console.WriteLine("Unavailable movement to choose");
                        Console.ReadKey();
                    }
                }
            }

            if (mov.Item4)
            {
                // Moves the piece
                cPiece.MoveTo((Directions)Convert.ToInt32(c));

                // Makes the place where the piece was null
                gameGrid[cPiece.Row, cPiece.Col].Piece = null;

                // Resets the movement
                cPiece.ResetMovement();

                // Updates the number of pieces each player has
                UpdatePieces();
            }

            cPiece.Row = mov.Item2;
            cPiece.Col = mov.Item3;

            gameGrid[cPiece.Row, cPiece.Col].Piece = cPiece;
            gameGrid[cPiece.PreviousRow, cPiece.PreviousCol].Piece = null;

            // Updates the blocked pieces
            UpdateBlockedPieces();
        }

        /// <summary>
        /// Method used to see if the piece can move in x direction.
        /// </summary>
        /// <param name="c">Choice of the player.</param>
        /// <returns>Values used to move the piece.</returns>
        private (bool, int, int, bool) CheckMovement(string c)
        {
            bool value = false;
            bool eraseEnemy = false;
            int nRow = 0;
            int nColumn = 0;
            int pRow;
            int pColumn;

            // Check if the player chooses an unavaliable direction
            if (c != "0" && c != "1" && c != "2" && c != "3"
            && c != "4" && c != "5" && c != "6" && c != "7")
            {
                // returns becaus input wasnt between 0 - 7
                return (false, 0, 0, false);
            }
            else
            {
                // Gets the direction the player wanted
                Directions dir = (Directions)Convert.ToInt32(c);

                if (gameGrid[cPiece.Row, cPiece.Col].HasDirection(dir))
                {
                    cPiece.MoveTo(dir);

                    Square targetSq = gameGrid[cPiece.Row, cPiece.Col];
                    pRow = cPiece.PreviousRow;
                    pColumn = cPiece.PreviousCol;
                    nRow = cPiece.Row;
                    nColumn = cPiece.Col;

                    if (!targetSq.HasPiece())
                    {
                        value = true;
                    }
                    else
                    {
                        if (cPiece.Color != targetSq.Piece.Color &&
                            targetSq.HasDirection(dir))
                        {
                            cPiece.MoveTo(dir);
                            nRow = cPiece.Row;
                            nColumn = cPiece.Col;
                            eraseEnemy = true;

                            value =
                                !gameGrid[cPiece.Row, cPiece.Col].HasPiece();
                        }
                    }

                    cPiece.Row = pRow;
                    cPiece.Col = pColumn;
                    cPiece.PreviousRow = pRow;
                    cPiece.PreviousCol = pColumn;
                }

                return (value, nRow, nColumn, eraseEnemy);
            }
        }

        /// <summary>
        /// Method used for the player to choose a piece.
        /// </summary>
        private void ChoosePiece()
        {
            string c = string.Empty;

            cPiece = null;

            // Ask the player which piece they wish to choose.
            while (cPiece is null)
            {
                while (c != "1" && c != "2" && c != "3" &&
                    c != "4" && c != "5              " && c != "6")
                {
                    Console.Clear();
                    UI.Render(gameGrid);
                    Console.WriteLine($"{turn.Id} - {turn.Color} is playing.");
                    Console.WriteLine("Choose the piece you want" +
                        " to play from 1-6.");
                    Console.WriteLine();
                    c = Console.ReadLine();
                }

                cPiece = ChoosenPiece(c);

                // If the chosen piece isn't allowed to be chosen, display 
                // an appropriate message.
                if (cPiece is null)
                {
                    Console.WriteLine("Unavailable Piece to choose");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Gets the piece.
        /// </summary>
        /// <param name="x">choice of the player.</param>
        /// <returns>Piece the player chooses.</returns>
        private Piece ChoosenPiece(string x)
        {
            Piece piece = null;

            // Gets the piece (with color and ID) the player chooses.
            foreach (Square square in gameGrid)
            {
                if (square.HasPiece() && square.Piece.Id == Convert.ToInt32(x)
                    && square.Piece.Color == turn.Color
                    && !square.Piece.IsBlocked)
                {
                    piece = square.Piece;
                }
            }

            return piece;
        }

        /// <summary>
        /// Changes the current player playing.
        /// </summary>
        private void ChangeTurn() => turn = turn == p1 ? p2 : p1;

        /// <summary>
        /// Updates the number of pieces each player has.
        /// </summary>
        private void UpdatePieces()
        {
            if (turn == p1)
            {
                p2.PieceCount--;
            }
            else
            {
                p1.PieceCount--;
            }
        }

        /// <summary>
        /// Updates the blocked pieces of the game.
        /// </summary>
        private void UpdateBlockedPieces()
        {
            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                for (int y = 0; y < gameGrid.GetLength(1); y++)
                {
                    if (gameGrid[x, y].HasPiece())
                    {
                        bool value = false;

                        foreach (Directions d in
                            gameGrid[x, y].PossibleMovements)
                        {
                            value = CheckPos(gameGrid[x, y].Piece, d);

                            if (!value)
                                break;
                        }

                        gameGrid[x, y].Piece.IsBlocked = value;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a piece can move in a direction.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <param name="dir">Direction to move.</param>
        /// <returns>True if piece can be moved in that direction.</returns>
        private bool CheckPos(Piece piece, Directions dir)
        {
            int row = piece.Row;
            int column = piece.Col;
            int previousRow = piece.PreviousRow;
            int previousColumn = piece.PreviousCol;
            bool value = false;

            piece.MoveTo(dir);

            // Check if a position on the grid doesn't have another piece and
            // is able to be moved there.
            if (gameGrid[piece.Row, piece.Col].HasPiece())
            {
                if (gameGrid[piece.Row, piece.Col].Piece.Color != piece.Color)
                {
                    if (gameGrid[piece.Row, piece.Col].HasDirection(dir))
                    {
                        piece.MoveTo(dir);
                        if (gameGrid[piece.Row, piece.Col].HasPiece())
                            value = true;
                    }
                }
                else
                {
                    value = true;
                }
            }

            piece.Row = row;
            piece.Col = column;
            piece.PreviousRow = previousRow;
            piece.PreviousCol = previousColumn;

            return value;
        }

        /// <summary>
        /// Populates the board with pieces.
        /// </summary>
        private void SpawnPieces()
        {
            // Draw the grid
            for (int i = 0; i < gameGrid.GetLength(0); ++i)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    gameGrid[i, j] = new Square(Playable.Playable);
                }
            }

            // Spawn each piece with its specific color and position
            // on the grid
            gameGrid[0, 0].Piece = new Piece(0, 0, 1, PieceColor.B);
            gameGrid[0, 2].Piece = new Piece(0, 2, 2, PieceColor.B);
            gameGrid[0, 4].Piece = new Piece(0, 4, 3, PieceColor.B);
            gameGrid[1, 1].Piece = new Piece(1, 1, 4, PieceColor.B);
            gameGrid[1, 2].Piece = new Piece(1, 2, 5, PieceColor.B);
            gameGrid[1, 3].Piece = new Piece(1, 3, 6, PieceColor.B);
            gameGrid[3, 1].Piece = new Piece(3, 1, 4, PieceColor.W);
            gameGrid[3, 2].Piece = new Piece(3, 2, 5, PieceColor.W);
            gameGrid[3, 3].Piece = new Piece(3, 3, 6, PieceColor.W);
            gameGrid[4, 0].Piece = new Piece(4, 0, 1, PieceColor.W);
            gameGrid[4, 2].Piece = new Piece(4, 2, 2, PieceColor.W);
            gameGrid[4, 4].Piece = new Piece(4, 4, 3, PieceColor.W);

            // Spawn each non-playable square on a specific position
            // on the grid
            gameGrid[0, 1] = new Square(Playable.NonPlayable);
            gameGrid[0, 3] = new Square(Playable.NonPlayable);
            gameGrid[1, 0] = new Square(Playable.NonPlayable);
            gameGrid[1, 4] = new Square(Playable.NonPlayable);
            gameGrid[2, 0] = new Square(Playable.NonPlayable);
            gameGrid[2, 1] = new Square(Playable.NonPlayable);
            gameGrid[2, 3] = new Square(Playable.NonPlayable);
            gameGrid[2, 4] = new Square(Playable.NonPlayable);
            gameGrid[3, 0] = new Square(Playable.NonPlayable);
            gameGrid[3, 4] = new Square(Playable.NonPlayable);
            gameGrid[4, 1] = new Square(Playable.NonPlayable);
            gameGrid[4, 3] = new Square(Playable.NonPlayable);
        }

        /// <summary>
        /// Gets the menu to let the players pick the color.
        /// </summary>
        private void GetPlayers()
        {
            string choice = string.Empty;

            while (!string.Equals(
                choice, "W", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(
                    choice, "B", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                UI.ChooseMenu();
                choice = Console.ReadLine().ToUpper();
            }

            SetPlayers(choice);
        }

        /// <summary>
        /// Sets the players with the respective colors.
        /// </summary>
        /// <param name="choice">Color of the player.</param>
        private void SetPlayers(string choice)
        {
            // If the player chooses W, set its color to white and the
            // opponent's color to black.
            if (string.Equals(choice, "W", StringComparison.OrdinalIgnoreCase))
            {
                p1 = new Player(PieceColor.W, 1);
                p2 = new Player(PieceColor.B, 2);
            }
            else
            {
                // If the player chooses B, set its color to black
                // and the opponent's color to white.
                p1 = new Player(PieceColor.B, 1);
                p2 = new Player(PieceColor.W, 2);
            }
        }

        /// <summary>
        /// Makes the possible moves from each board position.
        /// </summary>
        private void PossibleMoves()
        {
            gameGrid[0, 0].PossibleMovements
                    = new Directions[] {
                        Directions.E, Directions.SE, };
            gameGrid[0, 2].PossibleMovements
                = new Directions[] {
                    Directions.S, Directions.E, Directions.O, };
            gameGrid[0, 4].PossibleMovements
                = new Directions[] {
                    Directions.O, Directions.SO, };
            gameGrid[1, 1].PossibleMovements
                = new Directions[] {
                    Directions.NO, Directions.E, Directions.SE, };
            gameGrid[1, 2].PossibleMovements
                = new Directions[] {
                    Directions.N, Directions.S, Directions.E, Directions.O, };
            gameGrid[1, 3].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.O, Directions.SO, };
            gameGrid[2, 2].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.N, Directions.NO,
                    Directions.SO, Directions.S, Directions.SE, };
            gameGrid[3, 1].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.E, Directions.SO, };
            gameGrid[3, 2].PossibleMovements
                = new Directions[] {
                    Directions.N, Directions.S, Directions.E, Directions.O, };
            gameGrid[3, 3].PossibleMovements
                = new Directions[] {
                    Directions.NO, Directions.O, Directions.SE, };
            gameGrid[4, 0].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.E, };
            gameGrid[4, 2].PossibleMovements
                = new Directions[] {
                    Directions.O, Directions.E, Directions.N, };
            gameGrid[4, 4].PossibleMovements
                = new Directions[] {
                    Directions.O, Directions.NO, };
        }

        /// <summary>
        /// Sees if a player has win.
        /// </summary>
        /// <returns>True if a player won.</returns>
        private bool Win()
        {
            // If the other player has all their pieces blocked, they lose
            // and the current player wins.
            if (p1.Color == turn.Color)
            {
                if (p2.PieceCount == 0 || HasAllPiecesBlocked(p2.Color))
                {
                    return true;
                }
            }
            else
            {
                if (p1.PieceCount == 0 || HasAllPiecesBlocked(p1.Color))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a player has all his pieces blocked.
        /// </summary>
        /// <param name="color">Color of the player.</param>
        /// <returns>True if all pieces are blocked.</returns>
        private bool HasAllPiecesBlocked(PieceColor color)
        {
            bool value = false;

            // Check if the piece of the current player's color is
            // blocked by all sides
            foreach (Square square in gameGrid)
            {
                if (square.HasPiece() && square.Piece.Color == color)
                {
                    if (square.Piece.IsBlocked)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                        break;
                    }
                }
            }

            return value;
        }
    }
}