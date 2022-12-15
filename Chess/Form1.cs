﻿using System;
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
    {// all methods of drawing (stalemate...etc) //winning screen / main menu // timer?
        PictureBox[] boardArray;
        Pieces[] allPieces;
        Point oldPos;
        Point newPos;
        bool canMove = false;        //change name
        bool promote, haltMove = false;
        bool check = false;
        int spotInPieceArray;
        int gameMoves = 0, fiftyMoveBreaker = 0;
        int prevOld, prevNew, prevCheck;//make array Exclusions[]
        Color ogColor;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            boardArray = InitializeBoardArray();
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
        private PictureBox[] InitializeBoardArray()
        {
            PictureBox[] boardArray = new PictureBox[64];
            foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
                if (pb.Tag != null) boardArray[Convert.ToInt32(pb.Tag.ToString()[1] - '0') * 8 + Convert.ToInt32(pb.Tag.ToString()[0] - '0')] = pb; 
            return boardArray;
        }
        private void AssignImage(bool color, Type typeOfPiece, int i)
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
        private int FindPiece(int x = 99, int y = 99)
        {
            for (int i = 0; i < allPieces.Length; i++)
            {
                if (x == 99 && y == 99 && allPieces[i].Position == oldPos) return i;
                else if (allPieces[i].Position == new Point(x, y) && i != spotInPieceArray) return i;
            } 
            return 99;
        }
        private int FindBoardSpot(int x, int y)
        {
            for (int i = 0; i < boardArray.Length; i++)            
                if (x * 100 == boardArray[i].Location.X && y * 100 == boardArray[i].Location.Y) return i;          
            return 0;
        }
        private void pBoxClick(object sender, MouseEventArgs e)
        {
            if (promote || haltMove) return; // if pawn can be promoted moves cannot be done until it is promoted

            PictureBox myBox = (PictureBox)sender;
            if (!canMove)
            {
                String temp = myBox.Tag.ToString();
                oldPos = new Point(Convert.ToInt32(temp[0] - '0'), Convert.ToInt32(temp[1] - '0'));
                int q = FindBoardSpot(oldPos.X, oldPos.Y);
                if (boardArray[q].BackgroundImage == null) return; 
                if (gameMoves % 2 == 0 && allPieces[FindPiece()].Color) return; // if its black on even moves it returns;
                else if (gameMoves % 2 == 1 && !allPieces[FindPiece()].Color) return;// if its white on odd moves it returns;
                //           
                ogColor = boardArray[q].BackColor;
                boardArray[q].BackColor = Color.FromArgb(247, 247, 105);
                ShowLegals();
                canMove = true;
                return;
            }
            else if (canMove)
            {
                RefreshLegals();
                String temp = myBox.Tag.ToString();
                newPos = new Point(Convert.ToInt32(temp[0] - '0'), Convert.ToInt32(temp[1] - '0'));
                spotInPieceArray = FindPiece();
                if (!Legal(allPieces[spotInPieceArray])) { RefreshHighlights(); if (gameMoves > 0) { boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105); boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105); if (check) { boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59); } } canMove = false; return; }// legal switch statement                
                if (CheckPreventsMove(allPieces[spotInPieceArray])) { RefreshHighlights(); if (gameMoves > 0) { boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105); boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105); if (check) { boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59); } } canMove = false; return; }
                if (newPos == oldPos) { boardArray[FindBoardSpot(oldPos.X, oldPos.Y)].BackColor = ogColor; canMove = false; return; }//redundant but pieces' highlights bug if not here
                if (FindPiece(newPos.X, newPos.Y) != 99 && allPieces[FindPiece()].Color == allPieces[FindPiece(newPos.X, newPos.Y)].Color) { boardArray[FindBoardSpot(oldPos.X, oldPos.Y)].BackColor = ogColor; canMove = false; return; }
                int q = FindBoardSpot(newPos.X, newPos.Y);
                RefreshHighlights(FindBoardSpot(oldPos.X, oldPos.Y), q);
                boardArray[q].BackColor = Color.FromArgb(247, 247, 105);
                canMove = false;

                DoMove();
            }
        }
        private void RefreshHighlights(int one = 99, int two = 99)//issue: column 1 doesn't work right
        {
            bool even = false;
            for (int i = 0; i < boardArray.Length; i++)
            {
                if (i % 8 == 0 && (i == one || i == two)) continue;
                else if (i == one || i == two) { even =  !even; continue; }

                if (i % 8 == 0) even = !even;
                if (!even) { boardArray[i].BackColor = Color.FromArgb(238, 238, 210); even = !even; continue; }
                else if (even) { boardArray[i].BackColor = Color.FromArgb(118, 150, 86); even = !even; }
            }
        }
        private void ShowLegals()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {
                    newPos = new Point(i, q);
                    spotInPieceArray = 999;
                    if (Legal(allPieces[FindPiece()], true))
                    {
                        spotInPieceArray = FindPiece();
                        if (CheckPreventsMove(allPieces[spotInPieceArray])) continue;
                        if (boardArray[FindBoardSpot(i, q)].BackgroundImage != null)
                        {
                            boardArray[FindBoardSpot(i, q)].Image = Properties.Resources.finalCirc;
                            continue;
                        }
                        boardArray[FindBoardSpot(i, q)].BackgroundImage = Properties.Resources.gryCirc;
                        boardArray[FindBoardSpot(i, q)].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }
        private void RefreshLegals()
        {
            for (int i = 0; i < boardArray.Length; i++)
            {
                if (boardArray[i].Image != null) { boardArray[i].Image.Dispose(); boardArray[i].Image = null; }
                if (FindPiece(boardArray[i].Location.X / 100, boardArray[i].Location.Y / 100) == 99 && boardArray[i].BackgroundImage != null && !check)//!chekc fixes issue with king on square 62 going away in check
                {
                    boardArray[i].BackgroundImage.Dispose();
                    boardArray[i].BackgroundImage = null;
                }
            }
        }
        private bool CheckPreventsMove(Pieces piece)
        {
            Point oldold = oldPos, tempPos = allPieces[spotInPieceArray].Position;
            oldPos = newPos;
            allPieces[spotInPieceArray].Position = newPos;
            check = false;
            if (IfCheck(piece, piece.Color))
            {
                RefreshHighlights();
                boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105);
                allPieces[spotInPieceArray].Position = tempPos; oldPos = oldold;
                if (piece.Type == Type.King) prevCheck = FindBoardSpot(oldold.X, oldold.Y);
                boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
                boardArray[FindBoardSpot(oldPos.X, oldPos.Y)].BackColor = Color.FromArgb(247, 247, 105);
                return true;
            }
            oldPos = oldold; allPieces[spotInPieceArray].Position = tempPos;
            return false;
        }
        private void DoMove()
        {
            fiftyMoveBreaker++;
            spotInPieceArray = FindPiece(); // finds moved piece in piece array
            Pieces temp = allPieces[spotInPieceArray];

            int oldSpotInBoardArray = FindBoardSpot(oldPos.X, oldPos.Y); // spot in boardArray of old location
            int newSpotInBoardArray = FindBoardSpot(newPos.X, newPos.Y); // spot in boardArray of new location        

            prevOld = oldSpotInBoardArray;
            prevNew = newSpotInBoardArray;

            allPieces[spotInPieceArray].Moves++;

            boardArray[oldSpotInBoardArray].BackgroundImage.Dispose();// removes visual image
            boardArray[oldSpotInBoardArray].BackgroundImage = null;//sets image value to null 
            allPieces[spotInPieceArray].Position = newPos; // updates piece position  

            foreach (Pieces piece in allPieces) if (piece.Type == Type.Pawn)//checks for if promotion can happen
            {               
                if ((piece.Color && piece.Position.Y == 7) || (!piece.Color && piece.Position.Y == 0)) { haltMove = true; PromoteBoard(); }                
            }

            if (FindPiece(newPos.X, newPos.Y) != 99) { allPieces = RemovePiece(allPieces, FindPiece(newPos.X, newPos.Y)); fiftyMoveBreaker = 0; }

            AssignImage(temp.Color, temp.Type, newSpotInBoardArray);
            
            gameMoves++;

            if (IfCheck()) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
            if (check)//checkmate
            {
                int count = 0;
                for (int i = 0; i < allPieces.Length; i++)
                {
                    if (allPieces[i].Color == temp.Color) continue;
                    spotInPieceArray = i;
                    Point[] holder = AllLegalMoves(allPieces[i]);
                    count += holder.Length;
                }
                if (count == 0) Win(temp.Color);
                RefreshHighlights();
                boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
            }
            else if (!check)//stalemate
            {
                int count = 0;
                for (int i = 0; i < allPieces.Length; i++)
                {
                    if (allPieces[i].Color == temp.Color) continue;
                    spotInPieceArray = i;
                    Point[] holder = AllLegalMoves(allPieces[i]);
                    count += holder.Length;
                }
                if (count == 0) label17.Text = "Draw";
                RefreshHighlights();
                boardArray[prevNew].BackColor = Color.FromArgb(247, 247, 105);
                boardArray[prevOld].BackColor = Color.FromArgb(247, 247, 105);
                if (check) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
            }
            //three fold repition (fen String will make this so easy) "CurrentFenString()" OR a Pieces[][] jagged array (array of allPieces[] from each gameMove)
            //Pieces[(each iteration is an allpieces array)][the allpieces array for that gamemove] 
            if (fiftyMoveBreaker == 50) { label17.Text = "Draw"; }//50 move rule COMPLETE 
            //dead position
        }
        private bool IfCheck(Pieces movedPiece = null, bool? curCol = null)
        {
            foreach (Pieces piece in allPieces) if (piece.Type == Type.King) 
            {                              
                if (curCol != null && piece.Color != curCol) continue;
                Sim sim1 = new Sim(allPieces, oldPos, gameMoves);
                haltMove = true;
                if (sim1.underAttack(piece.Position, allPieces, piece, movedPiece))//
                {
                    haltMove = false;
                    label17.Text = "CHECK!";//
                    boardArray[FindBoardSpot(piece.Position.X, piece.Position.Y)].BackColor = Color.FromArgb(237, 59, 59);
                    prevCheck = FindBoardSpot(piece.Position.X, piece.Position.Y);
                    return check = true;
                }
                else label17.Text = "Not in check!";//
                haltMove = false;                
            }
            return check = false;
        }
        private Point[] AllLegalMoves(Pieces piece)
        {
            Point[] array = new Point[0];
            for (int i = 0; i < 8; i++)
            {
                for (int q = 0; q < 8; q++)
                {
                    newPos = new Point(i, q);
                    oldPos = piece.Position;
                    if (Legal(piece, true) && !CheckPreventsMove(piece))
                    {
                        Array.Resize(ref array, array.Length + 1);
                        array[array.Length - 1] = newPos;
                    }
                }
            }
            return array;
        }
        private bool Legal(Pieces piece, bool sim = false)
        {
            bool legal = false;
            if (FindPiece(newPos.X, newPos.Y) != 99 && allPieces[FindPiece(newPos.X, newPos.Y)].Color == piece.Color) return legal;
            if (newPos == oldPos) return legal;
            int deltaX = Math.Abs(newPos.X - oldPos.X), deltaY = Math.Abs(newPos.Y - oldPos.Y);

            switch (piece.Type)
            {
                case Type.Pawn:
                    int decider = 1; if (!piece.Color) decider = -1;
                    int xmove = 1; if (newPos.X - oldPos.X == -1) xmove = -1;

                    if (oldPos.Y + decider == newPos.Y && deltaX == 0 && FindPiece(newPos.X, newPos.Y) == 99) legal = true; // 1 move
                    if (piece.Moves == 0 && newPos.Y == 3.5 - (double)decider / 2 && deltaX == 0 && deltaY == 2 && FindPiece(newPos.X, newPos.Y) == 99 && FindPiece(newPos.X, newPos.Y - decider) == 99) { piece.LastMove = gameMoves; legal = true; } // 2 move
                    if (oldPos.Y + decider == newPos.Y && deltaX == 1 && FindPiece(newPos.X, newPos.Y) != 99 && allPieces[FindPiece(newPos.X, newPos.Y)].Color != piece.Color) legal = true; //take
                    if (oldPos.Y + decider == newPos.Y && deltaX == 1 && FindPiece(oldPos.X + xmove, oldPos.Y) != 99 && allPieces[FindPiece(oldPos.X + xmove, oldPos.Y)].Color != piece.Color && allPieces[FindPiece(oldPos.X + xmove, oldPos.Y)].Moves == 1 && (gameMoves - allPieces[FindPiece(oldPos.X + xmove, oldPos.Y)].LastMove < 2))
                    {
                        legal = true;
                        if (sim) return legal;
                        allPieces = RemovePiece(allPieces, FindPiece(oldPos.X + xmove, oldPos.Y));
                        boardArray[FindBoardSpot(oldPos.X + xmove, oldPos.Y)].BackgroundImage.Dispose();
                        boardArray[FindBoardSpot(oldPos.X + xmove, oldPos.Y)].BackgroundImage = null;
                    }
                    if (!sim && legal) fiftyMoveBreaker = -1;//doMove makes this 0 by adding 1
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
                            if (FindPiece(x, y) != 99 && new Point(x, y) != oldPos) { legal = false; break; }
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
                        if (newPos.X - 2 == oldPos.X && FindPiece(7, newPos.Y) != 99 && allPieces[FindPiece(7, newPos.Y)].Moves == 0 && FindPiece(6, newPos.Y) == 99 && FindPiece(5, newPos.Y) == 99)
                        {
                            if (!checkSim.underAttack(piece.Position, allPieces, piece))
                            {
                                piece.Position = new Point(6, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece))
                                {
                                    piece.Position = new Point(5, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece))
                                    {
                                        legal = true;
                                        piece.Position = oldPos;
                                        if (sim) return legal;
                                        allPieces[FindPiece(7, newPos.Y)].Moves++;
                                        allPieces[FindPiece(7, newPos.Y)].Position = new Point(5, newPos.Y);
                                        boardArray[FindBoardSpot(7, newPos.Y)].BackgroundImage.Dispose();// removes visual image
                                        boardArray[FindBoardSpot(7, newPos.Y)].BackgroundImage = null;
                                        AssignImage(piece.Color, Type.Rook, FindBoardSpot(5, newPos.Y)); // creates image
                                    }
                                }
                            }
                        }
                        else if (newPos.X + 2 == oldPos.X && FindPiece(0, newPos.Y) != 99 && allPieces[FindPiece(0, newPos.Y)].Moves == 0 && FindPiece(1, newPos.Y) == 99 && FindPiece(2, newPos.Y) == 99)
                        {
                            if (!checkSim.underAttack(piece.Position, allPieces, piece))
                            {
                                piece.Position = new Point(1, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece))
                                {
                                    piece.Position = new Point(2, newPos.Y); if (!checkSim.underAttack(piece.Position, allPieces, piece))
                                    {
                                        legal = true;
                                        piece.Position = oldPos;
                                        if (sim) return legal;
                                        allPieces[FindPiece(0, newPos.Y)].Moves++;
                                        allPieces[FindPiece(0, newPos.Y)].Position = new Point(3, newPos.Y);
                                        boardArray[FindBoardSpot(0, newPos.Y)].BackgroundImage.Dispose();// removes visual image
                                        boardArray[FindBoardSpot(0, newPos.Y)].BackgroundImage = null;
                                        AssignImage(piece.Color, Type.Rook, FindBoardSpot(3, newPos.Y)); // creates image
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
        private Pieces[] RemovePiece(Pieces[] array, int removeAt) { return array = array.Where((source, index) => index != removeAt).ToArray(); }
        private void Win(bool color)// maybe have set pictureboxes and stuff premade on Form1.cs [Design]* then fill in accoridng to how game ends
        {
            haltMove = true;
            PictureBox winningBox = new PictureBox();
            this.Controls.Add(winningBox);
            winningBox.Location = new Point(150, 250);
            winningBox.Size = new Size(500, 300);
            winningBox.BringToFront();

            Rectangle r = new Rectangle(0, 0, winningBox.Width, winningBox.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 22;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            winningBox.Region = new Region(gp);

            Label winMsg = new Label();
            this.Controls.Add(winMsg);
            String msg = ""; if (color) msg = "Black Won!"; else if (!color) msg = "White Won!";
            winMsg.Location = new Point(360, (int)(winningBox.Location.Y * 1.125));
            winMsg.BringToFront();
            winMsg.Text = msg;
            Font ltrFont = new Font("Arial", 12);
            winMsg.Font = ltrFont;

            PictureBox exitBox = new PictureBox();
            this.Controls.Add(exitBox);
            exitBox.Location = new Point(winningBox.Location.X + winningBox.Width - 50, winningBox.Location.Y + 50);
            exitBox.Size = new Size(50, 50);
            exitBox.BringToFront();
            exitBox.BackgroundImage = Properties.Resources.gryCirc;// change to X
        }
        private void PromoteBoard()
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
            int newSpotInBoardArray = FindBoardSpot(newPos.X, newPos.Y);

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
                    AssignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (IfCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
                    break;
                case "rook":
                    allPieces[spotInPieceArray].Type = Type.Rook;
                    AssignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (IfCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
                    break;
                case "bishop":
                    allPieces[spotInPieceArray].Type = Type.Bishop;
                    AssignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (IfCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
                    break;
                case "knight":
                    allPieces[spotInPieceArray].Type = Type.Knight;
                    AssignImage(placeHolder.Color, placeHolder.Type, newSpotInBoardArray);
                    if (IfCheck(allPieces[spotInPieceArray])) boardArray[prevCheck].BackColor = Color.FromArgb(237, 59, 59);
                    break;
            }
        }
        private void RestartGame(object sender, MouseEventArgs e) { Application.Restart(); }
    }
}