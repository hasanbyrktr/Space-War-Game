using System;
using System.Windows.Forms;

namespace spaceWar  // namespace'i projenizde kullandığınız şekilde bırakıyorum
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpaceWarGame game = new SpaceWarGame();
            Application.Run(new IntroForm(game));
        }
    }
}