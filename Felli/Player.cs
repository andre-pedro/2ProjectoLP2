namespace Felli
{
    class Player
    {
        public PieceColor Color { get; private set; }

        public int Id { get; private set; }

        public int PieceCount { get; set; }

        public Player(PieceColor color, int id)
        {
            Color = color;
            Id = id;
            PieceCount = 6;
        }
    }
}
