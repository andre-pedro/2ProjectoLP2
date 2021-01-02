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
                    if (Row == 0 || Row == 4) Col += 2;
                    else Col++;
                    break;

                case Directions.O:
                    if (Row == 0 || Row == 4) Col -= 2;
                    else Col--;
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

        public void ResetMovement()
        {
            Row = PreviousRow;
            Col = PreviousCol;
        }
    }
}
