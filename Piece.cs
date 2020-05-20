using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B18_Ex05
{
    public class Piece
    {
        private BoardPosition m_Position;
        private char m_PieceSign;

        public enum ePieceType
        {
            Men,
            King
        }

        private ePieceType m_PieceType;
        private bool m_IsPieceStillActive;

        public BoardPosition Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public ePieceType PieceType
        {
            get { return m_PieceType; }
            set { m_PieceType = value; }
        }

        public char PieceSign
        {
            get { return m_PieceSign; }
            set { m_PieceSign = value; }
        }

        public bool IsPieceStillActive
        {
            get { return m_IsPieceStillActive; }
            set { m_IsPieceStillActive = value; }
        }
    }
}