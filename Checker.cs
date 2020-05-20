using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B18_Ex05
{
    public class Checker
    {
        public enum eCheckerColor
        {
            Black,
            White
        }

        public enum eSignOfPlayerInChecker
        {
            NoPlayer,
            X,
            O
        }

        private eCheckerColor m_Color;
        private eSignOfPlayerInChecker m_SignOfPlayerInChecker;
        private char m_SignOfPieceInChecker;
        private bool m_IsCheckerEmpty;
        private BoardPosition m_CheckerBoardPosition;

        public eCheckerColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public char SignOfPieceInChecker
        {
            get { return m_SignOfPieceInChecker; }
            set { m_SignOfPieceInChecker = value; }
        }

        public bool IsCheckerEmpty
        {
            get { return m_IsCheckerEmpty; }
            set { m_IsCheckerEmpty = value; }
        }

        public eSignOfPlayerInChecker SignOfPlayerInChecker
        {
            get { return m_SignOfPlayerInChecker; }
            set { m_SignOfPlayerInChecker = value; }
        }

        public BoardPosition CheckerBoardPosition
        {
            get { return m_CheckerBoardPosition; }
            set { m_CheckerBoardPosition = value; }
        }
    }
}