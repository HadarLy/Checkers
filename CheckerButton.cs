using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class CheckerButton : Button
    {
        private readonly BoardPosition r_CheckerButtonBoardPosition;

        public CheckerButton(BoardPosition i_CheckerButtonBoardPosition)
        {
            r_CheckerButtonBoardPosition = new BoardPosition();
            r_CheckerButtonBoardPosition.Row = i_CheckerButtonBoardPosition.Row;
            r_CheckerButtonBoardPosition.Column = i_CheckerButtonBoardPosition.Column;
        }

        public BoardPosition CheckerButtonBoardPosition
        {
            get { return r_CheckerButtonBoardPosition; }
        }
    }
}