namespace Felli
{
    class Piece
    {
        public int Col { get; set; }

        public int Row { get; set; }

        public int PreviousCol { get; set; }

        public int PreviousRow { get; set; }

        public int Id { get; private set; }

        public PieceColor Color { get; private set; }

        public bool IsBlocked { get; set; }

        public Piece(int row, int col, int id, PieceColor color)
        {
            Row = row;
            Col = col;
            Id = id;
            Color = color;

        }

        public void ResetMovement()
        {
            Row = PreviousRow;
            Col = PreviousCol;
        }
    }
}
