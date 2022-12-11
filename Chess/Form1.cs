using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public enum Type
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        King,
        Queen
    }
    public partial class Form1 : Form 
    {//make pBoxClick better// all methods of drawing (stalemate...etc) // winning screen// clean up castle
        PictureBox[] boardArray;
        Pieces[] allPieces;
        Point oldPos;
        Point newPos;
        bool canMove = false;        //change name
        bool promote, haltMove = false;
        bool check = false;
        int spotInPieceArray;
        int gameMoves = 0;
        int prevOld, prevNew, prevCheck;
        Color ogColor;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            boardArray = initializeBoardArray();
            allPieces = new Pieces[32];
            if (true)
            {
                allPieces[0] = new Pieces(Type.Pawn, true, new Point(0, 1));
                allPieces[1] = new Pieces(Type.Pawn, true, new Point(1, 1));
                allPieces[2] = new Pieces(Type.Pawn, true, new Point(2, 1));
                allPieces[3] = new Pieces(Type.Pawn, true, new Point(3, 1));
                allPieces[4] = new Pieces(Type.Pawn, true, new Point(4, 1));
                allPieces[5] = new Pieces(Type.Pawn, true, new Point(5, 1));
                allPieces[6] = new Pieces(Type.Pawn, true, new Point(6, 1));
                allPieces[7] = new Pieces(Type.Pawn, true, new Point(7, 1));
                allPieces[8] = new Pieces(Type.Pawn, false, new Point(0, 6));
                allPieces[9] = new Pieces(Type.Pawn, false, new Point(1, 6));
                allPieces[10] = new Pieces(Type.Pawn, false, new Point(2, 6));
                allPieces[11] = new Pieces(Type.Pawn, false, new Point(3, 6));
                allPieces[12] = new Pieces(Type.Pawn, false, new Point(4, 6));
                allPieces[13] = new Pieces(Type.Pawn, false, new Point(5, 6));
                allPieces[14] = new Pieces(Type.Pawn, false, new Point(6, 6));
                allPieces[15] = new Pieces(Type.Pawn, false, new Point(7, 6));

                allPieces[16] = new Pieces(Type.Rook, true, new Point(0, 0));
                allPieces[17] = new Pieces(Type.Knight, true, new Point(1, 0));
                allPieces[18] = new Pieces(Type.Bishop, true, new Point(2, 0));
                allPieces[19] = new Pieces(Type.Queen, true, new Point(3, 0));
                allPieces[20] = new Pieces(Type.King, true, new Point(4, 0));
                allPieces[21] = new Pieces(Type.Bishop, true, new Point(5, 0));
                allPieces[22] = new Pieces(Type.Knight, true, new Point(6, 0));
                allPieces[23] = new Pieces(Type.Rook, true, new Point(7, 0));

                allPieces[24] = new Pieces(Type.Rook, false, new Point(0, 7));
                allPieces[25] = new Pieces(Type.Knight, false, new Point(1, 7));
                allPieces[26] = new Pieces(Type.Bishop, false, new Point(2, 7));
                allPieces[27] = new Pieces(Type.Queen, false, new Point(3, 7));
                allPieces[28] = new Pieces(Type.King, false, new Point(4, 7));
                allPieces[29] = new Pieces(Type.Bishop, false, new Point(5, 7));
                allPieces[30] = new Pieces(Type.Knight, false, new Point(6, 7));
                allPieces[31] = new Pieces(Type.Rook, false, new Point(7, 7));
            }//fills allPieces array full of all pieces. Just use if to minimize
        }
        private PictureBox[] initializeBoardArray()
        {
            PictureBox[] boardArray = new PictureBox[64];
            //label17.Text = " ";
            foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
            {
                if (pb.Tag != null)
                {
                    //label17.Text += Convert.ToInt32(pb.Tag.ToString()[1] - '0') * 8 + Convert.ToInt32(pb.Tag.ToString()[0] - '0') + " ";
                    boardArray[Convert.ToInt32(pb.Tag.ToString()[1] - '0') * 8 + Convert.ToInt32(pb.Tag.ToString()[0] - '0')] = pb; // the command after if statement is wrong }
                }
            }
            //boardArray[0] = A8;
            //boardArray[1] = B8;
            //boardArray[2] = C8;
            //boardArray[3] = D8;
            //boardArray[4] = E8;
            //boardArray[5] = F8;
            //boardArray[6] = G8;
            //boardArray[7] = H8;
            //boardArray[8] = A7;
            //boardArray[9] = B7;
            //boardArray[10] = C7;
            //boardArray[11] = D7;
            //boardArray[12] = E7;
            //boardArray[13] = F7;
            //boardArray[14] = G7;
            //boardArray[15] = H7;
            //boardArray[16] = A6;
            //boardArray[17] = B6;
            //boardArray[18] = C6;
            //boardArray[19] = D6;
            //boardArray[20] = E6;
            //boardArray[21] = F6;
            //boardArray[22] = G6;
            //boardArray[23] = H6;
            //boardArray[24] = A5;
            //boardArray[25] = B5;
            //boardArray[26] = C5;
            //boardArray[27] = D5;
            //boardArray[28] = E5;
            //boardArray[29] = F5;
            //boardArray[30] = G5;
            //boardArray[31] = H5;
            //boardArray[32] = A4;
            //boardArray[33] = B4;
            //boardArray[34] = C4;
            //boardArray[35] = D4;
            //boardArray[36] = E4;
            //boardArray[37] = F4;
            //boardArray[38] = G4;
            //boardArray[39] = H4;
            //boardArray[40] = A3;
            //boardArray[41] = B3;
            //boardArray[42] = C3;
            //boardArray[43] = D3;
            //boardArray[44] = E3;
            //boardArray[45] = F3;
            //boardArray[46] = G3;
            //boardArray[47] = H3;
            //boardArray[48] = A2;
            //boardArray[49] = B2;
            //boardArray[50] = C2;
            //boardArray[51] = D2;
            //boardArray[52] = E2;
            //boardArray[53] = F2;
            //boardArray[54] = G2;
            //boardArray[55] = H2;
            //boardArray[56] = A1;
            //boardArray[57] = B1;
            //boardArray[58] = C1;
            //boardArray[59] = D1;
            //boardArray[60] = E1;
            //boardArray[61] = F1;
            //boardArray[62] = G1;
            //boardArray[63] = H1;
            return boardArray;
        }
        private void assignImage(bool color, Type typeOfPiece, int i)
        {
            switch (color)
            {
                case true:
                    if (typeOfPiece == Type.Pawn) boardArray[i].BackgroundImage = Properties.Resources.bpawn_svg;
                    if (typeOfPiece == Type.Rook) boardArray[i].BackgroundImage = Properties.Resources.brook_svg;
                    if (typeOfPiece == Type.Knight) boardArray[i].BackgroundImage = Properties.Resources.bknight_svg;
                    if (typeOfPiece == Type.Bishop) boardArray[i].BackgroundImage = Properties.Resources.bbishop_svg;
                    if (typeOfPiece == Type.Queen) boardArray[i].BackgroundImage = Properties.Resources.bqueen_svg;
                    if (typeOfPiece == Type.King) boardArray[i].BackgroundImage = Properties.Resources.bking_svg;
                    break;
                case false:
                    if (typeOfPiece == Type.Pawn) boardArray[i].BackgroundImage = Properties.Resources.wpawn_svg;
                    if (typeOfPiece == Type.Rook) boardArray[i].BackgroundImage = Properties.Resources.wrook_svg;
                    if (typeOfPiece == Type.Knight) boardArray[i].BackgroundImage = Properties.Resources.wknight_svg;
                    if (typeOfPiece == Type.Bishop) boardArray[i].BackgroundImage = Properties.Resources.wbishop_svg;
                    if (typeOfPiece == Type.Queen) boardArray[i].BackgroundImage = Properties.Resources.wqueen_svg;
                    if (typeOfPiece == Type.King) boardArray[i].BackgroundImage = Properties.Resources.wking_svg;
                    break;
            }
            boardArray[i].BackgroundImageLayout = ImageLayout.Stretch;
        }
        //private int testIfPiece(int x, int y)//we want to ignore new posiiton of moved piece when testing for piece in a desired location // You never want to include the moving piece 
        //{
        //    int q = 99;
        //    for (int i = 0; i < allPieces.Length; i++)
        //    {
        //        if (allPieces[i].Position == new Point(x,y) && i != spotInPieceArray)
        //        {
        //            q = i;
        //        }
        //    }
        //    return q;
        //}
        private int findPiece(int x = 99,  int y = 99) // if no ints are sent for a position then it defaults to returning position of moved piece (oldPos)
        {
            int q = 99;
            for (int i = 0; i < allPieces.Length; i++)
            {
                if (x == 99 && y == 99 && allPieces[i].Position == oldPos) q = i;
                else if (allPieces[i].Position == new Point(x, y) && i != spotInPieceArray) q = i;
            }
            return q;
        }
        private int findBoardSpot(int x,int y)
        {
            int q = 0;
            for (int i = 0; i < boardArray.Length; i++)
            {
                if (x * 100 == boardArray[i].Location.X && y * 100 == boardArray[i].Location.Y)
                {
                    q = i;
                }
            }
            return q;
        }
        private void pBoxClick(object sender, MouseEventArgs e)
        {

            if (promote || haltMove) return; // if pawn can be promoted moves cannot be done until it is promoted

            PictureBox myBox = (PictureBox)sender;        
            if (!canMove)
            {
                String temp = myBox.Tag.ToString();
                oldPos = new Point(Convert.ToInt32(temp[0] - '0'), Convert.ToInt32(temp[1] - '0'));
                int q = findBoardSpot(oldPos.X, oldPos.Y);
              if (boardArray[q].BackgroundImage == null) { return; }
              if (gameMoves % 2 == 0 && allPieces[findPiece()].Color) return; // if its black on even moves it returns;
              else if (gameMoves % 2 == 1 && !allPieces[findPiece()].Color) return;// if its white on odd moves it returns;
                //           
                ogColor = boardArray[q].BackColor;
                boardArray[q].BackColor = Color.FromArgb(247, 247, 105);
                showLegals();
                canMove = true;
                return;
            }
            else if (canMove)
            {
                refreshLegals();
                //boardArray[findBoardSpot(oldPos.X, oldPos.Y)].BackColor = ogColor;
                String temp = myBox.Tag.ToString();
                newPos = new Point(Convert.ToInt32(temp[0]  - '0'), Convert.ToInt32(temp[1] - '0'));
                spotInPieceArray = findPiece();
              if (!Legal(allPieces[spotInPieceArray])) { refreshHighlights(); if (gameMoves > 0) { boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105); boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105); if (check) { boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59); } } canMove = false; return; }// legal switch statement                
              if (checkpreventsMove(allPieces[spotInPieceArray])) { refreshHighlights(); if (gameMoves > 0) { boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105); boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105); if (check) { boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59); } } canMove = false; return; }
              if (newPos == oldPos) { boardArray[findBoardSpot(oldPos.X, oldPos.Y)].BackColor = ogColor; canMove = false; return; }//redundant but pieces' highlights bug if not here
              if (findPiece(newPos.X, newPos.Y) != 99 && allPieces[findPiece()].Color == allPieces[findPiece(newPos.X, newPos.Y)].Color) { boardArray[findBoardSpot(oldPos.X, oldPos.Y)].BackColor = ogColor; canMove = false; return; }              
                int q = findBoardSpot(newPos.X, newPos.Y);
                refreshHighlights(findBoardSpot(oldPos.X, oldPos.Y), q);
                //ogColor = boardArray[q].BackColor;
                boardArray[q].BackColor = Color.FromArgb(247, 247, 105);
                canMove = false;
                          
                doMove();
            }
        }
        private void refreshHighlights(int one = 99, int two = 99)//issue: column 1 doesn't work right
        {
            int count = 1;
            for (int i = 0; i < boardArray.Length; i++)
            {
                if (i % 8 == 0 && (i == one || i == two)) continue; 
                else if (i == one || i == two) { count *= -1; continue; }

                if (i % 8 == 0) count *= -1;
                if (count == 1) { boardArray[i].BackColor = Color.FromArgb(238, 238, 210); count *= -1; continue; }
                else if (count == -1) { boardArray[i].BackColor = Color.FromArgb(118, 150, 86); count *= -1; }
            }
        }
        private void showLegals()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {
                    newPos = new Point(i, q);
                    spotInPieceArray = 999;
                    if (Legal(allPieces[findPiece()], true))//maybe add 'true' argument for sim
                    {
                        spotInPieceArray = findPiece();
                        if (checkpreventsMove(allPieces[findPiece()])) continue;
                        if (boardArray[findBoardSpot(newPos.X, newPos.Y)].BackgroundImage != null) 
                        { 
                            boardArray[findBoardSpot(newPos.X, newPos.Y)].Image = Properties.Resources.finalCirc;                           
                            continue; 
                        }
                        boardArray[findBoardSpot(newPos.X, newPos.Y)].BackgroundImage = Properties.Resources.gryCirc;
                        boardArray[findBoardSpot(newPos.X, newPos.Y)].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }
        private void refreshLegals()
        {
            //spotInPieceArray = 999;
            for (int i = 0; i < boardArray.Length; i++)
            {                   
                if (boardArray[i].Image != null) { boardArray[i].Image.Dispose(); boardArray[i].Image = null; }
                if (findPiece(boardArray[i].Location.X / 100, boardArray[i].Location.Y / 100) == 99 && boardArray[i].BackgroundImage != null)
                {
                    boardArray[i].BackgroundImage.Dispose();
                    boardArray[i].BackgroundImage = null;
                }
            }
        }
        private bool checkpreventsMove(Pieces piece)
        {
            Point oldold = oldPos, tempPos = allPieces[spotInPieceArray].Position;
            oldPos = newPos;
            allPieces[spotInPieceArray].Position = newPos;
            check = false;
            if (ifCheck(piece, piece.Color))
            {
                refreshHighlights();
                boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105);
                allPieces[spotInPieceArray].Position = tempPos; oldPos = oldold;
                if (piece.Type == Type.King) prevCheck = findBoardSpot(oldold.X, oldold.Y);
                boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
                boardArray[findBoardSpot(oldPos.X, oldPos.Y)].BackColor = Color.FromArgb(247, 247, 105);
                return true;
            }           
            oldPos = oldold; allPieces[spotInPieceArray].Position = tempPos;
            return false;
        }
        private void doMove()
        {           
            spotInPieceArray = findPiece(); // finds moved piece in piece array
            Pieces temp = allPieces[spotInPieceArray];
            
            int oldSpotInBoardArray = findBoardSpot(oldPos.X, oldPos.Y); // spot in boardArray of old location
            int newSpotInBoardArray = findBoardSpot(newPos.X, newPos.Y); // spot in boardArray of new location        

        //if (checkpreventsMove(temp)) return;

            prevOld = oldSpotInBoardArray;
            prevNew = newSpotInBoardArray;

            allPieces[spotInPieceArray].Moves++;

        //if (findPiece(newPos.X, newPos.Y) != 99 && newPos == allPieces[findPiece(newPos.X, newPos.Y)].Position && allPieces[findPiece(newPos.X, newPos.Y)].Type == Type.King) winner = true;
        // ^^^ tests if a king has been captured
            boardArray[oldSpotInBoardArray].BackgroundImage.Dispose();// removes visual image
            boardArray[oldSpotInBoardArray].BackgroundImage = null;//sets image value to null 
            allPieces[spotInPieceArray].Position = newPos; // updates piece position  

        foreach (Pieces piece in allPieces)//checks for if promotion can happen
        {
            if (piece.Type == Type.Pawn)
            {
                if ((piece.Color && piece.Position.Y == 7) || (!piece.Color && piece.Position.Y == 0)) { haltMove = true; promoteBoard(); }
            }          
        }
        if (findPiece(newPos.X, newPos.Y) != 99) { allPieces = removePiece(allPieces, findPiece(newPos.X, newPos.Y)); }

            assignImage(temp.Color, temp.Type, newSpotInBoardArray); // creates image
            //boardArray[newSpotInBoardArray].BackColor = Color.FromArgb(247, 247, 105);//
            gameMoves++;
            
        if (ifCheck()) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
            if (check)
            {
                int count = 0;
                for (int i = 0; i < allPieces.Length; i++)
                {
                    if (allPieces[i].Color == temp.Color) continue;
                    spotInPieceArray = i;
                    Point[] holder = allLegalMoves(allPieces[i]);
                    count += holder.Length;
                }
                if (count == 0) win();
                refreshHighlights();
                boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
            }
            //else if (!check)
            //{
            //    int count = 0;
            //    for (int i = 0; i < allPieces.Length; i++)
            //    {
            //        if (allPieces[i].Color == temp.Color) continue;
            //        spotInPieceArray = i;
            //        Point[] holder = allLegalMoves(allPieces[i]);
            //        count += holder.Length;
            //    }
            //    if (count == 0) label17.Text = "Draw";
            //    refreshHighlights();
            //    boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105);
            //    boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105);
            //    if (check) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);                             
            //}
        }
        private bool ifCheck(Pieces movedPiece = null, bool? curCol = null)
        {
            foreach (Pieces piece in allPieces)
            {
                if (piece.Type == Type.King)
                {
                    if (curCol != null && piece.Color != curCol) continue;

                    Sim sim1 = new Sim(allPieces, oldPos, gameMoves);//
                    promote = true;//
                    if (sim1.underAttack(piece.Position, allPieces, piece, movedPiece))//
                    {
                        promote = false;
                        label17.Text = "CHECK!";
                        boardArray[findBoardSpot(piece.Position.X, piece.Position.Y)].BackColor = Color.FromArgb(237, 59, 59);
                        prevCheck = findBoardSpot(piece.Position.X, piece.Position.Y);
                        return check = true;
                    }
                    else label17.Text = "No longer in check!";
                    promote = false;//
                }
            }
            return check = false;
        }
        //private bool underAttack(Point pos, Pieces target = null) // issue may involve spotInPieceArray in the findPiece() method. The variable changes when checkLegal is used for check sim
        //{
        //    promote = true; // just so moves cant be done while checking for check
        //    Point tempNew = newPos;
        //    Point tempOld = oldPos;

        //    foreach (Pieces piece in allPieces)
        //    {
        //        if (piece.Color != target.Color)
        //        {
        //            //if (check && piece.Position == allPieces[spotInPieceArray].Position) return check = false;
        //            newPos = target.Position;
        //            oldPos = piece.Position;

        //            int LM = piece.LastMove, temp = spotInPieceArray;
        //            spotInPieceArray = 99;
        //            if (checkLegal(piece, true)) check = true;
        //            spotInPieceArray = temp;
        //            piece.LastMove = LM;
        //        }
        //    }
        //    newPos = tempNew;
        //    oldPos = tempOld;
        //    promote = false;
        //    return check;
        //}
        private Point[] allLegalMoves(Pieces piece)
        {
            Point[] array = new Point[0];
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {
                    newPos = new Point(i, q);
                    oldPos = piece.Position;
                    if (Legal(piece, true) && !checkpreventsMove(piece))//maybe add 'true' argument for sim
                    {
                        Array.Resize(ref array, array.Length+1);
                        array[array.Length - 1] = newPos;
                    }
                }
            }
            return array;
        }
        private bool Legal(Pieces piece, bool sim = false)
        {
            bool legal = false;
            if (findPiece(newPos.X, newPos.Y) != 99 && allPieces[findPiece(newPos.X, newPos.Y)].Color == piece.Color) return legal = false;
            if (newPos == oldPos) return legal = false;
            int deltaX = Math.Abs(newPos.X - oldPos.X), deltaY = Math.Abs(newPos.Y - oldPos.Y);
            // if (findPiece(newPos.X, newPos.Y) != 99) { if (allPieces[findPiece(newPos.X, newPos.Y)].Color == piece.Color) { return legal; } } // if piece blocks desired move and its your own, method is returned.

            switch (piece.Type)
            {
                case Type.Pawn://issues with check may involve lastMove and other variables that are updated during move simulation.
                    //int x = newPos.X, y = newPos.Y;
                    //if (!allPieces[spotInPieceArray].Color && newPos.Y < oldPos.Y) // if white
                    //{
                    //    if (piece.Moves == 0 && (deltaY == 2 || deltaY == 1) && deltaX == 0 && testIfPiece(newPos.X, newPos.Y) == 99 && testIfPiece(newPos.X, newPos.Y + 1) == 99) { piece.Moves++; legal = true; piece.LastMove = gameMoves; }
                    //    if (piece.Moves > 0 && deltaY == 1 && deltaX == 0 && testIfPiece(newPos.X, newPos.Y) == 99) { piece.Moves++; legal = true; }
                    //    if (deltaY == 1 && deltaX == 1 && testIfPiece(newPos.X, newPos.Y) != 99) { piece.Moves++; legal = true; }
                    //    if (deltaY == 1 && deltaX == 1 && testIfPiece(oldPos.X - 1, oldPos.Y) != 99 && allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Position.Y == 3 && allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Moves == 1 && newPos.X == allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Position.X && (gameMoves - allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].LastMove < 2)) { allPieces[spotInPieceArray].Position = newPos; allPieces = removePiece(allPieces, testIfPiece(oldPos.X - 1, oldPos.Y)); boardArray[findBoardSpot(oldPos.X - 1, oldPos.Y)].BackgroundImage.Dispose(); boardArray[findBoardSpot(oldPos.X - 1, oldPos.Y)].BackgroundImage = null; legal = true; }
                    //    if (deltaY == 1 && deltaX == 1 && testIfPiece(oldPos.X + 1, oldPos.Y) != 99 && allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].Position.Y == 3 && allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].Moves == 1 && newPos.X == allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].Position.X && (gameMoves - allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].LastMove < 2)) { allPieces[spotInPieceArray].Position = newPos; allPieces = removePiece(allPieces, testIfPiece(oldPos.X + 1, oldPos.Y)); boardArray[findBoardSpot(oldPos.X + 1, oldPos.Y)].BackgroundImage.Dispose(); boardArray[findBoardSpot(oldPos.X + 1, oldPos.Y)].BackgroundImage = null; legal = true; } // this line bugged

                    //    if (newPos.Y == 0) promote = true;
                    //}
                    //if (allPieces[spotInPieceArray].Color && newPos.Y > oldPos.Y) // if black
                    //{
                    //    if (piece.Moves == 0 && (deltaY == 2 || deltaY == 1) && deltaX == 0 && testIfPiece(newPos.X, newPos.Y) == 99 && testIfPiece(newPos.X, newPos.Y - 1) == 99) { piece.Moves++; legal = true; piece.LastMove = gameMoves; }
                    //    if (piece.Moves > 0 && deltaY == 1 && deltaX == 0 && testIfPiece(newPos.X, newPos.Y) == 99) { piece.Moves++; legal = true; }
                    //    if (deltaY == 1 && deltaX == 1 && testIfPiece(newPos.X, newPos.Y) != 99) { piece.Moves++; legal = true; }
                    //    if (deltaY == 1 && deltaX == 1 && testIfPiece(oldPos.X - 1, oldPos.Y) != 99 && allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Position.Y == 4 && allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Moves == 1 && newPos.X == allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Position.X && (gameMoves - allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].LastMove < 2)) { allPieces[spotInPieceArray].Position = newPos; allPieces = removePiece(allPieces, testIfPiece(oldPos.X - 1, oldPos.Y)); boardArray[findBoardSpot(oldPos.X - 1, oldPos.Y)].BackgroundImage.Dispose(); boardArray[findBoardSpot(oldPos.X - 1, oldPos.Y)].BackgroundImage = null; legal = true; }
                    //    if (deltaY == 1 && deltaX == 1 && testIfPiece(oldPos.X + 1, oldPos.Y) != 99 && allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].Position.Y == 4 && allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].Moves == 1 && newPos.X == allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].Position.X && (gameMoves - allPieces[testIfPiece(oldPos.X + 1, oldPos.Y)].LastMove < 2)) { allPieces[spotInPieceArray].Position = newPos; allPieces = removePiece(allPieces, testIfPiece(oldPos.X + 1, oldPos.Y)); boardArray[findBoardSpot(oldPos.X + 1, oldPos.Y)].BackgroundImage.Dispose(); boardArray[findBoardSpot(oldPos.X + 1, oldPos.Y)].BackgroundImage = null; legal = true; }

                    //    if (newPos.Y == 7) promote = true;
                    //}
                    int decider = 1; if (!piece.Color) decider = -1;
                    int xmove = 1; if (newPos.X - oldPos.X == -1) xmove = -1;

                    if (oldPos.Y + decider == newPos.Y && deltaX == 0 && findPiece(newPos.X, newPos.Y) == 99) legal = true; // 1 move
                    if (piece.Moves == 0 && newPos.Y == 3.5 - (double)decider / 2 && deltaX == 0 && deltaY == 2 && findPiece(newPos.X, newPos.Y) == 99 && findPiece(newPos.X, newPos.Y-decider) == 99) { piece.LastMove = gameMoves; legal = true; } // 2 move
                    if (oldPos.Y + decider == newPos.Y && deltaX == 1 && findPiece(newPos.X, newPos.Y) != 99 && allPieces[findPiece(newPos.X, newPos.Y)].Color != piece.Color) legal = true; //take
                    if (oldPos.Y + decider == newPos.Y && deltaX == 1 && findPiece(oldPos.X + xmove, oldPos.Y) != 99 && allPieces[findPiece(oldPos.X + xmove, oldPos.Y)].Color != piece.Color && allPieces[findPiece(oldPos.X + xmove, oldPos.Y)].Moves == 1 && (gameMoves - allPieces[findPiece(oldPos.X + xmove, oldPos.Y)].LastMove < 2))
                    {
                        legal = true;
                        if (sim) return legal; 
                        allPieces = removePiece(allPieces, findPiece(oldPos.X + xmove, oldPos.Y)); 
                        boardArray[findBoardSpot(oldPos.X + xmove, oldPos.Y)].BackgroundImage.Dispose(); 
                        boardArray[findBoardSpot(oldPos.X + xmove, oldPos.Y)].BackgroundImage = null;
                    }
                    //if (oldPos.Y + decider == newPos.Y && deltaX == 1 && testIfPiece(oldPos.X - 1, oldPos.Y) != 99 && allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Color != piece.Color && allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].Moves == 1 && (gameMoves - allPieces[testIfPiece(oldPos.X - 1, oldPos.Y)].LastMove < 2))
                    //{
                    //    legal = true;
                    //    allPieces = removePiece(allPieces, testIfPiece(oldPos.X - 1, oldPos.Y));
                    //    boardArray[findBoardSpot(oldPos.X - 1, oldPos.Y)].BackgroundImage.Dispose();
                    //    boardArray[findBoardSpot(oldPos.X - 1, oldPos.Y)].BackgroundImage = null;
                    //}
                    
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
                            if (findPiece(x, y) != 99 && new Point(x,y) != oldPos) { legal = false; break; }
                        }
                    }
                    break;
                case Type.King:                   
                    if (Math.Abs(newPos.X - oldPos.X) == 1 && Math.Abs(newPos.Y - oldPos.Y) == 1) legal = true;
                    if (Math.Abs(newPos.X - oldPos.X) == 0 && Math.Abs(newPos.Y - oldPos.Y) == 1) legal = true;
                    if (Math.Abs(newPos.X - oldPos.X) == 1 && Math.Abs(newPos.Y - oldPos.Y) == 0) legal = true;

                    if (piece.Moves == 0 && deltaX == 2 && deltaY == 0)
                    {
                        Sim checkSim = new Sim(allPieces, oldPos, gameMoves);
                        if (newPos.X - 2 == oldPos.X && findPiece(7, newPos.Y) != 99 && allPieces[findPiece(7, newPos.Y)].Moves == 0 && findPiece(6, newPos.Y) == 99 && findPiece(5, newPos.Y) == 99)
                        {
                            if (!checkSim.underAttack(piece.Position, allPieces, piece))
                            {
                                piece.Position = new Point(6, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece))
                                { piece.Position = new Point(5, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece)) 
                                    {
                                        legal = true; 
                                        piece.Position = oldPos;
                                        if (sim) return legal;
                                        allPieces[findPiece(7, newPos.Y)].Moves++;
                                        allPieces[findPiece(7, newPos.Y)].Position = new Point(5, newPos.Y);                                       
                                        boardArray[findBoardSpot(7, newPos.Y)].BackgroundImage.Dispose();// removes visual image
                                        boardArray[findBoardSpot(7, newPos.Y)].BackgroundImage = null;
                                        assignImage(piece.Color, Type.Rook, findBoardSpot(5, newPos.Y)); // creates image
                                    } 
                                }
                            }
                        }
                        else if (newPos.X + 2 == oldPos.X && findPiece(0, newPos.Y) != 99 && allPieces[findPiece(0, newPos.Y)].Moves == 0 && findPiece(1, newPos.Y) == 99 && findPiece(2, newPos.Y) == 99)
                        {
                            if (!checkSim.underAttack(piece.Position, allPieces, piece))
                            {
                                piece.Position = new Point(1, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece))
                                { piece.Position = new Point(2, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece)) 
                                    { 
                                        legal = true; 
                                        piece.Position = oldPos;
                                        if (sim) return legal;
                                        allPieces[findPiece(0, newPos.Y)].Moves++;
                                        allPieces[findPiece(0, newPos.Y)].Position = new Point(3, newPos.Y);                                       
                                        boardArray[findBoardSpot(0, newPos.Y)].BackgroundImage.Dispose();// removes visual image
                                        boardArray[findBoardSpot(0, newPos.Y)].BackgroundImage = null;
                                        assignImage(piece.Color, Type.Rook, findBoardSpot(3, newPos.Y)); // creates image
                                    } 
                                }
                            }
                        }
                        piece.Position = oldPos;
                    }
                    break;
            }
            return legal;
        }
        private Pieces[] removePiece(Pieces[] array, int removeAt) // poss removable
        {
            array = array.Where((source, index) => index != removeAt).ToArray(); //INSTEA OF CALLING THIS METHOD            
            return array;
        }
        private void win()
        {
            haltMove = true;
            foreach (Pieces piece in allPieces)
            {
                if (piece.Type == Type.King)
                {
                    label18.Visible = true;
                    label19.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;                                      

                    if (piece.Color) { label18.Text = "Black Wins!"; pictureBox1.BackgroundImage = Properties.Resources.bigBlackKing; pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;}
                    else if (!piece.Color) { label18.Text = "White Wins!"; pictureBox1.BackgroundImage = Properties.Resources.bigWhiteKing; pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;}
                    label19.Text = "Press 'X' to play again!";
                }
            }
        }
        private void promoteBoard()
        {
            queen.Visible = true;
            rook.Visible = true;
            bishop.Visible = true;
            knight.Visible = true;

            if (allPieces[spotInPieceArray].Color)
            {
                queen.BackgroundImage = Properties.Resources.bqueen_svg; queen.BackgroundImageLayout = ImageLayout.Stretch;
                rook.BackgroundImage = Properties.Resources.brook_svg; rook.BackgroundImageLayout = ImageLayout.Stretch;
                bishop.BackgroundImage = Properties.Resources.bbishop_svg; bishop.BackgroundImageLayout = ImageLayout.Stretch;
                knight.BackgroundImage = Properties.Resources.bknight_svg; knight.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (!allPieces[spotInPieceArray].Color)
            {
                queen.BackgroundImage = Properties.Resources.wqueen_svg; queen.BackgroundImageLayout = ImageLayout.Stretch;
                rook.BackgroundImage = Properties.Resources.wrook_svg; rook.BackgroundImageLayout = ImageLayout.Stretch;
                bishop.BackgroundImage = Properties.Resources.wbishop_svg; bishop.BackgroundImageLayout = ImageLayout.Stretch;
                knight.BackgroundImage = Properties.Resources.wknight_svg; knight.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        private void promoChoice(object sender, MouseEventArgs e) // mouseClick
        {
            Button theBox = (Button)sender;
            Pieces placeHolder = allPieces[spotInPieceArray];
            int newSpotInBoardArray = findBoardSpot(newPos.X, newPos.Y);

            queen.Visible = false;
            rook.Visible = false;
            bishop.Visible = false;
            knight.Visible = false;
            promote = false;
            haltMove = false;

            switch (theBox.Name)
            {
                case "queen":
                    allPieces[spotInPieceArray].Type = Type.Queen;
                    assignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (ifCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);//issue is prob that temp is clone of an outdated and updated piece
                    break;
                case "rook":
                    allPieces[spotInPieceArray].Type = Type.Rook;
                    assignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (ifCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);//issue is prob that temp is clone of an outdated and updated piece
                    break;
                case "bishop":
                    allPieces[spotInPieceArray].Type = Type.Bishop;
                    assignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (ifCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);//issue is prob that temp is clone of an outdated and updated piece
                    break;
                case "knight":
                    allPieces[spotInPieceArray].Type = Type.Knight;
                    assignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (ifCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);//issue is prob that temp is clone of an outdated and updated piece
                    break;
            }
        }
        private void restartGame(object sender, MouseEventArgs e)
        {
            Application.Restart();
        }
    }
}