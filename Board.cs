using System;

using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Board
    {
        private readonly Checker[,] r_BoardMatrix;
        private readonly ePossibleBoardSizes r_BoardSize; 

        public enum ePossibleBoardSizes
        {
            SmallSize = 6,
            Medium = 8,
            Large = 10
        }

        public Board(int i_ChoiseForBoardSize)
        {
            r_BoardSize = (ePossibleBoardSizes)i_ChoiseForBoardSize;

            r_BoardMatrix = new Checker[(int)r_BoardSize, (int)r_BoardSize];
        }

        public ePossibleBoardSizes BoardSize
        {
            get { return r_BoardSize; }
        }

        public Checker[,] BoardMatrix
        {
            get { return r_BoardMatrix; }
        }

        public void CreatAndInitializeBoardMatrix()
        {
            for (int i = 0; i < (int)r_BoardSize; i++)
            {
                for (int j = 0; j < (int)r_BoardSize; j++)
                {
                    r_BoardMatrix[i, j] = new Checker();
                    r_BoardMatrix[i, j].CheckerBoardPosition = new BoardPosition();
                    r_BoardMatrix[i, j].CheckerBoardPosition.Row = i;
                    r_BoardMatrix[i, j].CheckerBoardPosition.Column = j;
                }
            }

            SetToStartPositionBoardMatrix();
        }

        public void SetToStartPositionBoardMatrix()
        {
            int numberOfLinesForEachTeam = ((int)r_BoardSize - 2) / 2;
            Checker.eCheckerColor CheckerColor = Checker.eCheckerColor.Black;
            bool isCheckerEmpty = true;
            char signInChecker = ' ';
            Checker.eSignOfPlayerInChecker playerInChecker = Checker.eSignOfPlayerInChecker.NoPlayer;

            for (int i = 0; i < (int)r_BoardSize; i++)
            {
                for (int j = 0; j < (int)r_BoardSize; j++)
                {
                    if (((i % 2 == 0) && (j % 2 == 0)) || ((i % 2 != 0) && (j % 2 != 0)))
                    {
                        CheckerColor = Checker.eCheckerColor.Black; // initializing all white checkers
                        isCheckerEmpty = !true;
                        signInChecker = ' ';
                        playerInChecker = Checker.eSignOfPlayerInChecker.NoPlayer;
                    }

                    if (i < numberOfLinesForEachTeam)
                    {
                        if (((i % 2 == 0) && (j % 2 != 0)) || ((i % 2 != 0) && (j % 2 == 0)))
                        {
                            CheckerColor = Checker.eCheckerColor.White;
                            isCheckerEmpty = true;
                            signInChecker = 'O';
                            playerInChecker = Checker.eSignOfPlayerInChecker.O;
                        }
                    }
                    else if (i == numberOfLinesForEachTeam || i == numberOfLinesForEachTeam + 1)
                    {
                        if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                        {
                            CheckerColor = Checker.eCheckerColor.White;
                            isCheckerEmpty = true;
                            signInChecker = ' ';
                            playerInChecker = Checker.eSignOfPlayerInChecker.NoPlayer;
                        }
                    }
                    else
                    {
                        if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                        {
                            CheckerColor = Checker.eCheckerColor.White;
                            isCheckerEmpty = true;
                            signInChecker = 'X';
                            playerInChecker = Checker.eSignOfPlayerInChecker.X;
                        }
                    }

                    r_BoardMatrix[i, j].Color = CheckerColor;
                    r_BoardMatrix[i, j].IsCheckerEmpty = isCheckerEmpty;
                    r_BoardMatrix[i, j].SignOfPieceInChecker = signInChecker;
                    r_BoardMatrix[i, j].SignOfPlayerInChecker = playerInChecker;
                }
            }
        }

        public void UpdateBoard(Move i_MoveToUpdate, char i_NewSignForChecker)
        {
            Checker.eSignOfPlayerInChecker newSignOfPlayerInChecker;

            r_BoardMatrix[i_MoveToUpdate.FromChecker.Row, i_MoveToUpdate.FromChecker.Column].IsCheckerEmpty = true;
            r_BoardMatrix[i_MoveToUpdate.FromChecker.Row, i_MoveToUpdate.FromChecker.Column].SignOfPieceInChecker = ' ';
            r_BoardMatrix[i_MoveToUpdate.FromChecker.Row, i_MoveToUpdate.FromChecker.Column].SignOfPlayerInChecker = Checker.eSignOfPlayerInChecker.NoPlayer;
            r_BoardMatrix[i_MoveToUpdate.ToChecker.Row, i_MoveToUpdate.ToChecker.Column].IsCheckerEmpty = !true;
            r_BoardMatrix[i_MoveToUpdate.ToChecker.Row, i_MoveToUpdate.ToChecker.Column].SignOfPieceInChecker = i_NewSignForChecker;

            if (i_NewSignForChecker == 'X' || i_NewSignForChecker == 'K')
            {
                newSignOfPlayerInChecker = Checker.eSignOfPlayerInChecker.X;
            }
            else
            {
                newSignOfPlayerInChecker = Checker.eSignOfPlayerInChecker.O;
            }

            r_BoardMatrix[i_MoveToUpdate.ToChecker.Row, i_MoveToUpdate.ToChecker.Column].SignOfPlayerInChecker = newSignOfPlayerInChecker;

            if (i_MoveToUpdate.EatingMove == true)
            {
                BoardPosition positionToUpdate;
                positionToUpdate = FindPositionOfTheEatenPiece(i_MoveToUpdate);
                r_BoardMatrix[positionToUpdate.Row, positionToUpdate.Column].SignOfPieceInChecker = ' ';
                r_BoardMatrix[positionToUpdate.Row, positionToUpdate.Column].IsCheckerEmpty = true;
                r_BoardMatrix[positionToUpdate.Row, positionToUpdate.Column].SignOfPlayerInChecker = Checker.eSignOfPlayerInChecker.NoPlayer;
            }
        }

        public BoardPosition FindPositionOfTheEatenPiece(Move i_PlayerMove)
        {
            int columnForPositionToUpdate, rowForPositionToUpdate;
            BoardPosition positionToUpdate;

            if (i_PlayerMove.FromChecker.Row == i_PlayerMove.ToChecker.Row - 2)
            {
                rowForPositionToUpdate = i_PlayerMove.FromChecker.Row + 1;
            }
            else
            {
                rowForPositionToUpdate = i_PlayerMove.FromChecker.Row - 1;
            }

            if (i_PlayerMove.FromChecker.Column == i_PlayerMove.ToChecker.Column - 2)
            {
                columnForPositionToUpdate = i_PlayerMove.FromChecker.Column + 1;
            }
            else
            {
                columnForPositionToUpdate = i_PlayerMove.FromChecker.Column - 1;
            }

            positionToUpdate = new BoardPosition();
            positionToUpdate.Column = columnForPositionToUpdate;
            positionToUpdate.Row = rowForPositionToUpdate;

            return positionToUpdate;
        }
    }
}