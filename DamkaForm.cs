using System;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class DamkaForm : Form
    {
        private const int k_CheckerSize = 60;
        private const int k_DistanceFromLeftLine = 30;
        private const int k_WidthOfMaxLenght = 205;
        private const int k_DistanceBetweenLabelsForSmallBoardSize = 15;
        private const int k_DistanceBetweenLabelsForMadiumBoardSize = 25;
        private const int k_DistanceBetweenLabelsForLargeBoardSize = 35;
        private Label player1Score;
        private Label player2Score;
        private Button startNewGame;
        private CheckerButton[,] checkerButtonMatrix;
        private Game m_CheckerGame;
        private BoardPosition m_FromChecker = null;
        private BoardPosition m_ToChecker = null;
        private Label playerTurnLabel;
        private PictureBox pieceOfCurrentPlayerPictureBox;
        private int m_PlayerTurn;
        private bool m_InvalidMove;

        public DamkaForm(Game i_DamkaGame)
        {
            this.Name = "DamkaForm";
            this.Text = "Damka";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackgroundImage = CheckersGame.Properties.Resources.whiteBackground;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            m_CheckerGame = i_DamkaGame;
            m_InvalidMove = !true;
            checkerButtonMatrix = new CheckerButton[(int)m_CheckerGame.CheckerBoard.BoardSize, (int)m_CheckerGame.CheckerBoard.BoardSize];
            m_PlayerTurn = 1; //// default: player 1 start
            initializeComponent();
            setBoardGame();
        }

        private void initializeComponent()
        {
            player1Score = new System.Windows.Forms.Label();
            player2Score = new System.Windows.Forms.Label();
            startNewGame = new System.Windows.Forms.Button();
            playerTurnLabel = new System.Windows.Forms.Label();
            pieceOfCurrentPlayerPictureBox = new System.Windows.Forms.PictureBox();

            // Player1Score 
            player1Score.AutoSize = true;
            player1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            player1Score.Location = new System.Drawing.Point(k_DistanceFromLeftLine, 25);
            player1Score.Name = "Player1Score";
            player1Score.Size = new System.Drawing.Size(110, 25);
            player1Score.TabIndex = 0;
            player1Score.BackColor = Color.Transparent;
            player1Score.Text = createPlayerLableString(m_CheckerGame.Player1);

            // Player2Score
            player2Score.AutoSize = true;
            player2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            if (m_CheckerGame.CheckerBoard.BoardSize == Board.ePossibleBoardSizes.SmallSize)
            {
                player2Score.Location = new System.Drawing.Point(k_DistanceFromLeftLine + k_WidthOfMaxLenght + k_DistanceBetweenLabelsForSmallBoardSize, 25);
            }
            else if (m_CheckerGame.CheckerBoard.BoardSize == Board.ePossibleBoardSizes.Medium)
            {
                player2Score.Location = new System.Drawing.Point(k_DistanceFromLeftLine + k_WidthOfMaxLenght + k_DistanceBetweenLabelsForMadiumBoardSize, 25);
            }
            else
            {
                player2Score.Location = new System.Drawing.Point(k_DistanceFromLeftLine + k_WidthOfMaxLenght + k_DistanceBetweenLabelsForLargeBoardSize, 25);
            }

            player2Score.Name = "Player2Score";
            player2Score.Size = new System.Drawing.Size(110, 25);
            player2Score.TabIndex = 1;
            player2Score.BackColor = Color.Transparent;
            player2Score.Text = createPlayerLableString(m_CheckerGame.Player2);
            
            // StartNewGame
            startNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            if (m_CheckerGame.CheckerBoard.BoardSize == Board.ePossibleBoardSizes.SmallSize)
            {
                startNewGame.Location = new System.Drawing.Point(k_DistanceFromLeftLine + ((k_WidthOfMaxLenght + k_DistanceBetweenLabelsForSmallBoardSize) * 2), 18);
            }
            else if (m_CheckerGame.CheckerBoard.BoardSize == Board.ePossibleBoardSizes.Medium)
            {
                startNewGame.Location = new System.Drawing.Point(k_DistanceFromLeftLine + ((k_WidthOfMaxLenght + k_DistanceBetweenLabelsForMadiumBoardSize) * 2), 18);
            }
            else
            {
                startNewGame.Location = new System.Drawing.Point(k_DistanceFromLeftLine + ((k_WidthOfMaxLenght + k_DistanceBetweenLabelsForLargeBoardSize) * 2), 18);
            }

            startNewGame.Name = "StartNewGame";
            startNewGame.Size = new System.Drawing.Size(142, 42);
            startNewGame.TabIndex = 2;
            startNewGame.Text = "Start New Game";
            startNewGame.UseVisualStyleBackColor = true;
            startNewGame.Click += new EventHandler(startNewGame_Click);
            startNewGame.BackColor = Color.Coral;
            
            // PlayerTurnLabel
            playerTurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            playerTurnLabel.Location = new System.Drawing.Point((int)m_CheckerGame.CheckerBoard.BoardSize / 2 * 60, 60);
            playerTurnLabel.Name = "PlayerTurnLabel";
            playerTurnLabel.AutoSize = true;
            playerTurnLabel.TabIndex = 0;
            setPlayerTurnLabel();
            playerTurnLabel.BackColor = Color.Transparent;

            // PieceOfCurrentPlayerPictureBox
            pieceOfCurrentPlayerPictureBox.Enabled = !true;
            pieceOfCurrentPlayerPictureBox.Image = CheckersGame.Properties.Resources.smallBlackSoldier;
            pieceOfCurrentPlayerPictureBox.Location = new System.Drawing.Point(((int)m_CheckerGame.CheckerBoard.BoardSize / 2 * 60) - 60, 60);
            pieceOfCurrentPlayerPictureBox.BackColor = Color.Transparent;
            pieceOfCurrentPlayerPictureBox.Size = new System.Drawing.Size(30, 30);

            // DamkaForm 
            Controls.Add(this.startNewGame);
            Controls.Add(this.player2Score);
            Controls.Add(this.player1Score);
            Controls.Add(this.playerTurnLabel);
            Controls.Add(this.pieceOfCurrentPlayerPictureBox);
        }

        public string Player1Score
        {
            get { return player1Score.Text; }
            set { player1Score.Text = value; }
        }

        public string Player2Score
        {
            get { return player2Score.Text; }
            set { player2Score.Text = value; }
        }

        private void setBoardGame()
        {
            int formHeight = ((int)m_CheckerGame.CheckerBoard.BoardSize * k_CheckerSize) + 190;
            int formWidth;
            int boardSize = (int)m_CheckerGame.CheckerBoard.BoardSize;

            if (((boardSize * k_CheckerSize) + 77) > startNewGame.Right + 30)
            {
                formWidth = (boardSize * k_CheckerSize) + 77;
            }
            else
            {
                formWidth = startNewGame.Right + 30;
            }

            int distanceFromLeftLine = k_DistanceFromLeftLine;
            int distanceFromTop = 100;
            this.Size = new System.Drawing.Size(formWidth, formHeight);

            for (int i = 0; i < (int)m_CheckerGame.CheckerBoard.BoardSize; i++)
            {
                for (int j = 0; j < (int)m_CheckerGame.CheckerBoard.BoardSize; j++)
                {
                    initializeCheckerInForm(ref distanceFromLeftLine, distanceFromTop, i, j);
                }

                distanceFromLeftLine = k_DistanceFromLeftLine;
                distanceFromTop += k_CheckerSize;
            }
        }

        private void initializeCheckerInForm(ref int distanceFromLeftLine, int distanceFromTop, int i_Row, int i_Column)
        {
            BoardPosition currentBoardPosition;

            currentBoardPosition = new BoardPosition();
            currentBoardPosition.Row = i_Row;
            currentBoardPosition.Column = i_Column;
            checkerButtonMatrix[i_Row, i_Column] = new CheckerButton(currentBoardPosition);
            checkerButtonMatrix[i_Row, i_Column].Location = new Point(distanceFromLeftLine, distanceFromTop);
            checkerButtonMatrix[i_Row, i_Column].Size = new Size(k_CheckerSize, k_CheckerSize);

            setCheckerInForm(i_Row, i_Column);

            distanceFromLeftLine += k_CheckerSize;
            checkerButtonMatrix[i_Row, i_Column].Click += new EventHandler(this.checkerButton_Click);
            Controls.Add(checkerButtonMatrix[i_Row, i_Column]);
        }

        private void startNewGame_Click(object sender, EventArgs e)
        {
            newGame(!true);
        }

        private void checkerButton_Click(object sender, EventArgs e)
        {
            CheckerButton pressedChecker = sender as CheckerButton;
            bool isTurnOver;

            if (m_FromChecker == null)
            {
                m_FromChecker = new BoardPosition();
                m_FromChecker.Row = pressedChecker.CheckerButtonBoardPosition.Row;
                m_FromChecker.Column = pressedChecker.CheckerButtonBoardPosition.Column;
                pressedChecker.BackgroundImage = CheckersGame.Properties.Resources.whiteBlueBackgroundpng;
                pressedChecker.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (m_FromChecker.CompareBoardPositions(pressedChecker.CheckerButtonBoardPosition))
            {
                m_FromChecker = null;
                pressedChecker.BackgroundImage = CheckersGame.Properties.Resources.whiteBackground;
            }
            else
            {
                m_ToChecker = new BoardPosition();
                m_ToChecker.Row = pressedChecker.CheckerButtonBoardPosition.Row;
                m_ToChecker.Column = pressedChecker.CheckerButtonBoardPosition.Column;
                checkerButtonMatrix[m_FromChecker.Row, m_FromChecker.Column].BackgroundImage = CheckersGame.Properties.Resources.whiteBackground;
                isTurnOver = manageTurn();
                if (!isTurnOver && m_InvalidMove)
                {
                    m_FromChecker = null;
                    m_ToChecker = null;
                    m_InvalidMove = !true;
                }
                else if (!isTurnOver) 
                {
                    //// the move was valid - there is another eating move
                    m_FromChecker = m_ToChecker;
                    m_ToChecker = null;
                }
                else
                {
                    pressedChecker.BackColor = Color.White;
                    checkerButtonMatrix[m_FromChecker.Row, m_FromChecker.Column].BackColor = Color.White;
                    m_FromChecker = null;
                    m_ToChecker = null;
                    m_PlayerTurn = (m_PlayerTurn % 2) + 1; // switch players
                    setPlayerTurnLabel();

                    if (m_CheckerGame.IsSecondPlayerComputer == true)
                    {
                        manageTurn();
                        m_PlayerTurn = (m_PlayerTurn % 2) + 1; // switch players
                        setPlayerTurnLabel();
                    }
                }
            }
        }

        private bool manageTurn()
        {
            bool isTurnOver = !true;
            string gameStatusMsg;

            if (m_PlayerTurn == 1 || m_CheckerGame.IsSecondPlayerComputer == !true)
            {
                isTurnOver = manageHumanTurn(m_FromChecker, m_ToChecker);
            }
            else
            {
                BoardPosition positionOfTheEatenpiece = null;
                while (!isTurnOver)
                {
                    Move computerMove = null;
                    isTurnOver = m_CheckerGame.ManageComputerTurn(ref positionOfTheEatenpiece, ref computerMove);

                    if (computerMove.CheckifMoveMakeMenAKing((int)m_CheckerGame.CheckerBoard.BoardSize))
                    {
                        playSound(CheckersGame.Properties.Resources.KingSound);
                    }
                    else
                    {
                        playSound(CheckersGame.Properties.Resources.woodKnockSound);
                    }

                    m_FromChecker = new BoardPosition();
                    m_ToChecker = new BoardPosition();
                    m_FromChecker.Row = computerMove.FromChecker.Row;
                    m_FromChecker.Column = computerMove.FromChecker.Column;
                    m_ToChecker.Row = computerMove.ToChecker.Row;
                    m_ToChecker.Column = computerMove.ToChecker.Column;
                    updateFormAtEndOfMove(positionOfTheEatenpiece);
                    m_FromChecker = null;
                    m_ToChecker = null;
                }
            }

            m_CheckerGame.CheckIfMatchIsOver();

            if (m_CheckerGame.IsMatchOver == true)
            {
                DialogResult result;
                if (m_CheckerGame.GameStatus == Game.eGameStatus.Tie)
                {
                    gameStatusMsg = string.Format(@"it's  a Tie!
                    Another Round?");
                }
                else
                {
                    gameStatusMsg = string.Format(
                    @"{0} WON!! GOOD JOB!
                    Another Round?",
                        m_CheckerGame.LeadingPlayer);
                }

                result = MessageBox.Show(gameStatusMsg, "Game Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    newGame(true);
                }
                else
                {
                    this.Close();
                }
            }

            return isTurnOver;
        }

        private bool manageHumanTurn(BoardPosition i_FromChecker, BoardPosition i_ToChecker)
        {
            bool isTurnOver = !true;
            bool isMoveMakeMenAKing = !true;
            Player currentPlayer, otherPlayer;
            Move playerMove = null;
            int indexOfthePieceToMove;
            BoardPosition positionOfEatenPiece = null;

            currentPlayer = m_CheckerGame.GetCurrentPlayer(m_PlayerTurn);
            otherPlayer = m_CheckerGame.GetCurrentPlayer((m_PlayerTurn % 2) + 1);

            playerMove = m_CheckerGame.CheckMoveValidity(currentPlayer.PlayerNumber, i_FromChecker, i_ToChecker);

            if (playerMove == null)
            {
                playSound(CheckersGame.Properties.Resources.InvalidMoveSound);
                MessageBox.Show(@"Invalid Move! please try again", "Invalid Move", MessageBoxButtons.OK);
                m_InvalidMove = true;
            }
            else
            {
                m_InvalidMove = !true;
                indexOfthePieceToMove = currentPlayer.FindIndexOfPieceOnTheListOfPieces(playerMove.FromChecker);

                if (playerMove.EatingMove == true)
                {
                    positionOfEatenPiece = new BoardPosition();
                    positionOfEatenPiece = m_CheckerGame.ManageEatenMove(m_PlayerTurn, playerMove);
                }

                isMoveMakeMenAKing = playerMove.CheckifMoveMakeMenAKing((int)m_CheckerGame.CheckerBoard.BoardSize);

                if (isMoveMakeMenAKing == true)
                {
                    m_CheckerGame.ManageMoveThatMakesMenAKing(m_PlayerTurn, indexOfthePieceToMove);
                    playSound(CheckersGame.Properties.Resources.KingSound);
                }
                else
                {
                    playSound(CheckersGame.Properties.Resources.woodKnockSound);
                }

                m_CheckerGame.UpdateGameAtEndOfTurn(m_PlayerTurn, playerMove, indexOfthePieceToMove);
                updateFormAtEndOfMove(positionOfEatenPiece);
                isTurnOver = m_CheckerGame.IsTurnOver(m_PlayerTurn, playerMove);
            }

            return isTurnOver;
        }

        private void newGame(bool i_GameStatusShowed)
        {
            int player1StatusOfPoints, player2StatusOfPoints;
            string leadingPlayer = string.Empty;
            string gameStatusMsg;

            if (!i_GameStatusShowed)
            {
                player1StatusOfPoints = m_CheckerGame.Player1.CalculatePointsForPlayer();
                player2StatusOfPoints = m_CheckerGame.Player2.CalculatePointsForPlayer();

                if (player1StatusOfPoints > player2StatusOfPoints)
                {
                    leadingPlayer = m_CheckerGame.Player1.Name;
                    m_CheckerGame.Player1.Score += m_CheckerGame.CalculateNumberOfPointsToAdd();
                }
                else if (player2StatusOfPoints > player1StatusOfPoints)
                {
                    leadingPlayer = m_CheckerGame.Player2.Name;
                    m_CheckerGame.Player2.Score += m_CheckerGame.CalculateNumberOfPointsToAdd();
                }

                if (leadingPlayer != string.Empty)
                {
                    m_CheckerGame.LeadingPlayer = leadingPlayer;
                    gameStatusMsg = string.Format(@"{0} WON!! GOOD JOB! ", leadingPlayer);
                    MessageBox.Show(gameStatusMsg);
                }
                else
                {
                    MessageBox.Show("it's a Tie!");
                }
            }
            
            m_CheckerGame.StartNewGame();
            setAllCheckersInForm();

            player1Score.Text = createPlayerLableString(m_CheckerGame.Player1);
            player2Score.Text = createPlayerLableString(m_CheckerGame.Player2);
            setPlayerTurnLabel();
            m_CheckerGame.NumberOfGames++;
        }

        private string createPlayerLableString(Player i_Player)
        {
            string playerScoreLabel = string.Format(@"{0}: {1}", i_Player.Name, i_Player.Score);

            return playerScoreLabel;
        }

        private void setCheckerInForm(int i_Row, int i_Coloumn)
        {
            if (m_CheckerGame.CheckerBoard.BoardMatrix[i_Row, i_Coloumn].Color == Checker.eCheckerColor.White)
            {
                checkerButtonMatrix[i_Row, i_Coloumn].BackgroundImage = CheckersGame.Properties.Resources.whiteBackground;
                if (m_CheckerGame.CheckerBoard.BoardMatrix[i_Row, i_Coloumn].SignOfPlayerInChecker == Checker.eSignOfPlayerInChecker.X)
                {
                    checkerButtonMatrix[i_Row, i_Coloumn].Image = CheckersGame.Properties.Resources.whiteSoldier;
                }
                else if (m_CheckerGame.CheckerBoard.BoardMatrix[i_Row, i_Coloumn].SignOfPlayerInChecker == Checker.eSignOfPlayerInChecker.O)
                {
                    checkerButtonMatrix[i_Row, i_Coloumn].Image = CheckersGame.Properties.Resources.blackSoldier2;
                }
                else
                {
                    checkerButtonMatrix[i_Row, i_Coloumn].Image = null;
                }
            }
            else
            {
                checkerButtonMatrix[i_Row, i_Coloumn].BackgroundImage = CheckersGame.Properties.Resources.blackCheckerBackground;
                checkerButtonMatrix[i_Row, i_Coloumn].Enabled = !true;
            }

            if (checkerButtonMatrix[i_Row, i_Coloumn].Image != null)
            {
                checkerButtonMatrix[i_Row, i_Coloumn].ImageAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void setAllCheckersInForm()
        {
            for (int i = 0; i < (int)m_CheckerGame.CheckerBoard.BoardSize; i++)
            {
                for (int j = 0; j < (int)m_CheckerGame.CheckerBoard.BoardSize; j++)
                {
                    setCheckerInForm(i, j);
                }
            }
        }

        private void updateFormAtEndOfMove(BoardPosition i_PositionOfEatenPiece)
        {
            if ((m_ToChecker.Row == 0) || (m_ToChecker.Row == (int)m_CheckerGame.CheckerBoard.BoardSize - 1))
            {
                if (m_CheckerGame.CheckerBoard.BoardMatrix[m_ToChecker.Row, m_ToChecker.Column].SignOfPlayerInChecker == Checker.eSignOfPlayerInChecker.O)
                {
                    checkerButtonMatrix[m_ToChecker.Row, m_ToChecker.Column].Image = CheckersGame.Properties.Resources.BlackCrown;
                }
                else
                {
                    checkerButtonMatrix[m_ToChecker.Row, m_ToChecker.Column].Image = CheckersGame.Properties.Resources.WhiteCrown;
                }
            }
            else
            {
                checkerButtonMatrix[m_ToChecker.Row, m_ToChecker.Column].Image = checkerButtonMatrix[m_FromChecker.Row, m_FromChecker.Column].Image;
            }

            checkerButtonMatrix[m_FromChecker.Row, m_FromChecker.Column].Image = null;

            if (i_PositionOfEatenPiece != null)
            {
                checkerButtonMatrix[i_PositionOfEatenPiece.Row, i_PositionOfEatenPiece.Column].Image = null;
            }

            checkerButtonMatrix[m_ToChecker.Row, m_ToChecker.Column].ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void playSound(System.IO.Stream i_SoundName)
        {
            SoundPlayer playSound = new SoundPlayer(i_SoundName);
            playSound.Play();
        }

        private void setPlayerTurnLabel()
        {
            playerTurnLabel.Text = m_CheckerGame.GetCurrentPlayer(m_PlayerTurn).Name + "'s turn";
            if (m_PlayerTurn == 1)
            {
                pieceOfCurrentPlayerPictureBox.Image = CheckersGame.Properties.Resources.smallBlackSoldier;
            }
            else
            {
                pieceOfCurrentPlayerPictureBox.Image = CheckersGame.Properties.Resources.smallWhiteSoldier;
            }

            playerTurnLabel.TextAlign = ContentAlignment.MiddleLeft;
            playerTurnLabel.ImageAlign = ContentAlignment.MiddleLeft;
        }
    }
}