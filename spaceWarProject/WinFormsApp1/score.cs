using System;
using System.Windows.Forms;

namespace spaceWar
{
    public partial class score : Form
    {
        private Scoreboard scoreboard;

        public score(Scoreboard scoreboard)
        {
            InitializeComponent();
            this.scoreboard = scoreboard;
            LoadScoresIntoListBox();
        }

        private void LoadScoresIntoListBox()
        {
            var scores = scoreboard.GetTopScoresFormatted();
            listBox1.Items.AddRange(scores.ToArray());
        }
    }
}
