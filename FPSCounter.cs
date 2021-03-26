using System;

namespace Testumgebung
{
    internal class FPSCounter : IGameObject
    {
        double ausgabe = 0;
        DateTime AlteZeit = DateTime.Now;
        int frameCounter = 0;

        public FPSCounter()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(ausgabe);
        }

        public void Update()
        {
            if ((DateTime.Now - AlteZeit).TotalSeconds >= 1)
            {
                ausgabe = frameCounter;
                AlteZeit = DateTime.Now;
                frameCounter = 0;
            }
            frameCounter++;
        }
    }
}