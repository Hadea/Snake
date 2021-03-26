using System;

namespace Testumgebung
{
    class Board : IGameObject
    {
        public string textToDisplay;
        public int OffsetX;
        public int OffsetY;

        bool[,] Spielfeld = new bool[10, 20]; // true = wand, false = begehbar

        int SnakeX = 1;
        int SnakeY = 1;

        public Board()
        {
            for (int count = 0; count < 20; count++)
            {
                //        Y  X
                Spielfeld[9, count] = true;
                Spielfeld[0, count] = true;
            }

            for (int count = 0; count < 10; count++)
            {
                Spielfeld[count, 19] = true;
                Spielfeld[count, 0] = true;
            }
        }

        public void Draw()
        {
            for (int Y = 0; Y < 10; Y++)
            {
                for (int X = 0; X < 20; X++)
                {
                    Console.SetCursorPosition(OffsetX + X, OffsetY + Y);
                    if (Spielfeld[Y, X])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }

            Console.SetCursorPosition(OffsetX + SnakeX, OffsetY + SnakeY);
            Console.Write("O");

        }

        public void Update()
        {

        }
    }
}
