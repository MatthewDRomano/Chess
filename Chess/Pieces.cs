using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess
{
    class Pieces 
    {
        private Type type;
        private Point position;
        private bool color;
        private int moves = 0;
        private int lastMove;

        public Pieces(Type theType, bool theColor, Point pos)
        {
            type = theType;
            position = pos;
            color = theColor;
        }
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }        
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }
        public bool Color
        {
            get { return color; }
        }
        public int Moves
        {
            get { return moves; }
            set { moves = value; }
        }
        public int LastMove
        {
            get { return lastMove; }
            set { lastMove = value; }
        }
    }
}
