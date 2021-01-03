namespace Felli
{
    /// <summary>
    /// Class tha contains a player.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Gets the color of the player.
        /// </summary>
        public PieceColor Color { get; }

        /// <summary>
        /// Gets the ID of the player.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the number of pieces the player has.
        /// </summary>
        public int PieceCount { get; set; }

        /// <summary>
        /// Constructor of the class player.
        /// </summary>
        /// <param name="color">Color of the player.</param>
        /// <param name="id">ID of the player.</param>
        public Player(PieceColor color, int id)
        {
            Color = color;
            Id = id;
            PieceCount = 6;
        }
    }
}
