using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B18_Ex05
{
    public class DamkaGameStatusForm : Form
    {
        private Button m_YesButton = new Button();
        private Button m_NoButton = new Button();
        private Label m_StatusLabel = new Label();
        private Label m_AnotherRound = new Label();
        private PictureBox m_QuestionMarkPicture = new PictureBox();

        public DamkaGameStatusForm()
        {
            this.Text = "Damka";
            this.Size = new Size(350, 250);
        }

        public void InitDamkaGameStatusFormControls()
        {
            m_YesButton.Text = "Yes";
            m_YesButton.Size = new Size(50, 50);
            m_YesButton.Location = new Point(10, 90);
            m_NoButton.Text = "No";
            m_NoButton.Size = new Size(50, 50);
            m_NoButton.Location = new Point(80, 90);
            m_AnotherRound.Location = new Point(45, 65);
            m_AnotherRound.Text = "Another Round?";
            m_QuestionMarkPicture.ImageLocation = "https://www.ecinc.ca/wp-content/uploads/2012/07/question-mark.png";

            this.Controls.Add(m_YesButton);
            this.Controls.Add(m_NoButton);
            this.Controls.Add(m_StatusLabel);
            this.Controls.Add(m_AnotherRound);
            this.Controls.Add(m_QuestionMarkPicture);

            this.m_YesButton.Click += new EventHandler(m_YesButton_Click);
            this.m_NoButton.Click += new EventHandler(m_NoButton_Click);
        }

        void m_YesButton_Click(object sender, EventArgs e)
        {

        }

        void m_NoButton_Click(object sender, EventArgs e)
        {

        }
    }
}