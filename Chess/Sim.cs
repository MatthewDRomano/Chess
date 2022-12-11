using System;
using System.Drawing;
using System.Linq;

namespace Chess
{
    class Sim
    {
        Pieces[] simArray;
        Point oldPosOfMovedPiece;
        Point newPos, oldPos;
        int gameMoves;
        public Sim (Pieces[] array, Point oldPos, int moves)
        {
            simArray = array;
            oldPosOfMovedPiece = oldPos;
            gameMoves = moves;
        }
        private int findPiece(int x = 99, int y = 99) // if no ints are sent for a position then it defaults to returning position of moved piece (oldPos)
        {
            int q = 99;
            for (int i = 0; i < simArray.Length; i++)
            {
                if (x == 99 && y == 99 && simArray[i].Position == oldPosOfMovedPiece) q = i;
                else if (simArray[i].Position == new Point(x, y) && i != 99) q = i;
            }
            return q;
        }
        public bool underAttack(Point pos, Pieces[] simArray, Pieces target = null, Pieces movedPiece = null)
        {
            for (int i = 0; i < simArray.Length; i++)// finds and removes a piece that hasnt been deleted but was 'taken' by another piece trying to get its own king out of check
            {
                if (movedPiece != null && simArray[i].Position == movedPiece.Position && simArray[i].Color != movedPiece.Color) simArray = removePiece(simArray, i);
            }
           
            bool check = false;
            //promote = true; // just so moves cant be done while checking for check

            foreach (Pieces piece in simArray)
            {
                if (piece.Color != target.Color)
                {
                    //if (check && piece.Position == allPieces[spotInPieceArray].Position) return check = false;
                    newPos = target.Position;
                    oldPos = piece.Position;
                    
                    if (Legal(piece)) check = true;
                }
            }
            //promote = false;
            return check;
        }
        private Pieces[] removePiece(Pieces[] array, int removeAt) // poss removable
        {
            array = array.Where((source, index) => index != removeAt).ToArray(); //INSTEA OF CALLING THIS METHOD            
            return array;
        }
        private  bool Legal(Pieces piece)
        {
            bool legal = false;
            int deltaX = Math.Abs(newPos.X - oldPos.X), deltaY = Math.Abs(newPos.Y - oldPos.Y);

            switch (piece.Type)
            {
                case Type.Pawn:                 
                    int decider = 1; if (!piece.Color) decider = -1;
                    int xmove = 1; if (newPos.X - oldPos.X == -1) xmove = -1;

                    if (oldPos.Y + decider == newPos.Y && deltaX == 0 && findPiece(newPos.X, newPos.Y) == 99) legal = true; // 1 move
                    if (piece.Moves == 0 && newPos.Y == 3.5 - (double)decider / 2 && deltaX == 0 && deltaY == 2 && findPiece(newPos.X, newPos.Y) == 99 && findPiece(newPos.X, newPos.Y - decider) == 99) { piece.LastMove = gameMoves; legal = true; } // 2 move
                    if (oldPos.Y + decider == newPos.Y && deltaX == 1 && findPiece(newPos.X, newPos.Y) != 99 && simArray[findPiece(newPos.X, newPos.Y)].Color != piece.Color) legal = true; //take
                    if (oldPos.Y + decider == newPos.Y && deltaX == 1 && findPiece(oldPos.X + xmove, oldPos.Y) != 99 && simArray[findPiece(oldPos.X + xmove, oldPos.Y)].Color != piece.Color && simArray[findPiece(oldPos.X + xmove, oldPos.Y)].Moves == 1 && (gameMoves - simArray[findPiece(oldPos.X + xmove, oldPos.Y)].LastMove < 2))
                    {
                        legal = true;
                    }
                    //if (newPos.Y == 3.5 + decider * 3 + (double)decider / 2) promote = true;
                    break;
                case Type.Knight:
                    if (Math.Abs(newPos.X - oldPos.X) == 2 && Math.Abs(newPos.Y - oldPos.Y) == 1) legal = true;
                    if (Math.Abs(newPos.X - oldPos.X) == 1 && Math.Abs(newPos.Y - oldPos.Y) == 2) legal = true;
                    break;
                case Type.Rook:
                case Type.Bishop:
                case Type.Queen:
                    int x = newPos.X, y = newPos.Y;
                    if ((deltaX == deltaY && piece.Type != Type.Rook) || (deltaX == 0 || deltaY == 0) && (piece.Type != Type.Bishop))
                    {
                        legal = true;
                        while (!oldPos.Equals(new Point(x, y)))
                        {
                            if (newPos.X < oldPos.X) x++;
                            else if (newPos.X > oldPos.X) x--;
                            if (newPos.Y > oldPos.Y) y--;
                            else if (newPos.Y < oldPos.Y) y++;
                            if (findPiece(x, y) != 99 && new Point(x, y) != oldPos) { legal = false; break; }
                        }
                    }
                    break;
                case Type.King:
                    if (Math.Abs(newPos.X - oldPos.X) == 1 && Math.Abs(newPos.Y - oldPos.Y) == 1) legal = true;
                    if (Math.Abs(newPos.X - oldPos.X) == 0 && Math.Abs(newPos.Y - oldPos.Y) == 1) legal = true;
                    if (Math.Abs(newPos.X - oldPos.X) == 1 && Math.Abs(newPos.Y - oldPos.Y) == 0) legal = true;
                    break;
            }
            return legal;
    }
    }
}
