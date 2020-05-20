using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Player
    {
        private const int k_KingValue = 4;

        public enum eNumberOfPiecesForPlayer
        {
            NumberOfPiecesInSmallBoard = 6,
            NumberOfPiecesInMediumBoard = 12,
            NumberOfPiecesInLargeBoard = 20
        }

        private readonly string r_Name;
        private readonly int r_PlayerNumber;
        private int m_Score;
        private readonly bool r_IsPlayerComputer;
        private readonly List<Piece> m_PlayerPieces;
        private readonly eNumberOfPiecesForPlayer r_NumberOfPiecesForPlayer;
        private int m_NumberOfActivePieces;
        private readonly char r_PlayerSign;
        private List<Move> m_PossiableMoves;
        private List<Move> m_MustEatMoves;

        public Player(string i_PlayerName, bool i_IsComputer, char i_PlayerSign, int i_BoardSize, int i_PlayerNumber)
        {
            int numberOfPiecesInARow;
            int numberOfRowsForEachPlayer;

            numberOfPiecesInARow = i_BoardSize / 2;
            numberOfRowsForEachPlayer = (i_BoardSize - 2) / 2;
            r_NumberOfPiecesForPlayer = (eNumberOfPiecesForPlayer)(numberOfPiecesInARow * numberOfRowsForEachPlayer);
            m_NumberOfActivePieces = (int)r_NumberOfPiecesForPlayer;
            r_IsPlayerComputer = i_IsComputer;
            r_Name = i_PlayerName;
            m_Score = 0;
            r_PlayerSign = i_PlayerSign;
            m_PossiableMoves = new List<Move>();
            m_MustEatMoves = new List<Move>();
            m_PlayerPieces = new List<Piece>();
            r_PlayerNumber = i_PlayerNumber;
        }

        public string Name
        {
            get { return r_Name; }
        }

        public bool IsPlayerComputer
        {
            get { return r_IsPlayerComputer; }
        }

        public List<Piece> PlayerPieces
        {
            get { return m_PlayerPieces; }
        }

        public List<Move> PossiableMoves
        {
            get { return m_PossiableMoves; }
            set { m_PossiableMoves = value; }
        }

        public List<Move> MustEatMoves
        {
            get { return m_MustEatMoves; }
            set { m_MustEatMoves = value; }
        }

        public char PlayerSign
        {
            get { return r_PlayerSign; }
        }

        public int NumberOfActivePieces
        {
            get { return m_NumberOfActivePieces; }
            set { m_NumberOfActivePieces = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public int PlayerNumber
        {
            get { return r_PlayerNumber; }
        }

        public eNumberOfPiecesForPlayer NumberOfPiecesForPlayer
        {
            get { return r_NumberOfPiecesForPlayer; }
        }

        public void CreateAndSetPiecesForStartPositions()
        {
            for (int i = 0; i < (int)r_NumberOfPiecesForPlayer; i++)
            {
                m_PlayerPieces.Add(new Piece());
                m_PlayerPieces[i].Position = new BoardPosition();
                if (r_PlayerSign == 'O')
                {
                    ///put picture of piece 1 team
                }
                else
                {
                    ///put picture of piece 2 team
                }
            }

            SetPiecesForStartPositions();
        }

        public void SetPiecesForStartPositions()
        {
            int numberOfPiecesInARow;
            int numberOfRowsForEachPlayer;
            int row, column, countLine;

            if (r_NumberOfPiecesForPlayer == eNumberOfPiecesForPlayer.NumberOfPiecesInLargeBoard)
            {
                numberOfRowsForEachPlayer = 4;
            }
            else if (r_NumberOfPiecesForPlayer == eNumberOfPiecesForPlayer.NumberOfPiecesInMediumBoard)
            {
                numberOfRowsForEachPlayer = 3;
            }
            else
            {
                numberOfRowsForEachPlayer = 2;
            }

            numberOfPiecesInARow = (int)r_NumberOfPiecesForPlayer / numberOfRowsForEachPlayer;

            if (r_PlayerSign == 'O')
            {
                row = 0;
                column = 1;
            }
            else
            {
                row = numberOfRowsForEachPlayer + 2;
                if (numberOfRowsForEachPlayer % 2 == 0)
                {
                    column = 1;
                }
                else
                {
                    column = 0;
                }
            }

            countLine = 1;

            for (int i = 0; i < (int)r_NumberOfPiecesForPlayer; i++)
            {
                if ((row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0))
                {
                    m_PlayerPieces[i].IsPieceStillActive = true;
                    m_PlayerPieces[i].PieceType = Piece.ePieceType.Men;
                    m_PlayerPieces[i].Position.Row = row;
                    m_PlayerPieces[i].Position.Column = column;
                    m_PlayerPieces[i].PieceSign = r_PlayerSign;
                }

                column += 2;

                if (i == (countLine * numberOfPiecesInARow) - 1)
                {
                    countLine++;
                    row++;

                    if (row % 2 == 0)
                    {
                        column = 1;
                    }
                    else
                    {
                        column = 0;
                    }
                }
            }
        }

        public int FindIndexOfPieceOnTheListOfPieces(BoardPosition i_PositionOfPiece)
        {
            int indexOfPiece = 0; //// The first index on the list. If it's the piece we were looking for, it would change during the loop.

            for (int i = 0; i < m_PlayerPieces.Count; i++)
            {
                if (m_PlayerPieces[i].Position.CompareBoardPositions(i_PositionOfPiece) == true)
                {
                    indexOfPiece = i;
                }
            }

            return indexOfPiece;
        }

        public bool IsMoveOnMustEatMovesList(BoardPosition i_FromPosition)
        {
            bool isMoveFound = !true;

            for (int i = 0; i < m_MustEatMoves.Count; i++)
            {
                if (m_MustEatMoves[i].FromChecker.Row == i_FromPosition.Row && m_MustEatMoves[i].FromChecker.Column == i_FromPosition.Column)
                {
                    isMoveFound = true;
                }
            }

            return isMoveFound;
        }

        public bool CheckIfMoveIsOnList(Move i_SearchForMove)
        {
            bool isMoveOnTheList = !true;
            List<Move> currentList;

            if (i_SearchForMove.EatingMove == !true)
            {
                currentList = m_PossiableMoves;
            }
            else
            {
                currentList = m_MustEatMoves;
            }

            foreach (Move move in currentList)
            {
                if (move.CompareMoves(i_SearchForMove) == true)
                {
                    isMoveOnTheList = true;
                }
            }

            return isMoveOnTheList;
        }

        public int CountNumberOfKingsAndMenForPlayer(ref int o_NumberOfMen)
        {
            int numberOfKings = 0;

            for (int i = 0; i < m_PlayerPieces.Count; i++)
            {
                if (m_PlayerPieces[i].IsPieceStillActive)
                {
                    if (m_PlayerPieces[i].PieceType == Piece.ePieceType.King)
                    {
                        numberOfKings++;
                    }
                    else
                    {
                        o_NumberOfMen++;
                    }
                }
            }

            return numberOfKings;
        }

        public int CalculatePointsForPlayer()
        {
            int numberOfMenForPlayer = 0;
            int numberOfKingsForPlayer = 0;
            int statusOfPoints;

            numberOfKingsForPlayer = CountNumberOfKingsAndMenForPlayer(ref numberOfMenForPlayer);
            statusOfPoints = numberOfMenForPlayer + (k_KingValue * numberOfKingsForPlayer);

            return statusOfPoints;
        }
    }
}