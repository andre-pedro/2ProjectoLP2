using System;

namespace Felli
{
    /// <summary>
    /// Class UI.
    /// </summary>
    internal static class UI
    {
        /// <summary>
        /// Shows the main menu of the game.
        /// </summary>
        public static void MainMenu()
        {
            Console.WriteLine("Made by André Pedro and Diogo Maia!");
            Console.WriteLine();
            Console.WriteLine("Rules of the Game:");
            Console.WriteLine();
            Console.WriteLine("Movement:");
            Console.WriteLine("     The players decide which colors to play" +
                " with and who " +
                              "plays first.");
            Console.WriteLine("     Then the first player chooses which" +
                " piece he wants to play first. " +
                "\n     The pieces can be moved in the following ways: " +
                "\n          The player can move in all possible directions" +
                " and then " +
                             "choose the direction on the numpad." +
                "\n          The can jump over an adjacent opponent's piece," +
                " eliminating " +
                             "that piece and landing at a free spot on the" +
                             " board." +
                "\n          Only one piece can move per turn.");
            Console.WriteLine("   Afterwards, it's the second player's turn," +
                " following the same rules.");
            Console.WriteLine();
            Console.WriteLine("Objectives:");
            Console.WriteLine("     The game ends when a player has captured" +
                " or immobilized all of the " +
                              "opponent's pieces.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Menu where the player chooses their color.
        /// </summary>
        public static void ChooseMenu() =>
            Console.WriteLine("First Player is White (W) or Black (B)?");

        /// <summary>
        /// Draws the playable pieces.
        /// </summary>
        /// <param name="square">A position on the board.</param>
        /// <returns>Char to write.</returns>
        private static char GetChar(Square square)
        {
            char c = ' ';
            if (square.Piece is null)
            {
                c = square.Type == Playable.Playable ? '\u00B7' : ' ';
            }
            else
            {
                Piece piece = square.Piece;

                // c = (char)piece.Id; //I dont know why it doesnt work had to
                // put it it a switch
                switch (piece.Id)
                {
                    case 1:
                        c = '1';
                        break;

                    case 2:
                        c = '2';
                        break;

                    case 3:
                        c = '3';
                        break;

                    case 4:
                        c = '4';
                        break;

                    case 5:
                        c = '5';
                        break;

                    case 6:
                        c = '6';
                        break;
                }
            }

            return c;
        }

        /// <summary>
        /// Renders the game board.
        /// </summary>
        /// <param name="gameGrid">Game board.</param>
        public static void Render(Square[,] gameGrid)
        {
            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                // Iterate through the grid.
                for (int y = 0; y < gameGrid.GetLength(1); y++)
                {
                    Console.Write(GetChar(gameGrid[x, y]) + "\t");
                }

                Console.WriteLine("\n\n");
            }

            DrawBoard();
            DrawMov();
        }

        /// <summary>
        /// Draws the respective directions and numbers to use them.
        /// </summary>
        private static void DrawMov()
        {
            WriteIn("Select the desired direction with your numpad!", 2, 50);
            WriteIn("  5(NO)   0(N)   4(NE)", 4, 60);
            WriteIn(@"    \      |      /", 5, 60);
            WriteIn(@"     \     |     /", 6, 60);
            WriteIn(@"      \    |    /", 7, 60);
            WriteIn("3(O)-------------- 2(E)", 8, 60);
            WriteIn(@"      /    |    \", 9, 60);
            WriteIn(@"     /     |     \", 10, 60);
            WriteIn(@"    /      |      \", 11, 60);
            WriteIn("  7(SO)   1(S)   6(SE)", 12, 60);
        }

        /// <summary>
        /// Draws the lines of the board.
        /// </summary>
        private static void DrawBoard()
        {
            WriteIn(" ------------- ", 0, 1);
            WriteIn(" ------------- ", 0, 17);

            WriteIn("-            |             -", 1, 3);
            WriteIn("-         |          -", 2, 6);

            WriteIn("-----", 3, 10);
            WriteIn("-----", 3, 18);

            WriteIn("-     |     -", 4, 10);
            WriteIn("-   |   -", 5, 12);

            WriteIn("-   |   -", 7, 12);
            WriteIn("-     |     -", 8, 10);

            WriteIn("-----", 9, 18);
            WriteIn("-----", 9, 10);

            WriteIn("-         |          -", 10, 6);
            WriteIn("-            |             -", 11, 3);

            WriteIn(" ------------- ", 12, 17);
            WriteIn(" ------------- ", 12, 1);
        }        

        /// <summary>
        /// Writes the winner of he game.
        /// </summary>
        /// <param name="winner">winner of the game.</param>
        public static void Win(Player winner)
        {
            Console.Clear();
            Console.Write($"Player {winner.Id} wins!!!!");
        }

        /// <summary>
        /// Show the direction of where a piece can be moved.
        /// </summary>
        /// <param name="possibleMoves">Possible moves.</param>
        /// <param name="p">Piece to check the possible directions.</param>
        public static void ShowPossibleDirections(
            Directions[] possibleMoves, Piece p)
        {
            Console.WriteLine();
            Console.WriteLine("Possible movements:");
            foreach (Directions direction in possibleMoves)
            {
                Console.Write(direction + ", ");
            }

            Console.WriteLine();
            Console.WriteLine($"Selected Piece: {p.Id}");
            Console.WriteLine();
            Console.Write("Choose one of the options -> ");
        }

        /// <summary>
        /// Writes in a certain part of the console.
        /// </summary>
        /// <param name="s">What to write.</param>
        /// <param name="x">X pos of the console.</param>
        /// <param name="y">Y pos of the console.</param>
        public static void WriteIn(string s, int x, int y)
        {
            int oRow = Console.CursorTop;
            int oCol = Console.CursorLeft;

            Console.SetCursorPosition(y, x);
            Console.Write(s);
            Console.SetCursorPosition(oCol, oRow);
        }
    }
}