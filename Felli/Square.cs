﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    class Square
    {
        public Directions[] PossibleMovements { get; set; }

        public Playable Type { get; private set; }

        public Piece Piece { get; set; }

        public Square(Playable type) => Type = type;

        public bool HasPiece()
        {
            return (Piece is null) ? false : true;
        }

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