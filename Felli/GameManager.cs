using System;

namespace Felli
{
    class GameManager
    {
        private readonly UI ui;

        private Player p1;
        private Player p2;
        private Player turn;

        private Piece cPiece;

        private Square[,] gameGrid;

        public GameManager(UI ui)
        {
            this.ui = ui;
            gameGrid = new Square[5, 5];

            SpawnPieces();
            PossibleMoves();
        }

        public void GameLoop()
        {
            GetPlayers();

            turn = p1;

            UpdateBlockedPieces();

            while (true)
            {      
                ChoosePiece();

                ChooseDirection();                

                ChangeTurn();
            }
        }

        private void ChooseDirection()
        {
            string c = "";

            (bool, int, int, bool) mov = (false, 0, 0, false);

            while (mov.Item1 == false)
            {

                while (c != "0" && c != "1" && c != "2" && c != "3" && c != "4"
                    && c != "5" && c != "6" && c != "7")
                    {
                    Console.Clear();

                    ui.Render(gameGrid);
                    ui.ShowPossibleDirections(gameGrid[cPiece.Row,
                    cPiece.Col].PossibleMovements, cPiece);

                    c = Console.ReadLine();

                    mov = CheckMovement(c);

                    if (!mov.Item1)
                    {
                        c = "";
                        Console.WriteLine("Unavailable movment to choose");
                        Console.ReadKey();
                    }
                    
                }
                
            }

            if (mov.Item4)
            {
                cPiece.MoveTo((Directions)Convert.ToInt32(c));

                gameGrid[cPiece.Row, cPiece.Col].Piece = null;

                cPiece.ResetMovement();
                UpdatePieces();
            }

            cPiece.Row = mov.Item2;
            cPiece.Col = mov.Item3;

            gameGrid[cPiece.Row, cPiece.Col].Piece = cPiece;
            gameGrid[cPiece.PreviousRow, cPiece.PreviousCol].Piece = null;

            UpdateBlockedPieces();
        }

        private (bool, int, int, bool) CheckMovement(string c)
        {
            bool value = false;
            bool eraseEnemy = false;
            int nRow = 0;
            int nColumn = 0;
            int pRow;
            int pColumn;

            if (c != "0" && c != "1" && c != "2" && c != "3"
            && c != "4" && c != "5" && c != "6" && c != "7")
            {
                return (false, 0, 0, false);
            }
            else
            {
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
                        if (cPiece.Color != targetSq.Piece.Color)
                        {
                            if (targetSq.HasDirection(dir))
                            {
                                cPiece.MoveTo(dir);
                                nRow = cPiece.Row;
                                nColumn = cPiece.Col;
                                eraseEnemy = true;

                                if (gameGrid[cPiece.Row, cPiece.Col].HasPiece())
                                {
                                    value = false;
                                }
                                else
                                {
                                    value = true;
                                }
                            }
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

        private void ChoosePiece()
        {
            string c = "";

            cPiece = null;

            while (cPiece is null)
            {
                while (c != "1" && c != "2" && c != "3" &&
                    c != "4" && c != "5" && c != "6")
                {
                    Console.Clear();
                    ui.Render(gameGrid);
                    Console.WriteLine($"{turn.Id} - {turn.Color} is playing.");
                    Console.WriteLine("Choose the piece you want" +
                        " to play from 1-6.");
                    Console.WriteLine();
                    c = Console.ReadLine();
                }

                cPiece = ChoosenPiece(c);

                if (cPiece is null)
                {
                    Console.WriteLine("Unavailable Piece to choose");
                    Console.ReadKey();
                }
            }
        }

        private Piece ChoosenPiece(string x)
        {
            Piece piece = null;

            foreach (Square square in gameGrid)
            {
                if (square.HasPiece())
                {
                    if (square.Piece.Id == Convert.ToInt32(x)
                    && square.Piece.Color == turn.Color
                    && square.Piece.IsBlocked == false)
                    {
                        piece = square.Piece;
                    }
                }
            }

            return piece;
        }

        private void ChangeTurn() => turn = turn == p1 ? p2 : p1;

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

                            if (!value) break;
                        }
                        gameGrid[x, y].Piece.IsBlocked =  value;
                    }
                }
            }
        }

        private bool CheckPos(Piece piece, Directions dir)
        {
            int row = piece.Row;
            int column = piece.Col;
            int previousRow = piece.PreviousRow;
            int previousColumn = piece.PreviousCol;
            bool value = false;

            piece.MoveTo(dir);

            if (gameGrid[piece.Row, piece.Col].HasPiece())
            {
                if (gameGrid[piece.Row, piece.Col].Piece.Color != piece.Color)
                { 
                    if (gameGrid[piece.Row, piece.Col].HasDirection(dir))
                    {
                        piece.MoveTo(dir);
                        if (gameGrid[piece.Row, piece.Col].HasPiece()) value = true;
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

        private void SpawnPieces()
        {
            for (int i = 0; i < gameGrid.GetLength(0); ++i)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    gameGrid[i, j] = new Square(Playable.playable);
                }
            }

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

            gameGrid[0, 1] = new Square(Playable.nonPlayable);
            gameGrid[0, 3] = new Square(Playable.nonPlayable);
            gameGrid[1, 0] = new Square(Playable.nonPlayable);
            gameGrid[1, 4] = new Square(Playable.nonPlayable);
            gameGrid[2, 0] = new Square(Playable.nonPlayable);
            gameGrid[2, 1] = new Square(Playable.nonPlayable);
            gameGrid[2, 3] = new Square(Playable.nonPlayable);
            gameGrid[2, 4] = new Square(Playable.nonPlayable);
            gameGrid[3, 0] = new Square(Playable.nonPlayable);
            gameGrid[3, 4] = new Square(Playable.nonPlayable);
            gameGrid[4, 1] = new Square(Playable.nonPlayable);
            gameGrid[4, 3] = new Square(Playable.nonPlayable);

        }


        private void GetPlayers()
        {
            string choice = "";

            while (choice.ToUpper() != "W" && choice.ToUpper() != "B")
            {
                Console.Clear();
                ui.ChooseMenu();
                choice = Console.ReadLine().ToUpper();
            }

            SetPlayers(choice);
        }

        private void SetPlayers(string choice)
        {
            if (choice.ToUpper() == "W")
            {
                p1 = new Player(PieceColor.W, 1);
                p2 = new Player(PieceColor.B, 2);
            }
            else
            {
                p1 = new Player(PieceColor.B, 1);
                p2 = new Player(PieceColor.W, 2);
            }

        }

        private void PossibleMoves()
        {
            gameGrid[0, 0].PossibleMovements
                    = new Directions[] {
                        Directions.E, Directions.SE };
            gameGrid[0, 2].PossibleMovements
                = new Directions[] {
                    Directions.S, Directions.E, Directions.O};
            gameGrid[0, 4].PossibleMovements
                = new Directions[] {
                    Directions.O, Directions.SO };
            gameGrid[1, 1].PossibleMovements
                = new Directions[] {
                    Directions.NO, Directions.E, Directions.SE};
            gameGrid[1, 2].PossibleMovements
                = new Directions[] {
                    Directions.N, Directions.S, Directions.E, Directions.O };
            gameGrid[1, 3].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.O, Directions.SO };
            gameGrid[2, 2].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.N, Directions.NO,
                    Directions.SO, Directions.S, Directions.SE };
            gameGrid[3, 1].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.E, Directions.SO };
            gameGrid[3, 2].PossibleMovements
                = new Directions[] {
                    Directions.N, Directions.S, Directions.E, Directions.O };
            gameGrid[3, 3].PossibleMovements
                = new Directions[] {
                    Directions.NO, Directions.O, Directions.SE };
            gameGrid[4, 0].PossibleMovements
                = new Directions[] {
                    Directions.NE, Directions.E };
            gameGrid[4, 2].PossibleMovements
                = new Directions[] {
                    Directions.O, Directions.E, Directions.N };
            gameGrid[4, 4].PossibleMovements
                = new Directions[] {
                    Directions.O, Directions.NO };
        }
    }
}