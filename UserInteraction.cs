using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class UserInteraction
    {
        public void RunGame()
        {
            Game checkerGame;
            GameSettingForm settingForm = new GameSettingForm();
            DamkaForm damkaForm;

            settingForm.ShowDialog();

            checkerGame = new Game(settingForm.GetBoardSize(), settingForm.IsSecondPlayerComputer(), settingForm.Player1Name, settingForm.Player2Name);

            checkerGame.CheckerBoard.CreatAndInitializeBoardMatrix();
            checkerGame.Player1.CreateAndSetPiecesForStartPositions();
            checkerGame.Player2.CreateAndSetPiecesForStartPositions();
            checkerGame.UpdateListsOfMoves(checkerGame.Player1.PlayerNumber);
            checkerGame.UpdateListsOfMoves(checkerGame.Player2.PlayerNumber);

            damkaForm = new DamkaForm(checkerGame);
            damkaForm.ShowDialog();
        }
    }
}