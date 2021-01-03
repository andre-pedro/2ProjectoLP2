namespace Felli
{
    /// <summary>
    /// Class that represents a game piece.
    /// </summary>
    internal class Piece
    {
        /// <summary>
        /// Gets or sets column where the piece is in.
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// Gets or Sets the row where the piece is in.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the previous Column.
        /// </summary>
        public int PreviousCol { get; set; }

        /// <summary>
        /// Gets or sets the previous row.
        /// </summary>
        public int PreviousRow { get; set; }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the volor of the piece.
        /// </summary>
        public PieceColor Color { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the piece
        /// is blocked or not.
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Construcotr of the class.
        /// </summary>
        /// <param name="row">Row of the Piece.</param>
        /// <param name="col">Collumn of the piece.</param>
        /// <param name="id">ID of the piece.</param>
        /// <param name="color">Color of the piece.</param>
        public Piece(int row, int col, int id, PieceColor color)
        {
            Row = row;
            Col = col;
            Id = id;
            Color = color;
        }

        /// <summary>
        /// Method to move the piece.
        /// </summary>
        /// <param name="dir">Direction to move.</param>
        public void MoveTo(Directions dir)
        {
            PreviousCol = Col;
            PreviousRow = Row;

            switch (dir)
            {
                case Directions.NE:
                    Col++;
                    Row--;
                    break;

                case Directions.N:
                    Row--;
                    break;

                case Directions.NO:
                    Col--;
                    Row--;
                    break;

                case Directions.E:
                    if (Row == 0 || Row == 4)
                        Col += 2;
                    else 
                        Col++;
                    break;

                case Directions.O:
                    if (Row == 0 || Row == 4)
                        Col -= 2;
                    else 
                        Col--;
                    break;

                case Directions.SE:
                    Col++;
                    Row++;
                    break;

                case Directions.S:
                    Row++;
                    break;

                case Directions.SO:
                    Col--;
                    Row++;
                    break;
            }
        }

        /// <summary>
        /// Method to reset the movment of the piece.
        /// </summary>
        public void ResetMovement()
        {
            Row = PreviousRow;
            Col = PreviousCol;
        }
    }
}
