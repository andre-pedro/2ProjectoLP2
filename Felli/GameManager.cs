using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{    
    class GameManager
    {
        private UI ui;

        private Player p1;
        private Player p2;

        public GameManager(UI ui)
        {
            this.ui = ui;
        }

        public void GameLoop()
        {
            GetPlayers();
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
            if(choice.ToUpper() == "W")
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
