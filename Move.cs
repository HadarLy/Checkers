using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Move
    {
        private readonly BoardPosition r_FromChecker;
        private readonly BoardPosition r_ToChecker;
        private readonly bool r_EatingMove;

        public Move(BoardPosition i_FromChecker, BoardPosition i_ToChecker, bool i_EatingMove)
        {
            r_FromChecker = i_FromChecker;
            r_ToChecker = i_ToChecker;
            r_EatingMove = i_EatingMove;
        }

        public BoardPosition FromChecker
        {
            get { return r_FromChecker; }
        }

        public BoardPosition ToChecker
        {
            get { return r_ToChecker; }
        }

        public bool EatingMove
        {
            get { return r_EatingMove; }
        }

        public bool CheckifMoveMakeMenAKing(int i_BoardSize)
        {
            bool isMoveMakesKing = !true;

            if ((r_ToChecker.Row == 0) || (r_ToChecker.Row == i_BoardSize - 1))
            {
                isMoveMakesKing = true;
            }

            return isMoveMakesKing;
        }

        public bool CompareMoves(Move i_CompareTo)
        {
            bool isMoveEqual = !true;

            if (r_FromChecker.Row == i_CompareTo.FromChecker.Row && r_FromChecker.Column == i_CompareTo.FromChecker.Column)
            {
                if (r_ToChecker.Row == i_CompareTo.ToChecker.Row && r_ToChecker.Column == i_CompareTo.ToChecker.Column)
                {
                    isMoveEqual = true;
                }
            }

            return isMoveEqual;
        }
    }
}
