using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    class UI
    {
        public void MainMenu()
        {
            Console.WriteLine(" Made by André Pedro and Diogo Maia!");
            Console.WriteLine();
            Console.WriteLine(" Rules of the Game:");
            Console.WriteLine();
            Console.WriteLine(" Movement:");
            Console.WriteLine("   The players decide which colors to play with and who " +
                              "plays first.");
            Console.WriteLine("   Then the first player chooses which piece he wants to play first. " +
                "\n   The pieces can be moved in the following ways: " +
                "\n          The player can move in all possible directions and then " +
                             "choose the direction on the numpad." +
                "\n          *The can jump over an adjacent opponent's piece, eliminating " +
                             "that piece and landing at a free spot on the board." +
                "\n          *Only one piece can move per turn.");
            Console.WriteLine("   Afterwards, it's the second player's turn, following the same rules.");
            Console.WriteLine();
            Console.WriteLine(" Objectives:");
            Console.WriteLine("   The game ends when a player has captured or immobilized all of the " +
                              "opponent's pieces.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Press any key to continue!");
            Console.ReadKey();
            Console.Clear();
        }

        public void ChooseMenu() =>
            Console.WriteLine("First Player is White (W) or Black (B)?");

        public void Render(Square[,] gameGrid)
        {
            for (int x = 0; x < gameGrid.GetLength(0); x++)
            {
                //Iterate through the grid  
                for (int y = 0; y < gameGrid.GetLength(1); y++)
                {
                    Console.Write(GetChar(gameGrid[x, y]) + "\t");
                }

                Console.WriteLine("\n\n");
            }
        }

        private char GetChar(Square square)
        {
            char c = ' ';
            if (square.Piece is null)
            {
                c = square.Type == Playable.playable ? '\u00B7' : ' ';
            }
            else
            {
                Piece piece = square.Piece;

                //c = (char)piece.Id; //I dont know why it doesnt work had to
                //put it it a switch
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

        public void ShowPossibleDirections(Directions[] possibleMoves, Piece p)
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
    }
}