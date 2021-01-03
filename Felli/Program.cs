using System.Threading;

namespace Felli
{
    /// <summary>
    /// Class program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Main method of the program.
        /// </summary>
        private static void Main()
        {
            GameManager gm = new GameManager();
            Thread game = new Thread(new ThreadStart(gm.GameLoop));

            UI.MainMenu();
            game.Start();
        }
    }
}
