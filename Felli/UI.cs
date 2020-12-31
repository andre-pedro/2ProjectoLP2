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
    }
}
