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
        }

        public void GameLoop()
        {
            GetPlayers();

            turn = p1;

            while (true)
            {
                (bool, int, int, bool) mov = (false, 0, 0, false);

                ChoosePiece();

                ChooseDirection(mov);

                ChangeTurn();
            }
        }

        private void ChooseDirection((bool, int, int, bool) mov)
        {
            string c = "";

            while (mov.Item1 == false)
            {

                while (c != "1" && c != "2" && c != "3" && c != "4" &&
                    c != "5" && c != "6")
                {
                    Console.Clear();

                    ui.Render(gameGrid);
                    ui.ShowPossibleDirections(gameGrid[cPiece.Row,
                    cPiece.Col].PossibleMovements, cPiece);

                    c = Console.ReadLine();

                    mov = CheckMovement(c);

                    if (!mov.Item1)
                    {
                        Console.WriteLine("Unavailable movment to choose");
                        Console.ReadKey();
                    }

                }

            }

        }

        private (bool, int, int, bool) CheckMovement(string c)
        {
            bool value = false;
            bool eraseEnemy = false;
            int previousRow;
            int previousColumn;
            int newRow;
            int newColumn;

            if (c != "1" && c != "2" && c != "3" && c != "4" && c != "5" &&
                c != "6")
            {
                return (false, 0, 0, false);
            }
            else
            {
                Directions dir = (Directions)Convert.ToInt32(c);

                if (gameGrid[cPiece.Row, cPiece.Col].HasDirection(dir))
                {

                }
            }

        }

        private void ChoosePiece()
        {
            string c = "";

            while (cPiece is null)
            {
                while (c != "1" && c != "2" && c != "3" && c != "4" &&
                    c != "5" && c != "6")
                {
                    Console.Clear();
                    ui.Render(gameGrid);
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
    }
}