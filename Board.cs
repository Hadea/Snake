using System;

namespace Testumgebung
{
    class Board : IGameObject
    {
        public int OffsetX;
        public int OffsetY;
        readonly FieldState[,] Spielfeld = new FieldState[10, 20]; // true = wand, false = begehbar

        int SnakePosX = 1;
        int SnakePosY = 1;

        int SnakeDirX = 1;
        int SnakeDirY = 0;

        DateTime walkCooldown;
        DateTime appleCooldown;
        readonly Random rndGen = new();

        int score = 0;

        public Board()
        {
            for (int count = 0; count < 20; count++)
            {
                //        Y  X
                Spielfeld[9, count] = FieldState.Wall;
                Spielfeld[0, count] = FieldState.Wall;
            }

            for (int count = 0; count < 10; count++)
            {
                Spielfeld[count, 19] = FieldState.Wall;
                Spielfeld[count, 0] = FieldState.Wall;
            }
        }

        public void Draw()
        {
            // ui zeichnen
            string punkteText = "Punkte: " + score;

            Console.SetCursorPosition(Console.WindowWidth / 2 - punkteText.Length / 2, 1);
            Console.Write(punkteText);

            // spielfeld zeichnen
            for (int Y = 0; Y < 10; Y++)
            {
                for (int X = 0; X < 20; X++)
                {
                    Console.SetCursorPosition(OffsetX + X, OffsetY + Y);
                    switch (Spielfeld[Y, X])
                    {
                        case FieldState.Free:
                            Console.Write(" ");
                            break;
                        case FieldState.Wall:
                            Console.Write("#");
                            break;
                        case FieldState.Apfel:
                            Console.Write("@");
                            break;
                        case FieldState.Snake:
                            Console.Write("O");
                            break;
                        case FieldState.Tail:
                            Console.Write("o");
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Update()
        {
            // walk
            if ((DateTime.Now - walkCooldown).TotalSeconds >= 1)
            {
                walkCooldown = DateTime.Now;

                Spielfeld[SnakePosY, SnakePosX] = FieldState.Free;

                SnakePosX += SnakeDirX;
                SnakePosY += SnakeDirY;

                // alles hier läuft 1x die sekunde

                switch (Spielfeld[SnakePosY, SnakePosX])
                {
                    case FieldState.Free:
                        Spielfeld[SnakePosY, SnakePosX] = FieldState.Snake;
                        break;
                    case FieldState.Apfel:
                        Spielfeld[SnakePosY, SnakePosX] = FieldState.Snake;
                        score++;
                        break;
                    default:
                        SnakePosX = 9;
                        SnakePosY = 4;
                        break;
                }
            }

            // controls
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                switch (pressedKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        SnakeDirX = -1;
                        SnakeDirY = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        SnakeDirX = 1;
                        SnakeDirY = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        SnakeDirX = 0;
                        SnakeDirY = -1;
                        break;
                    case ConsoleKey.DownArrow:
                        SnakeDirX = 0;
                        SnakeDirY = 1;
                        break;
                    case ConsoleKey.Escape:
                        Program.KeepRunning = false;
                        break;
                }

            }

            // apple spawn
            if ((DateTime.Now - appleCooldown).TotalSeconds >= 5)
            {
                appleCooldown = DateTime.Now;
                int ApfelX;
                int ApfelY;

                do
                {
                    ApfelX = rndGen.Next(1, 20);
                    ApfelY = rndGen.Next(1, 10);

                } while (Spielfeld[ApfelY, ApfelX] != FieldState.Free);


                Spielfeld[ApfelY, ApfelX] = FieldState.Apfel;

            }
        }
    }
}
