using System;
using System.Threading;

namespace Felli
{
    class Program
    {
        static void Main(string[] args)
        {            
            UI ui = new UI();
            GameManager gm = new GameManager(ui);

            ui.MainMenu();
            gm.GameLoop();
        }
    }
}
