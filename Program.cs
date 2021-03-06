using System;
using System.Collections.Generic;

namespace Testumgebung
{
    class Program
    {
        public static bool KeepRunning;
        static void Main(string[] args)
        {
            List<IGameObject> gameObjectList = new();
            Console.CursorVisible = false;
            KeepRunning = true;

            FPSCounter timer = new FPSCounter();
            Board someText = new Board();
            someText.OffsetX = 10;
            someText.OffsetY = 5;

            gameObjectList.Add(timer);
            gameObjectList.Add(someText);

            do
            {
                for (int count = 0; count < gameObjectList.Count; count++)
                    gameObjectList[count].Draw();

                for (int count = 0; count < gameObjectList.Count; count++)
                    gameObjectList[count].Update();

            } while (KeepRunning);
        }
    }
}
