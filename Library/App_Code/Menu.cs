using System;

namespace Library.App_Code
{
    /// <summary>
    /// Class that handles menu drawing and option choosing
    /// </summary>
    public class Menu
    {
        private int SelectedIndex;
        private readonly string[] MenuOptions;
        private readonly string Title;
        private readonly string Instructions;

        public Menu(string[] menuOptions, string instructions)
        {
            MenuOptions = menuOptions;
            Title = "";
            Instructions = instructions;
            SelectedIndex = 0;
        }

        public Menu(string[] menuOptions, string title, string instructions)
        {
            MenuOptions = menuOptions;
            Title = title;
            Instructions = instructions;
            SelectedIndex = 0;
        }

        /// <summary>
        /// Method that handles main menu
        /// </summary>
        /// <returns>Returns selected menu option index</returns>
        /// <param name="includeTitle">Flag to handle different menues</param>
        public int TheMenu(bool includeTitle)
        {
            Console.CursorVisible = false;
            bool stillOn = true;

            Console.Clear();
            DrawMenu(includeTitle);

            while (stillOn)
            {
                HandleMenuInput(MenuOptions.Length, ref stillOn);

                Console.Clear();
                DrawMenu(includeTitle);
            }

            Console.CursorVisible = true;
            return SelectedIndex;
        }

        /// <summary>
        /// Method that handles main menu drawing(Includes title)
        /// </summary>
        /// <param name="includeTitle">Flag to handle different menues</param>
        private void DrawMenu(bool includeTitle)
        {
            int x = 2, y, initialY;
            if (includeTitle)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(Title);

                Console.SetCursorPosition(0, 8);
                Console.Write(Instructions);

                y = 9;
            }else
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(Instructions);

                y = 1;
            }
            initialY = y;

            foreach (string option in MenuOptions)
            {
                if (y - initialY == SelectedIndex)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write("->");

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.SetCursorPosition(x, y);
                Console.Write(option);

                y++;
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Method that handles keyboard input for the menu
        /// </summary>
        /// <param name="menuLength">The number of options in the menu</param>
        /// <param name="stillOn">Flag to know if escape is pressed</param>
        private void HandleMenuInput(int menuLength, ref bool stillOn)
        {
            ConsoleKey key;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
            } while (Console.KeyAvailable);

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (SelectedIndex != 0)
                    {
                        SelectedIndex--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectedIndex != menuLength - 1)
                    {
                        SelectedIndex++;
                    }
                    break;
                case ConsoleKey.Enter:
                    stillOn = false;
                    break;
                case ConsoleKey.Escape:
                    SelectedIndex = menuLength - 1;
                    stillOn = false;
                    break;
                default:
                    break;
            }
        }
    }
}
