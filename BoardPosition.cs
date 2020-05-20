using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class BoardPosition
    {
        private int m_Row;
        private int m_Column;

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public bool CompareBoardPositions(BoardPosition i_CompareToPosition)
        {
            bool isSameBoardPosition = !true;

            isSameBoardPosition = (m_Row == i_CompareToPosition.m_Row) && (m_Column == i_CompareToPosition.m_Column);

            return isSameBoardPosition;
        }
    }
}