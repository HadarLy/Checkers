using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Game
    {
        private bool m_IsSecondPlayerComputer;
        private Board m_CheckerBoard;
        private Player m_Player1, m_Player2;
        private int m_NumberOfGames;
        private bool m_IsMatchOver;
        private bool m_IsGameOver;
        private string m_LeadingPlayer;

        public enum eGameStatus
        {
            Running,
            Tie,
            Win
        }

        private eGameStatus m_GameStatus;

        public Game(int i_ChoiseForBoardSize, bool i_IsOpponentComputer, string i_player1Name, string i_player2Name)
        {
            m_NumberOfGames = 0;
            m_IsGameOver = !true;
            m_IsMatchOver = !true;
            m_GameStatus = Game.eGameStatus.Running;
            m_CheckerBoard = new Board(i_ChoiseForBoardSize);
            m_IsSecondPlayerComputer = i_IsOpponentComputer;
            m_LeadingPlayer = "No Leading Player Yet";

            if (i_player1Name == string.Empty) 
            {
                // default value
                i_player1Name = "Player 1";
            }

            if (i_player2Name == string.Empty) 
            {
                // default value
                i_player2Name = "Player 2";
            }
            else if (i_IsOpponentComputer)
            {
                i_player2Name = "Computer";
            }

            Player1 = new Player(i_player1Name, !true, 'O', (int)m_CheckerBoard.BoardSize, 1);
            Player2 = new Player(i_player2Name, m_IsSecondPlayerComputer, 'X', (int)m_CheckerBoard.BoardSize, 2);
        }

        public Board CheckerBoard
        {
            get { return m_CheckerBoard; }
            set { m_CheckerBoard = value; }
        }

        public Player Player1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        public bool IsSecondPlayerComputer
        {
            get { return m_IsSecondPlayerComputer; }
            set { m_IsSecondPlayerComputer = value; }
        }

        public int NumberOfGames
        {
            get { return m_NumberOfGames; }
            set { m_NumberOfGames = value; }
        }

        public bool IsMatchOver
        {
            get { return m_IsMatchOver; }
            set { m_IsMatchOver = value; }
        }

        public eGameStatus GameStatus
        {
            get { return m_GameStatus; }
            set { m_GameStatus = value; }
        }

        public bool IsGameOver
        {
            get { return m_IsGameOver; }
            set { m_IsGameOver = value; }
        }

        public string LeadingPlayer
        {
            get { return m_LeadingPlayer; }
            set { m_LeadingPlayer = value; }
        }

        public Move CreateMove(int i_RowFrom, int i_ColumnFrom, int i_RowTo, int i_ColumnTo, bool i_IsEatingMove)
        {
            Move newMove;
            BoardPosition moveFromCheker = new BoardPosition();
            BoardPosition moveToCheker = new BoardPosition();

            moveFromCheker.Row = i_RowFrom;
            moveFromCheker.Column = i_ColumnFrom;
            moveToCheker.Row = i_RowTo;
            moveToCheker.Column = i_ColumnTo;

            newMove = new Move(moveFromCheker, moveToCheker, i_IsEatingMove);

            return newMove;
        }

        public void UpdateListsOfMoves(int i_NumberOfPlayer)
        {
            Player currentPlayer = GetCurrentPlayer(i_NumberOfPlayer);

            currentPlayer.PossiableMoves.Clear();
            currentPlayer.MustEatMoves.Clear();

            for (int i = 0; i < currentPlayer.PlayerPieces.Count; i++)
            {
                if (currentPlayer.PlayerPieces[i].IsPieceStillActive)
                {
                    int row = currentPlayer.PlayerPieces[i].Position.Row;
                    int column = currentPlayer.PlayerPieces[i].Position.Column;
                    Move possiableMove = null;

                    if ((currentPlayer.PlayerSign == 'O') || (currentPlayer.PlayerPieces[i].PieceType == Piece.ePieceType.King))
                    {
                        if (column >= 0 && column < (int)m_CheckerBoard.BoardSize - 1)
                        {
                            if (row < (int)m_CheckerBoard.BoardSize - 1)
                            {
                                // the checker is empty - this is a valid move
                                if (m_CheckerBoard.BoardMatrix[row + 1, column + 1].IsCheckerEmpty)
                                {
                                    possiableMove = CreateMove(row, column, row + 1, column + 1, !true);
                                    currentPlayer.PossiableMoves.Add(possiableMove);
                                }
                                else if (m_CheckerBoard.BoardMatrix[row, column].SignOfPlayerInChecker != m_CheckerBoard.BoardMatrix[row + 1, column + 1].SignOfPlayerInChecker)
                                {
                                    // check if it's possiable to eat the Men
                                    if (row < (int)m_CheckerBoard.BoardSize - 2 && column < (int)m_CheckerBoard.BoardSize - 2 && m_CheckerBoard.BoardMatrix[row + 2, column + 2].IsCheckerEmpty)
                                    {
                                        possiableMove = CreateMove(row, column, row + 2, column + 2, true);
                                        currentPlayer.MustEatMoves.Add(possiableMove);
                                    }
                                }
                            }
                        }

                        if (column <= (int)m_CheckerBoard.BoardSize - 1 && column > 0)
                        {
                            if (row < (int)m_CheckerBoard.BoardSize - 1)
                            {
                                if (m_CheckerBoard.BoardMatrix[row + 1, column - 1].IsCheckerEmpty)
                                {
                                    possiableMove = CreateMove(row, column, row + 1, column - 1, !true);
                                    currentPlayer.PossiableMoves.Add(possiableMove);
                                }
                                else if (m_CheckerBoard.BoardMatrix[row, column].SignOfPlayerInChecker != m_CheckerBoard.BoardMatrix[row + 1, column - 1].SignOfPlayerInChecker)
                                {
                                    // check if it's possiable to eat the Men
                                    if (row < (int)m_CheckerBoard.BoardSize - 2 && column > 1 && m_CheckerBoard.BoardMatrix[row + 2, column - 2].IsCheckerEmpty)
                                    {
                                        possiableMove = CreateMove(row, column, row + 2, column - 2, true);
                                        currentPlayer.MustEatMoves.Add(possiableMove);
                                    }
                                }
                            }
                        }
                    }

                    if ((currentPlayer.PlayerSign == 'X') || (currentPlayer.PlayerPieces[i].PieceType == Piece.ePieceType.King))
                    {
                        if (column >= 0 && column < (int)m_CheckerBoard.BoardSize - 1)
                        {
                            if (row > 0)
                            {
                                if (m_CheckerBoard.BoardMatrix[row - 1, column + 1].IsCheckerEmpty)
                                {
                                    possiableMove = CreateMove(row, column, row - 1, column + 1, !true);
                                    currentPlayer.PossiableMoves.Add(possiableMove);
                                }
                                else if (m_CheckerBoard.BoardMatrix[row, column].SignOfPlayerInChecker != m_CheckerBoard.BoardMatrix[row - 1, column + 1].SignOfPlayerInChecker)
                                {
                                    if (row > 1 && column < (int)m_CheckerBoard.BoardSize - 2 && m_CheckerBoard.BoardMatrix[row - 2, column + 2].IsCheckerEmpty)
                                    {
                                        possiableMove = CreateMove(row, column, row - 2, column + 2, true);
                                        currentPlayer.MustEatMoves.Add(possiableMove);
                                    }
                                }
                            }
                        }

                        if (column <= (int)m_CheckerBoard.BoardSize - 1 && column > 0)
                        {
                            if (row > 0)
                            {
                                if (m_CheckerBoard.BoardMatrix[row - 1, column - 1].IsCheckerEmpty)
                                {
                                    possiableMove = CreateMove(row, column, row - 1, column - 1, !true);
                                    currentPlayer.PossiableMoves.Add(possiableMove);
                                }
                                else if (m_CheckerBoard.BoardMatrix[row, column].SignOfPlayerInChecker != m_CheckerBoard.BoardMatrix[row - 1, column - 1].SignOfPlayerInChecker)
                                {
                                    if (row >= 2 && column >= 2 && m_CheckerBoard.BoardMatrix[row - 2, column - 2].IsCheckerEmpty)
                                    {
                                        possiableMove = CreateMove(row, column, row - 2, column - 2, true);
                                        currentPlayer.MustEatMoves.Add(possiableMove);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public Move CheckMoveValidity(int i_NumberOfPlayer, BoardPosition i_positionToMoveFrom, BoardPosition i_positionToMoveTo)
        {
            Player currentPlayer, otherPlayer = null;
            Move newMove = null;
            currentPlayer = GetCurrentPlayer(i_NumberOfPlayer);
            otherPlayer = GetCurrentPlayer((i_NumberOfPlayer % 2) + 1);

            bool isEatingMove = IsMoveAEatingMove(i_positionToMoveFrom, i_positionToMoveTo);

            if (isEatingMove != true && currentPlayer.MustEatMoves.Count != 0)
            {
                newMove = null;
            }
            else if (currentPlayer.CheckIfMoveIsOnList(new Move(i_positionToMoveFrom, i_positionToMoveTo, isEatingMove)))
            {
                newMove = new Move(i_positionToMoveFrom, i_positionToMoveTo, isEatingMove);
            }

            return newMove;
        }

        public void CheckIfMatchIsOver()
        {
            int numberOfPointToAddToTheWinner;
            int winnerNumber = 0; // no winner yet

            if (m_Player1.NumberOfActivePieces == 0 || m_Player2.NumberOfActivePieces == 0)
            {
                if (Player1.NumberOfActivePieces == 0)
                {
                    winnerNumber = m_Player2.PlayerNumber;
                }
                else
                {
                    winnerNumber = m_Player1.PlayerNumber;
                }

                m_IsMatchOver = true;
                m_GameStatus = eGameStatus.Win;
            }
            else if ((m_Player1.PossiableMoves.Count == 0 && m_Player1.MustEatMoves.Count == 0) || (m_Player2.PossiableMoves.Count == 0 && m_Player2.MustEatMoves.Count == 0))
            {
                m_IsMatchOver = true;

                if ((m_Player1.PossiableMoves.Count == 0 && m_Player1.MustEatMoves.Count == 0) && (m_Player2.PossiableMoves.Count == 0) && (m_Player2.MustEatMoves.Count == 0))
                {
                    m_GameStatus = eGameStatus.Tie;
                }
                else if (m_Player1.PossiableMoves.Count == 0 && m_Player1.MustEatMoves.Count == 0)
                {
                    m_GameStatus = eGameStatus.Win;
                    winnerNumber = m_Player2.PlayerNumber;
                }
                else
                {
                    m_GameStatus = eGameStatus.Win;
                    winnerNumber = m_Player1.PlayerNumber;
                }
            }

            if (m_GameStatus == eGameStatus.Win)
            {
                numberOfPointToAddToTheWinner = CalculateNumberOfPointsToAdd();
                if (winnerNumber == m_Player1.PlayerNumber)
                {
                    m_Player1.Score += numberOfPointToAddToTheWinner;
                    m_LeadingPlayer = m_Player1.Name;
                }
                else
                {
                    m_Player2.Score += numberOfPointToAddToTheWinner;
                    m_LeadingPlayer = m_Player2.Name;
                }
            }

            if (m_IsMatchOver == true)
            {
                m_NumberOfGames++;
            }
        }

        public int CalculateNumberOfPointsToAdd()
        {
            int pointsToAdd;

            pointsToAdd = Math.Abs(m_Player1.CalculatePointsForPlayer() - m_Player2.CalculatePointsForPlayer());

            return pointsToAdd;
        }

        public void StartNewGame()
        {
            m_IsMatchOver = !true;
            m_Player1.NumberOfActivePieces = (int)m_Player1.NumberOfPiecesForPlayer;
            m_Player2.NumberOfActivePieces = (int)m_Player2.NumberOfPiecesForPlayer;
            m_Player1.SetPiecesForStartPositions();
            m_Player2.SetPiecesForStartPositions();
            m_CheckerBoard.SetToStartPositionBoardMatrix();
            UpdateListsOfMoves(m_Player1.PlayerNumber);
            UpdateListsOfMoves(m_Player2.PlayerNumber);
            m_GameStatus = eGameStatus.Running;
            m_LeadingPlayer = "No Leading Player Yet";
        }

        public Player GetCurrentPlayer(int i_NumberOfPlayer)
        {
            Player currentPlayer;

            if (i_NumberOfPlayer == 1)
            {
                currentPlayer = m_Player1;
            }
            else
            {
                currentPlayer = m_Player2;
            }

            return currentPlayer;
        }

        public bool IsMoveAEatingMove(BoardPosition i_FromPosition, BoardPosition i_ToPosition)
        {
            bool isEatingMove = !true;

            isEatingMove = Math.Abs(i_ToPosition.Row - i_FromPosition.Row) == 2 && Math.Abs(i_ToPosition.Column - i_FromPosition.Column) == 2;

            return isEatingMove;
        }

        public bool ManageComputerTurn(ref BoardPosition io_EatenPiecePosition, ref Move io_ComputerMove)
        {
            Random randObj = new Random();
            int randomIndexForMove;
            bool isTurnOver = !true;
            bool isMoveMakeMenAKing = !true;
            int indexOfthePieceToMove;

            if (m_Player2.MustEatMoves.Count != 0)
            {
                randomIndexForMove = randObj.Next(m_Player2.MustEatMoves.Count);
                io_ComputerMove = new Move(m_Player2.MustEatMoves[randomIndexForMove].FromChecker, m_Player2.MustEatMoves[randomIndexForMove].ToChecker, m_Player2.MustEatMoves[randomIndexForMove].EatingMove);
                io_EatenPiecePosition = new BoardPosition();
                io_EatenPiecePosition = ManageEatenMove(m_Player2.PlayerNumber, io_ComputerMove);
            }
            else
            {
                randomIndexForMove = randObj.Next(m_Player2.PossiableMoves.Count);
                io_ComputerMove = new Move(m_Player2.PossiableMoves[randomIndexForMove].FromChecker, m_Player2.PossiableMoves[randomIndexForMove].ToChecker, m_Player2.PossiableMoves[randomIndexForMove].EatingMove);
            }

            indexOfthePieceToMove = m_Player2.FindIndexOfPieceOnTheListOfPieces(io_ComputerMove.FromChecker);
            isMoveMakeMenAKing = io_ComputerMove.CheckifMoveMakeMenAKing((int)m_CheckerBoard.BoardSize);

            if (isMoveMakeMenAKing == true)
            {
                ManageMoveThatMakesMenAKing(m_Player2.PlayerNumber, indexOfthePieceToMove);
            }

            UpdateGameAtEndOfTurn(m_Player2.PlayerNumber, io_ComputerMove, indexOfthePieceToMove);
            isTurnOver = IsTurnOver(m_Player2.PlayerNumber, io_ComputerMove);

            return isTurnOver;
        }

        public BoardPosition ManageEatenMove(int i_PlayerTurn, Move i_PlayerMove)
        {
            int indexOfPiece;
            Player otherPlayer = GetCurrentPlayer((i_PlayerTurn % 2) + 1);
            BoardPosition positionOfEatenPiece = m_CheckerBoard.FindPositionOfTheEatenPiece(i_PlayerMove);

            indexOfPiece = otherPlayer.FindIndexOfPieceOnTheListOfPieces(positionOfEatenPiece);
            otherPlayer.PlayerPieces[indexOfPiece].IsPieceStillActive = !true;
            otherPlayer.PlayerPieces[indexOfPiece].Position.Row = 0; // checker (0,0) is Black. there can never be a piece there
            otherPlayer.PlayerPieces[indexOfPiece].Position.Column = 0;
            otherPlayer.NumberOfActivePieces--;

            return positionOfEatenPiece;
        }

        public void ManageMoveThatMakesMenAKing(int i_PlayerTurn, int i_IndexOfthePieceToMove)
        {
            Player currentPlayer = GetCurrentPlayer(i_PlayerTurn);

            currentPlayer.PlayerPieces[i_IndexOfthePieceToMove].PieceType = Piece.ePieceType.King;

            if (currentPlayer.PlayerSign == 'O')
            {
                currentPlayer.PlayerPieces[i_IndexOfthePieceToMove].PieceSign = 'U';
            }
            else
            {
                currentPlayer.PlayerPieces[i_IndexOfthePieceToMove].PieceSign = 'K';
            }
        }

        public void UpdateGameAtEndOfTurn(int i_NumberOfCurrentPlayer, Move i_PlayerMove, int i_IndexOfthePieceToMove)
        {
            Player currentPlayer = GetCurrentPlayer(i_NumberOfCurrentPlayer);
            Player otherPlayer = GetCurrentPlayer((i_NumberOfCurrentPlayer % 2) + 1);

            currentPlayer.PlayerPieces[i_IndexOfthePieceToMove].Position = i_PlayerMove.ToChecker;
            m_CheckerBoard.UpdateBoard(i_PlayerMove, currentPlayer.PlayerPieces[i_IndexOfthePieceToMove].PieceSign);
            UpdateListsOfMoves(currentPlayer.PlayerNumber);
            UpdateListsOfMoves(otherPlayer.PlayerNumber);
        }

        public bool IsTurnOver(int i_NumberOfCurrentPlayer, Move i_PlayerMove)
        {
            bool isTurnOver = !true;
            Player currentPlayer = GetCurrentPlayer(i_NumberOfCurrentPlayer);

            // means this player turn is over because he doesnt have a move that he can eat an opponent piece
            if ((currentPlayer.IsMoveOnMustEatMovesList(i_PlayerMove.ToChecker) && i_PlayerMove.EatingMove) == true)
            {
                isTurnOver = !true;
            }
            else
            {
                isTurnOver = true;
            }

            return isTurnOver;
        }
    }
}