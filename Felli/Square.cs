namespace Felli
{
    /// <summary>
    /// Class Square.
    /// </summary>
    internal class Square
    {
        /// <summary>
        /// Gets or sets the possible movments the player can do.
        /// </summary>
        public Directions[] PossibleMovements { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Playable Type { get; }

        /// <summary>
        /// Gets or sets a piece.
        /// </summary>
        public Piece Piece { get; set; }

        /// <summary>
        /// Constructor of the class Square.
        /// </summary>
        /// <param name="type">To see if its playable or not.</param>
        public Square(Playable type) => Type = type;

        /// <summary>
        /// Sees if it has a piece.
        /// </summary>
        /// <returns>True if theres piece.</returns>
        public bool HasPiece() => !(Piece is null);

        /// <summary>
        /// Sees if a direction is available.
        /// </summary>
        /// <param name="dir">direction to check.</param>
        /// <returns>True if direction is available.</returns>
        public bool HasDirection(Directions dir)
        {
            foreach (Directions direction in PossibleMovements)
            {
                if (direction == dir)
                {
                    return true;
                }
            }

            return false;
        }
    }
}