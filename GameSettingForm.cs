using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class GameSettingForm : Form
    {
        private const int k_SmallBoardSize = 6;
        private const int k_MediumBoardSize = 8;
        private const int k_LargeBoardSize = 10;
        private const int k_MaxiumLengthOfPlayerName = 20;
        private Label boardSizeLabel;
        private Label playersLabel;
        private Label player1Label;
        private RadioButton smallBoardRadioButton;
        private RadioButton mediumBoardRadioButton;
        private RadioButton largeBoardRadioButton;
        private TextBox player1TextBox;
        private TextBox player2TextBox;
        private CheckBox player2CheckBox;
        private Button doneButton;

        public GameSettingForm()
        {
            this.Text = "GameSetting";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(350, 300);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            initGameSettingFormControls();
        }

        public int GetBoardSize()
        {
            int boardSize;

            if (smallBoardRadioButton.Checked)
            {
                boardSize = k_SmallBoardSize;
            }
            else if (mediumBoardRadioButton.Checked)
            {
                boardSize = k_MediumBoardSize;
            }
            else 
            {
                // also default value
                boardSize = k_LargeBoardSize;
            }

            return boardSize;
        }

        public bool IsSecondPlayerComputer()
        {
            return !player2CheckBox.Checked;
        }

        public string Player1Name
        {
            get { return player1TextBox.Text; }
            set { player1TextBox.Text = value; }
        }

        public string Player2Name
        {
            get { return player2TextBox.Text; }
            set { player2TextBox.Text = value; }
        }

        private void initGameSettingFormControls()
        {
            boardSizeLabel = new Label();
            playersLabel = new Label();
            player1Label = new Label();
            smallBoardRadioButton = new RadioButton();
            mediumBoardRadioButton = new RadioButton();
            largeBoardRadioButton = new RadioButton();
            player1TextBox = new TextBox();
            player2TextBox = new TextBox();
            player2CheckBox = new CheckBox();
            doneButton = new Button(); 

            // m_DoneButton
            this.doneButton.Location = new System.Drawing.Point(268, 237);
            this.doneButton.Name = "m_DoneButton";
            this.doneButton.Size = new System.Drawing.Size(126, 34);
            this.doneButton.TabIndex = 0;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);

            // m_BoardSizeLabel
            this.boardSizeLabel.AutoSize = true;
            this.boardSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.boardSizeLabel.Location = new System.Drawing.Point(23, 9);
            this.boardSizeLabel.Name = "m_BoardSizeLabel";
            this.boardSizeLabel.Size = new System.Drawing.Size(106, 24);
            this.boardSizeLabel.TabIndex = 1;
            this.boardSizeLabel.Text = "Board Size:";

            ////  m_SmallBoard
            this.smallBoardRadioButton.AutoSize = true;
            this.smallBoardRadioButton.Location = new System.Drawing.Point(39, 46);
            this.smallBoardRadioButton.Name = "m_SmallBoard";
            this.smallBoardRadioButton.Size = new System.Drawing.Size(56, 24);
            this.smallBoardRadioButton.TabIndex = 2;
            this.smallBoardRadioButton.TabStop = true;
            this.smallBoardRadioButton.Text = "6x6";
            this.smallBoardRadioButton.UseVisualStyleBackColor = true;

            // m_MediumBoard
            this.mediumBoardRadioButton.AutoSize = true;
            this.mediumBoardRadioButton.Location = new System.Drawing.Point(157, 46);
            this.mediumBoardRadioButton.Name = "m_MediumBoard";
            this.mediumBoardRadioButton.Size = new System.Drawing.Size(56, 24);
            this.mediumBoardRadioButton.TabIndex = 3;
            this.mediumBoardRadioButton.TabStop = true;
            this.mediumBoardRadioButton.Text = "8x8";
            this.mediumBoardRadioButton.UseVisualStyleBackColor = true;

            ////  m_LargeBoard
            this.largeBoardRadioButton.AutoSize = true;
            this.largeBoardRadioButton.Location = new System.Drawing.Point(268, 46);
            this.largeBoardRadioButton.Name = "m_LargeBoard";
            this.largeBoardRadioButton.Size = new System.Drawing.Size(74, 24);
            this.largeBoardRadioButton.TabIndex = 4;
            this.largeBoardRadioButton.TabStop = true;
            this.largeBoardRadioButton.Text = "10x10";
            this.largeBoardRadioButton.UseVisualStyleBackColor = true;

            // m_PlayersLabel
            this.playersLabel.AutoSize = true;
            this.playersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.playersLabel.Location = new System.Drawing.Point(27, 97);
            this.playersLabel.Name = "m_PlayersLabel";
            this.playersLabel.Size = new System.Drawing.Size(76, 24);
            this.playersLabel.TabIndex = 5;
            this.playersLabel.Text = "Players:";

            // m_Player1Label
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(80, 140);
            this.player1Label.Name = "m_Player1Label";
            this.player1Label.Size = new System.Drawing.Size(75, 20);
            this.player1Label.TabIndex = 6;
            this.player1Label.Text = "Player 1:";

            // m_Player1TextBox
            this.player1TextBox.Name = "m_Player1TextBox";
            this.player1TextBox.Size = new System.Drawing.Size(143, 26);
            this.player1TextBox.TabIndex = 7;
            this.player1TextBox.Location = new System.Drawing.Point(184, 134);

            // m_Player2CheckBox
            this.player2CheckBox.AutoSize = true;
            this.player2CheckBox.Location = new System.Drawing.Point(58, 193);
            this.player2CheckBox.Name = "m_Player2CheckBox";
            this.player2CheckBox.Size = new System.Drawing.Size(97, 24);
            this.player2CheckBox.TabIndex = 9;
            this.player2CheckBox.Text = " Player 2:";
            this.player2CheckBox.UseVisualStyleBackColor = true;
            this.player2CheckBox.CheckedChanged += new System.EventHandler(this.player2CheckBox_Click);

            // m_Player2TextBox 
            this.player2TextBox.BackColor = Color.LightGray;
            this.player2TextBox.Enabled = false;
            this.player2TextBox.Location = new System.Drawing.Point(184, 193);
            this.player2TextBox.Name = "m_Player2TextBox";
            this.player2TextBox.Size = new System.Drawing.Size(143, 26);
            this.player2TextBox.TabIndex = 10;
            this.player2TextBox.Text = "       [Computer]";

            // GameSettingForm
            this.ClientSize = new System.Drawing.Size(406, 283);
            this.Controls.Add(this.player2TextBox);
            this.Controls.Add(this.player2CheckBox);
            this.Controls.Add(this.player1TextBox);
            this.Controls.Add(this.player1Label);
            this.Controls.Add(this.playersLabel);
            this.Controls.Add(this.largeBoardRadioButton);
            this.Controls.Add(this.mediumBoardRadioButton);
            this.Controls.Add(this.smallBoardRadioButton);
            this.Controls.Add(this.boardSizeLabel);
            this.Controls.Add(this.doneButton);
        }

        private void player2CheckBox_Click(object sender, EventArgs e)
        {
            if (player2CheckBox.Checked == true)
            {
                player2TextBox.Enabled = true;
                player2TextBox.BackColor = Color.White;
                player2TextBox.Text = string.Empty;
            }
            else
            {
                player2TextBox.Enabled = !true;
                player2TextBox.BackColor = Color.LightGray;
                player2TextBox.Text = "       [Computer]";
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            bool areNamesValid = !true;

            if (player2TextBox.Enabled == true)
            {
                if (CheckNameValidity(player1TextBox.Text) && CheckNameValidity(player2TextBox.Text))
                {
                    areNamesValid = true;
                }
            }
            else
            {
                if (CheckNameValidity(player1TextBox.Text))
                {
                    areNamesValid = true;
                    player2TextBox.Text = "Computer";
                }
            }

            if (areNamesValid)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    @"Invalid name!
Please enter a name up to 20 characters with no spaces");
            }
        }

        public bool CheckNameValidity(string i_PlayerName)
        {
            bool isValid = true;
            int playerNameLength = i_PlayerName.Length;

            if ((playerNameLength > k_MaxiumLengthOfPlayerName) || (i_PlayerName == string.Empty))
            {
                isValid = !true;
            }
            else
            {
                for (int i = 0; i < playerNameLength; i++)
                {
                    if (i_PlayerName[i] == ' ')
                    {
                        isValid = !true;
                    }
                }
            }

            return isValid;
        }
    }
}
