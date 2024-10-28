using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mortal_Kombat
{
    class Program

    {
        /*/
         * Code for the beam animation that is so far, scrapped.

                while (g.Length < 218 && ready == true && pos == shoot)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(22, shoot);
                    g = g.Replace("э", "=э");
                    g = g.Replace("========э", "\b ========э");
                    Console.Write(g);
                }
        /*/
        static void CurrentDomain_ProcessExit(object sender, EventArgs e) {
            NetManager netManager = new NetManager();
            netManager.Send("11111111").GetAwaiter().GetResult();
        }

        public static string StringToBinary(string input) {
            StringBuilder binary = new StringBuilder();
            foreach (char c in input) {
                binary.Append(Convert.ToString(c, 2).PadLeft(8, '0') + " "); // Convert char to binary and pad to 8 bits
            }
            return binary.ToString().Trim();
        }

        public static void Draw(string text, int? x = null, int? y = null, ConsoleColor? color = null)
        {
            if(x == null) { x = Console.CursorLeft; }
            if(y == null) { y = Console.CursorTop; }
            if(color == null) { color = Console.ForegroundColor; }
            Console.SetCursorPosition(Convert.ToInt32(x), Convert.ToInt32(y));
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Convert.ToString(color), true);

            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                Console.SetCursorPosition(Convert.ToInt32(x), Convert.ToInt32(y));
                Console.Write(line);
                y++;
            }

        }

        public static bool SendData(string data, string expectedResult) {
            NetManager netManager = new NetManager();
            return StringToBinary(netManager.Send(data).GetAwaiter().GetResult()) == expectedResult;
        }

        public static void Disconnect() {
            AppDomain.CurrentDomain.ProcessExit -= new EventHandler(CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.DomainUnload -= CurrentDomain_ProcessExit;
        }

        static void Main(string[] args)
        {
            NetManager netManager = new NetManager();
            //// Settings ////
            Console.Title = "Settings";
            bool randomCrits = false;
            bool randomSpread = true;
            int hpAmount = 100;
            int manaAmount = 100;
            int staminaAmount = 100;
            ConsoleColor p1Color = ConsoleColor.Blue;
            ConsoleColor p2Color = ConsoleColor.Red;
            string enemyname = "CPU";
            string yourname = "Guest";
            bool concealAttacks = false;
            bool debugCheats = true;
            string ver = "2.0.0";

            //// Variables definition////
            Console.Title = "Variables Definition";
            // Console.WriteLine("Enter your username: ");
            // string name = Console.ReadLine();
            int enemyHp = hpAmount;
            int yourHp = hpAmount;
            int enemyMana = manaAmount;
            int yourMana = manaAmount;
            int enemyStamina = staminaAmount;
            int yourStamina = staminaAmount;
            bool yourTurn = true;
            bool onGoingGame = false;
            string yourAttack = "";
            string enemyAttack = "";
            int enemyAtt = 0;
            bool inSettings = false;
            bool inOnlineMenu = false;
            bool multiplayerMode = false;
            bool onlineMode = false;                  // DO NOT CHANGE

            Random random = new Random();
            //// Console Variations ////
            Console.Title = "Console Variations";
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.CursorVisible = false;

            // Need to get the position to print things at.
            int consoleWidthCenter = Console.LargestWindowWidth / 2;
            int consoleHeightCenter = Console.LargestWindowHeight / 2;

            //// Main Menu ////
            while(true) {
                int curPos = 11;
                Console.Clear();
                yourTurn = true;
                yourAttack = "";
                enemyHp = hpAmount;
                yourHp = hpAmount;
                enemyMana = manaAmount;
                yourMana = manaAmount;
                enemyStamina = staminaAmount;
                yourStamina = staminaAmount;
                randomSpread = true;
                ConsoleColor p1Dark = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "Dark" + p1Color, true);
                ConsoleColor p2Dark = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "Dark" + p2Color, true);
                Console.CursorVisible = false;
                while (inSettings)
                {
                    Console.CursorVisible = false;
                    enemyHp = hpAmount;
                    yourHp = hpAmount;
                    enemyMana = manaAmount;
                    yourMana = manaAmount;
                    enemyStamina = staminaAmount;
                    yourStamina = staminaAmount;
                    p1Dark = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "Dark" + p1Color, true);
                    p2Dark = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "Dark" + p2Color, true);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Title = "Settings";

                    Draw(".|'''|           ||      ||                               ", consoleWidthCenter - 30, 1, ConsoleColor.Blue);
                    Draw("||               ||      ||     ''                        ", consoleWidthCenter - 30, 2, ConsoleColor.Blue);
                    Draw("`|'''|, .|''|, ''||''  ''||''   ||  `||''|,  .|''|, ('''' ", consoleWidthCenter - 30, 3, ConsoleColor.Blue);
                    Draw(" .   || ||..||   ||      ||     ||   ||  ||  ||  ||  `'') ", consoleWidthCenter - 30, 4, ConsoleColor.Blue);
                    Draw(" |...|' `|...    `|..'   `|..' .||. .||  ||. `|..|| `...' ", consoleWidthCenter - 30, 5, ConsoleColor.Blue);
                    Draw("                                                 ||       ", consoleWidthCenter - 30, 6, ConsoleColor.Blue);
                    Draw("                                              `..|'       ", consoleWidthCenter - 30, 7, ConsoleColor.Blue);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Draw("------------------------ ---------", consoleWidthCenter - 17, 9, ConsoleColor.Cyan);
                    Draw("                        ", consoleWidthCenter - 17, 10, ConsoleColor.Cyan);
                    
                    if (curPos == 11) {
                        Draw("  Random Crits  ", consoleWidthCenter - 17, 11, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(randomCrits), consoleWidthCenter + 9, 11, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Random Crits  ", consoleWidthCenter - 17, 11, ConsoleColor.Cyan);
                        Draw(Convert.ToString(randomCrits), consoleWidthCenter + 9, 11, ConsoleColor.Cyan);
                    }

                    if (curPos == 12) {
                        Draw("  Random Damage Spread  ", consoleWidthCenter - 17, 12, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(randomSpread), consoleWidthCenter + 9, 12, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Random Damage Spread  ", consoleWidthCenter - 17, 12, ConsoleColor.Cyan);
                        Draw(Convert.ToString(randomSpread), consoleWidthCenter + 9, 12, ConsoleColor.Cyan);
                    }

                    if (curPos == 13) {
                        Draw("  HP Amount  ", consoleWidthCenter - 17, 13, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(hpAmount), consoleWidthCenter + 9, 13, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  HP Amount  ", consoleWidthCenter - 17, 13, ConsoleColor.Cyan);
                        Draw(Convert.ToString(hpAmount), consoleWidthCenter + 9, 13, ConsoleColor.Cyan);
                    }

                    if (curPos == 14) {
                        Draw("  Mana Amount  ", consoleWidthCenter - 17, 14, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(manaAmount), consoleWidthCenter + 9, 14, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Mana Amount  ", consoleWidthCenter - 17, 14, ConsoleColor.Cyan);
                        Draw(Convert.ToString(manaAmount), consoleWidthCenter + 9, 14, ConsoleColor.Cyan);
                    }

                    if (curPos == 15) {
                        Draw("  Stamina Amount  ", consoleWidthCenter - 17, 15, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(staminaAmount), consoleWidthCenter + 9, 15, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Stamina Amount  ", consoleWidthCenter - 17, 15, ConsoleColor.Cyan);
                        Draw(Convert.ToString(staminaAmount), consoleWidthCenter + 9, 15, ConsoleColor.Cyan);
                    }

                    if (curPos == 16) {
                        Draw("  Player 1 Color  ", consoleWidthCenter - 17, 16, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(p1Color), consoleWidthCenter + 9, 16, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Player 1 Color  ", consoleWidthCenter - 17, 16, ConsoleColor.Cyan);
                        Draw(Convert.ToString(p1Color), consoleWidthCenter + 9, 16, ConsoleColor.Cyan);
                    }

                    if (curPos == 17) {
                        Draw("  Player 2 Color  ", consoleWidthCenter - 17, 17, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(p2Color), consoleWidthCenter + 9, 17, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Player 2 Color  ", consoleWidthCenter - 17, 17, ConsoleColor.Cyan);
                        Draw(Convert.ToString(p2Color), consoleWidthCenter + 9, 17, ConsoleColor.Cyan);
                    }

                    if (curPos == 18) {
                        Draw("  Enemy's Name  ", consoleWidthCenter - 17, 18, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(enemyname), consoleWidthCenter + 9, 18, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Enemy's Name  ", consoleWidthCenter - 17, 18, ConsoleColor.Cyan);
                        Draw(Convert.ToString(enemyname), consoleWidthCenter + 9, 18, ConsoleColor.Cyan);
                    }

                    if (curPos == 19) {
                        Draw("  Conceal Attacks  ", consoleWidthCenter - 17, 19, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(concealAttacks), consoleWidthCenter + 9, 19, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Conceal Attacks  ", consoleWidthCenter - 17, 19, ConsoleColor.Cyan);
                        Draw(Convert.ToString(concealAttacks), consoleWidthCenter + 9, 19, ConsoleColor.Cyan);
                    }

                    if (curPos == 20) {
                        Draw("  Main Menu             ", consoleWidthCenter - 17, 20, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Main Menu             ", consoleWidthCenter - 17, 20, ConsoleColor.Cyan);
                    }

                    Draw("                        ", consoleWidthCenter - 17, 21, ConsoleColor.Cyan);
                    Draw("------------------------ ---------", consoleWidthCenter - 17, 22, ConsoleColor.Cyan);

                    Draw("> ", consoleWidthCenter - 17, curPos, ConsoleColor.DarkCyan);

                    ConsoleKey nextinput = Console.ReadKey(intercept: true).Key;
                    if (nextinput == ConsoleKey.DownArrow || nextinput == ConsoleKey.S)
                    {
                        if (curPos != 20) { curPos++; }
                        else { curPos = 11; }
                    }
                    else if (nextinput == ConsoleKey.UpArrow || nextinput == ConsoleKey.W)
                    {
                        if (curPos != 11) { curPos--; }
                        else { curPos = 20; }
                    }
                    else if (nextinput == ConsoleKey.RightArrow || nextinput == ConsoleKey.Enter || nextinput == ConsoleKey.Spacebar || nextinput == ConsoleKey.D)
                    {
                        if (curPos == 11)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Draw("Random Crits?", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.DarkCyan);
                                Draw("1) True", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Cyan);
                                Draw("2) False", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Cyan);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1) {
                                    Console.Clear();
                                    randomCrits = true;
                                    break;
                                } else if (nexti == ConsoleKey.D2) {
                                    Console.Clear();
                                    randomCrits = false;
                                    break;
                                }
                            }
                        }
                        if (curPos == 12)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Draw("Random Damage/Defense Spread?", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.DarkCyan);
                                Draw("1) True", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Cyan);
                                Draw("2) False", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Cyan);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1)
                                {
                                    Console.Clear();
                                    randomSpread = true;
                                    break;
                                }
                                else if (nexti == ConsoleKey.D2)
                                {
                                    Console.Clear();
                                    randomSpread = false;
                                    break;
                                }
                            }
                        }
                        if (curPos == 13)
                        {
                            hpAmount = 0;
                            while (hpAmount == 0)
                            {
                                try
                                {
                                    Console.Clear();
                                    Draw("Enter HP Amount: ", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.DarkCyan);
                                    hpAmount = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                }
                                catch
                                {
                                    hpAmount = 0;
                                }
                            }
                        }
                        if (curPos == 14)
                        {
                            manaAmount = 0;
                            while (manaAmount == 0)
                            {
                                try
                                {
                                    Console.Clear();
                                    Draw("Enter Mana Amount: ", consoleWidthCenter - 11, consoleHeightCenter, ConsoleColor.DarkCyan);
                                    manaAmount = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                }
                                catch
                                {
                                    manaAmount = 0;
                                }
                            }
                        }
                        if (curPos == 15)
                        {
                            staminaAmount = 0;
                            while (staminaAmount == 0)
                            {
                                try
                                {
                                    Console.Clear();
                                    Draw("Enter Stamina Amount: ", consoleWidthCenter - 12, consoleHeightCenter, ConsoleColor.DarkCyan);
                                    staminaAmount = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                }
                                catch
                                {
                                    staminaAmount = 0;
                                }
                            }
                        }
                        if (curPos == 16)
                        {
                            p1Color = ConsoleColor.Black;
                            while (p1Color == ConsoleColor.Black)
                            {
                                Console.Clear();
                                Draw("Select Player 1 Color: ", consoleWidthCenter - 10, consoleHeightCenter - 4, ConsoleColor.DarkCyan);

                                Draw("1) Blue ", consoleWidthCenter - 10, consoleHeightCenter - 3, ConsoleColor.Blue);
                                Draw("2) Green ", consoleWidthCenter - 10, consoleHeightCenter - 2, ConsoleColor.Green);
                                Draw("3) Cyan", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.Cyan);
                                Draw("4) Red ", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Red);
                                Draw("5) Magenta ", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Magenta);
                                Draw("6) Yellow ", consoleWidthCenter - 10, consoleHeightCenter + 2, ConsoleColor.Yellow);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Blue;
                                } else if (nexti == ConsoleKey.D2) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Green;
                                } else if (nexti == ConsoleKey.D3) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Cyan;
                                } else if (nexti == ConsoleKey.D4) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Red;
                                } else if (nexti == ConsoleKey.D5) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Magenta;
                                }
                                else if (nexti == ConsoleKey.D6) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Yellow;
                                } else {
                                    p1Color = ConsoleColor.Black;
                                }
                            }
                        }
                        if (curPos == 17)
                        {
                            p2Color = ConsoleColor.Black;
                            while (p2Color == ConsoleColor.Black)
                            {
                                Console.Clear();
                                Draw("Select Player 2 Color: ", consoleWidthCenter - 10, consoleHeightCenter - 4, ConsoleColor.DarkCyan);

                                Draw("1) Blue ", consoleWidthCenter - 10, consoleHeightCenter - 3, ConsoleColor.Blue);
                                Draw("2) Green ", consoleWidthCenter - 10, consoleHeightCenter - 2, ConsoleColor.Green);
                                Draw("3) Cyan", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.Cyan);
                                Draw("4) Red ", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Red);
                                Draw("5) Magenta ", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Magenta);
                                Draw("6) Yellow ", consoleWidthCenter - 10, consoleHeightCenter + 2, ConsoleColor.Yellow);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1) {
                                    Console.Clear();
                                    p2Color = ConsoleColor.Blue;
                                } else if (nexti == ConsoleKey.D2) {
                                    Console.Clear();
                                    p2Color = ConsoleColor.Green;
                                } else if (nexti == ConsoleKey.D3) {
                                    Console.Clear();
                                    p2Color = ConsoleColor.Cyan;
                                } else if (nexti == ConsoleKey.D4) {
                                    Console.Clear();
                                    p2Color = ConsoleColor.Red;
                                } else if (nexti == ConsoleKey.D5) {
                                    Console.Clear();
                                    p2Color = ConsoleColor.Magenta;
                                } else if (nexti == ConsoleKey.D6) {
                                    Console.Clear();
                                    p2Color = ConsoleColor.Yellow;
                                } else {
                                    p2Color = ConsoleColor.Black;
                                }
                            }
                        }
                        if (curPos == 18)
                        {
                            enemyname = "";
                            while (enemyname == "")
                            {
                                try
                                {
                                    Console.Clear();
                                    Draw("Enter Enemy Name: ", consoleWidthCenter - 12, consoleHeightCenter, ConsoleColor.DarkCyan);
                                    enemyname = Console.ReadLine();
                                    Console.Clear();
                                }
                                catch
                                {
                                    enemyname = "";
                                }
                            }
                        }
                        if (curPos == 19)
                        {
                            while (true)
                            {
                                Draw("Conceal Attacks?", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.DarkCyan);
                                Draw("1) True", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Cyan);
                                Draw("2) False", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Cyan);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1)
                                {
                                    Console.Clear();
                                    concealAttacks = true;
                                    break;
                                }
                                else if (nexti == ConsoleKey.D2)
                                {
                                    Console.Clear();
                                    concealAttacks = false;
                                    break;
                                }
                                else
                                {
                                    // pass
                                }
                            }
                        }
                        if (curPos == 20)
                        {
                            curPos = 9;
                            Console.Clear();
                            inSettings = false;
                        }

                    }
                }
                while (inOnlineMenu) {
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Title = "Online Menu"; 
                                               
                    Draw(" ..|''||            '||   ||                   ", consoleWidthCenter - 25, 1, ConsoleColor.Blue);
                    Draw(".|'    ||  .. ...    ||  ...  .. ...     ....  ", consoleWidthCenter - 25, 2, ConsoleColor.Blue);
                    Draw("||      ||  ||  ||   ||   ||   ||  ||  .|...|| ", consoleWidthCenter - 25, 3, ConsoleColor.Blue);
                    Draw("'|.     ||  ||  ||   ||   ||   ||  ||  ||      ", consoleWidthCenter - 25, 4, ConsoleColor.Blue);
                    Draw(" ''|...|'  .||. ||. .||. .||. .||. ||.  '|...' ", consoleWidthCenter - 25, 5, ConsoleColor.Blue);


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Draw("------------------------ ---------", consoleWidthCenter - 17, 9, ConsoleColor.Cyan);
                    Draw("                        ", consoleWidthCenter - 17, 10, ConsoleColor.Cyan);

                    if (curPos == 11) {
                        Draw("  Random Damage Spread  ", consoleWidthCenter - 17, 11, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(randomSpread), consoleWidthCenter + 9, 11, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Random Damage Spread  ", consoleWidthCenter - 17, 11, ConsoleColor.Cyan);
                        Draw(Convert.ToString(randomSpread), consoleWidthCenter + 9, 11, ConsoleColor.Cyan);
                    }

                    if (curPos == 12) {
                        Draw("  Your Color  ", consoleWidthCenter - 17, 12, ConsoleColor.DarkCyan);
                        Draw(Convert.ToString(p1Color), consoleWidthCenter + 9, 12, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Your Color  ", consoleWidthCenter - 17, 12, ConsoleColor.Cyan);
                        Draw(Convert.ToString(p1Color), consoleWidthCenter + 9, 12, ConsoleColor.Cyan);
                    }

                    if (curPos == 13) {
                        Draw("  Your Name  ", consoleWidthCenter - 17, 13, ConsoleColor.DarkCyan);
                        Draw(yourname, consoleWidthCenter + 9, 13, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Your Name  ", consoleWidthCenter - 17, 13, ConsoleColor.Cyan);
                        Draw(yourname, consoleWidthCenter + 9, 13, ConsoleColor.Cyan);
                    }

                    if (curPos == 14) {
                        Draw("  Connect                        ", consoleWidthCenter - 17, 14, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Connect                        ", consoleWidthCenter - 17, 14, ConsoleColor.Cyan);
                    }

                    if (curPos == 15) {
                        Draw("  Refresh             ", consoleWidthCenter - 17, 15, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Refresh             ", consoleWidthCenter - 17, 15, ConsoleColor.Cyan);
                    }

                    if (curPos == 16) {
                        Draw("  Main Menu             ", consoleWidthCenter - 17, 16, ConsoleColor.DarkCyan);
                    } else {
                        Draw("  Main Menu             ", consoleWidthCenter - 17, 16, ConsoleColor.Cyan);
                    }
                    
                    Draw("                        ", consoleWidthCenter - 17, 19, ConsoleColor.Cyan);
                    Draw("------------------------ ---------", consoleWidthCenter - 17, 20, ConsoleColor.Cyan);

                    Draw("> ", consoleWidthCenter - 17, curPos, ConsoleColor.DarkCyan);

                    ConsoleKey nextinput = Console.ReadKey(intercept: true).Key;
                    if (nextinput == ConsoleKey.DownArrow || nextinput == ConsoleKey.S) {
                        if (curPos != 16) { curPos++; } else { curPos = 11; }
                    } else if (nextinput == ConsoleKey.UpArrow || nextinput == ConsoleKey.W) {
                        if (curPos != 11) { curPos--; } else { curPos = 16; }
                    } else if (nextinput == ConsoleKey.RightArrow || nextinput == ConsoleKey.Enter || nextinput == ConsoleKey.Spacebar || nextinput == ConsoleKey.D) {
                        if (curPos == 11) {
                            while (true) {
                                Console.Clear();
                                Draw("Random Damage/Defense Spread?", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.DarkCyan);
                                Draw("1) True", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Cyan);
                                Draw("2) False", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Cyan);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1) {
                                    Console.Clear();
                                    randomSpread = true;
                                    break;
                                } else if (nexti == ConsoleKey.D2) {
                                    Console.Clear();
                                    randomSpread = false;
                                    break;
                                }
                            }
                        }
                        if (curPos == 12) {
                            p1Color = ConsoleColor.Black;
                            while (p1Color == ConsoleColor.Black) {
                                Console.Clear();
                                Draw("Select Player 1 Color: ", consoleWidthCenter - 10, consoleHeightCenter - 4, ConsoleColor.DarkCyan);

                                Draw("1) Blue ", consoleWidthCenter - 10, consoleHeightCenter - 3, ConsoleColor.Blue);
                                Draw("2) Green ", consoleWidthCenter - 10, consoleHeightCenter - 2, ConsoleColor.Green);
                                Draw("3) Cyan", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.Cyan);
                                Draw("4) Red ", consoleWidthCenter - 10, consoleHeightCenter, ConsoleColor.Red);
                                Draw("5) Magenta ", consoleWidthCenter - 10, consoleHeightCenter + 1, ConsoleColor.Magenta);
                                Draw("6) Yellow ", consoleWidthCenter - 10, consoleHeightCenter + 2, ConsoleColor.Yellow);

                                ConsoleKey nexti = Console.ReadKey().Key;
                                if (nexti == ConsoleKey.D1) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Blue;
                                } else if (nexti == ConsoleKey.D2) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Green;
                                } else if (nexti == ConsoleKey.D3) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Cyan;
                                } else if (nexti == ConsoleKey.D4) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Red;
                                } else if (nexti == ConsoleKey.D5) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Magenta;
                                } else if (nexti == ConsoleKey.D6) {
                                    Console.Clear();
                                    p1Color = ConsoleColor.Yellow;
                                } else {
                                    p1Color = ConsoleColor.Black;
                                }
                            }
                        }
                        if (curPos == 13) {
                            yourname = "";
                            while (yourname == "") {
                                try {
                                    Console.Clear();
                                    Draw("Enter your name: ", consoleWidthCenter - 10, consoleHeightCenter - 1, ConsoleColor.DarkCyan);
                                    yourname = Console.ReadLine();
                                    if (yourname.Length > 12) yourname = "";
                                    Console.Clear();
                                } catch {
                                    yourname = "";
                                }
                            }
                        }
                        if (curPos == 14) {
                            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
                            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_ProcessExit);
                            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_ProcessExit;
                            Draw("  Connecting...                ", consoleWidthCenter - 17, 14, ConsoleColor.DarkCyan);
                            if (SendData("10000000", "10000000")) {
                                Draw("  Sending player info...    ", consoleWidthCenter - 17, 14, ConsoleColor.DarkCyan);
                                // Color
                                if (p1Color == ConsoleColor.Blue) SendData("11010001", "10000000");
                                if (p1Color == ConsoleColor.Green) SendData("11010010", "10000000");
                                if (p1Color == ConsoleColor.Cyan) SendData("11010011", "10000000");
                                if (p1Color == ConsoleColor.Red) SendData("11010100", "10000000");
                                if (p1Color == ConsoleColor.Magenta) SendData("11010101", "10000000");
                                if (p1Color == ConsoleColor.Yellow) SendData("11010110", "10000000");
                                // Name
                                if (SendData("10000010", "10000000")) netManager.Send(yourname, false).Wait();
                                // Random Spread
                                if (!randomSpread) SendData("10111000", "10000000");
                                Draw("  Waiting for other player...    ", consoleWidthCenter - 17, 14, ConsoleColor.DarkCyan);
                                if (StringToBinary(netManager.Send("10100000").GetAwaiter().GetResult()) == "10100000") {
                                    Draw("  Starting game...    ", consoleWidthCenter - 17, 14, ConsoleColor.DarkCyan);
                                    inOnlineMenu = false;
                                    onlineMode = true;
                                    Console.Clear();
                                    curPos = 11;
                                    StartOnlineGame();
                                    Disconnect();
                                } else {
                                    Disconnect();
                                    Draw("  Error Connecting...                ", consoleWidthCenter - 17, 14, ConsoleColor.Red);
                                    Console.ReadKey();
                                }
                            } else {
                                Disconnect();
                                Draw("  Error Connecting...    ", consoleWidthCenter - 17, 14, ConsoleColor.Red);
                                Console.ReadKey();
                            }
                        }
                        if (curPos == 15) {
                            char[] bits = StringToBinary(netManager.Send("11001100").GetAwaiter().GetResult()).ToCharArray();
                            if (bits[0] != '0' || bits[1] != '0' || bits[2] != '0' || bits[3] != '0' || bits[4] != '0' || bits[5] != '0') {
                                Draw("  Error Refreshing...    ", consoleWidthCenter - 17, 16, ConsoleColor.Red);
                                Console.ReadKey();
                            } else {
                                bool p1o = bits[6] == '1';
                                bool p2o = bits[7] == '1';
                                if (p1o) { Draw("  Player 1 Status: Online    ", consoleWidthCenter - 17, 17, ConsoleColor.Green); } else { Draw("  Player 1 Status: Offline    ", consoleWidthCenter - 17, 17, ConsoleColor.Red); }
                                if (p2o) { Draw("  Player 2 Status: Online    ", consoleWidthCenter - 17, 18, ConsoleColor.Green); } else { Draw("  Player 2 Status: Offline     ", consoleWidthCenter - 17, 18, ConsoleColor.Red); }
                            }

                        }
                        if (curPos == 16) {
                            curPos = 9;
                            Console.Clear();
                            inOnlineMenu = false;
                        }
                    }
                }
                while (onGoingGame == false && inSettings == false && inOnlineMenu == false)
                {
                    Console.CursorVisible = false;
                    Draw(ver, 0, 0, ConsoleColor.White);
                    Console.Title = "Main Menu";
                    Draw("'||\\   /||`                             '||\\   /||`                          ", consoleWidthCenter - 39, 1, ConsoleColor.Blue);
                    Draw(" ||\\\\.//||           ''                  ||\\\\.//||                            ", consoleWidthCenter - 39, 2);
                    Draw(" ||     ||   '''|.   ||  `||''|,         ||     ||  .|''|, `||''|,  '||  ||` ", consoleWidthCenter - 39, 3);
                    Draw(" ||     ||  .|''||   ||   ||  ||         ||     ||  ||..||  ||  ||   ||  ||  ", consoleWidthCenter - 39, 4);
                    Draw(".||     ||. `|..||. .||. .||  ||.       .||     ||. `|...  .||  ||.  `|..'|. ", consoleWidthCenter - 39, 5);

                    Draw("------------------------", consoleWidthCenter - 12, 7, ConsoleColor.Cyan);
                    Draw("                        ", consoleWidthCenter - 12, 8);

                    if (curPos == 9) { Draw("   Single Player Mode   ", consoleWidthCenter - 12, 9, ConsoleColor.DarkCyan); }
                    else { Draw("   Single Player Mode   ", consoleWidthCenter - 12, 9, ConsoleColor.Cyan); }

                    if (curPos == 10) { Draw("    Multiplayer Mode    ", consoleWidthCenter - 12, 10, ConsoleColor.DarkCyan); }
                    else { Draw("    Multiplayer Mode    ", consoleWidthCenter - 12, 10, ConsoleColor.Cyan); }

                    if (curPos == 11) { Draw("      Online Mode       ", consoleWidthCenter - 12, 11, ConsoleColor.Red); }
                    else { Draw("      Online Mode       ", consoleWidthCenter - 12, 11, ConsoleColor.Cyan); }

                    if (curPos == 12) { Draw("        Settings        ", consoleWidthCenter - 12, 12, ConsoleColor.DarkCyan); }
                    else { Draw("        Settings        ", consoleWidthCenter - 12, 12, ConsoleColor.Cyan); }

                    if (curPos == 13) { Draw("          Quit          ", consoleWidthCenter - 12, 13, ConsoleColor.Red); }
                    else { Draw("          Quit          ", consoleWidthCenter - 12, 13, ConsoleColor.Cyan); }

                    Draw("                        ", consoleWidthCenter - 12, 14, ConsoleColor.Cyan);
                    Draw("------------------------", consoleWidthCenter - 12, 15);
                    Draw("> ", consoleWidthCenter - 12, curPos, ConsoleColor.DarkCyan);

                    ConsoleKey nextinput = Console.ReadKey(intercept: true).Key;
                    if (nextinput == ConsoleKey.DownArrow || nextinput == ConsoleKey.S) {
                        if (curPos != 13) { curPos++; } else { curPos = 9; }
                    } else if (nextinput == ConsoleKey.UpArrow || nextinput == ConsoleKey.W) {
                        if (curPos != 9) { curPos--; } else { curPos = 13; }
                    } else if (nextinput == ConsoleKey.RightArrow || nextinput == ConsoleKey.Enter || nextinput == ConsoleKey.Spacebar || nextinput == ConsoleKey.D) {
                        // Game time
                        if (curPos == 9) {
                            Console.Clear();
                            multiplayerMode = false;
                            onGoingGame = true;
                        } if (curPos == 10) {
                            Console.Clear();
                            multiplayerMode = true;
                            onGoingGame = true;
                        } if (curPos == 11) {
                            Console.Clear(); 
                            inOnlineMenu = true;
                        } if (curPos == 12) {
                            Console.Clear();
                            inSettings = true;
                        } if (curPos == 13) {
                            Environment.Exit(0);
                        }
                    }
                }

                void DrawCharacters() {
                    Draw("         .---.", 12, 7, p1Dark);
                    Draw("         |    \\", 12, 8);
                    Draw("         |     \\", 12, 9);
                    Draw("         |      \\", 12, 10);
                    Draw("         |       \\", 12, 11);
                    Draw("         |        '.___", 12, 12);
                    Draw("         :          '-  >", 12, 13);
                    Draw("       _/_.-``'--..__.-`", 12, 14);

                    Draw("        <`_", 12, 15, ConsoleColor.Gray);
                    Draw("(..: ` \\", null, null, ConsoleColor.DarkYellow);
                    Draw("o", null, null, p1Color);
                    Draw("|", null, null, ConsoleColor.DarkYellow);

                    Draw("        (\\|", 12, 16, ConsoleColor.Gray);
                    Draw("|     ` \\:", null, null, ConsoleColor.DarkYellow);

                    Draw("        /| ", 12, 17, ConsoleColor.Gray);
                    Draw("`-.__.(._)", null, null, ConsoleColor.DarkYellow);

                    Draw("       : |`   ``.` )\\", 12, 18, ConsoleColor.Gray);
                    Draw("       ; ;  `  (.-`'/", 12, 19);

                    Draw("        >-", 12, 20, p1Color);
                    Draw("\\       ` |", null, null, ConsoleColor.Gray);

                    Draw("      .`", 12, 21, p1Color);
                    Draw("   \\  `     |", null, null, ConsoleColor.Gray);

                    Draw("     /'-..__", 12, 22, p1Color);
                    Draw(":    `  |", null, null, ConsoleColor.Gray);
                    Draw("\\     ____", null, null, p1Color);
                    Draw("..-_-.", null, null, ConsoleColor.DarkYellow);

                    Draw("    /", 12, 23, p1Color);
                    Draw("       ;      `:", null, null, ConsoleColor.Gray);
                    Draw(" \\.-``  //", null, null, p1Color);
                    Draw("__.-'.\\", null, null, ConsoleColor.DarkYellow);

                    Draw("   /", 12, 24, p1Color);
                    Draw(" .`   .  ", null, null, p1Dark);
                    Draw("\\  `   )", null, null, ConsoleColor.Gray);
                    Draw("  : /  //`", null, null, p1Color);
                    Draw("  /", null, null, ConsoleColor.Yellow);
                    Draw("`", null, null, ConsoleColor.DarkYellow);
                    Draw(" /", null, null, ConsoleColor.Yellow);
                    Draw("`", null, null, ConsoleColor.DarkYellow);

                    Draw("  /", 12, 25, p1Color);
                    Draw(" /.     :", null, null, p1Dark);
                    Draw("  :   ` |", null, null, ConsoleColor.Gray);
                    Draw("  |   //", null, null, p1Color);
                    Draw("   / `/", null, null, ConsoleColor.Yellow);

                    Draw(" : ", 12, 26, p1Color);
                    Draw(":(      |  ", null, null, p1Dark);
                    Draw("|     ;  ", null, null, ConsoleColor.Gray);
                    Draw(":  ((  ", null, null, p1Color);
                    Draw("_", null, null, ConsoleColor.DarkYellow);
                    Draw("/` /      ", null, null, ConsoleColor.Yellow);

                    Draw(" |", 12, 27, p1Color);
                    Draw(" |       ;  ", null, null, p1Dark);
                    Draw("; `   /", null, null, ConsoleColor.Gray);
                    Draw(" /|  || ", null, null, p1Color);
                    Draw("/'-.-`)     ", null, null, ConsoleColor.DarkYellow);

                    Draw(" |", 12, 28, p1Color);
                    Draw(" |       _\\ ", null, null, p1Dark);
                    Draw(" \\  ` )  ", null, null, ConsoleColor.Gray);
                    Draw(" _.;;   ", null, null, p1Dark);
                    Draw("`   _-/     ", null, null, ConsoleColor.DarkYellow);

                    Draw(" | ", 12, 29, p1Color);
                    Draw(";         '._", null, null, p1Dark);
                    Draw("\\__/", null, null, ConsoleColor.Gray);
                    Draw("_.-`'. \\\\   ", null, null, p1Dark);
                    Draw(".-.-`       ", null, null, ConsoleColor.DarkYellow);

                    Draw(" |  ", 12, 30, p1Color);
                    Draw("\\   .     `     \\     : ::", null, null, p1Dark);
                    Draw("/            ", null, null, ConsoleColor.Yellow);

                    Draw(" | ", 12, 31, p1Color);
                    Draw("  \\   \\ '.        )    | ||", null, null, p1Dark);

                    Draw(" | ", 12, 32, p1Color);
                    Draw("   \\   '.                ||", null, null, p1Dark);

                    // P2
                    Draw("                          ,---.         \n                         /    |         \n                        /     |         \n                       /      |         \n                      /       |         \n                 ___,'        |         \n               <  -'          :         \n                `-.__..--'``-,_\\_       ", Console.LargestWindowWidth - 53, 7, p2Dark);

                    Draw("                   |", Console.LargestWindowWidth - 53, 15, ConsoleColor.DarkYellow);
                    Draw("o", null, null, p2Color);
                    Draw("/ ` :,.)", null, null, ConsoleColor.DarkYellow);
                    Draw("_`>        ", null, null, ConsoleColor.Gray);

                    Draw("                   :/ `     |", Console.LargestWindowWidth - 53, 16, ConsoleColor.DarkYellow);
                    Draw("|/)        ", null, null, ConsoleColor.Gray);

                    Draw("                   (_.).__,-`", Console.LargestWindowWidth - 53, 17, ConsoleColor.DarkYellow);
                    Draw(" |\\        ", null, null, ConsoleColor.Gray);

                    Draw("                   /( `.``   `| :       \n                   \\'`-.)  `  ; ;       \n                   | `       /", Console.LargestWindowWidth - 53, 18);

                    Draw("-<        ", null, null, p2Color);

                    Draw("                   |     `  /", Console.LargestWindowWidth - 53, 21, ConsoleColor.Gray);
                    Draw("   `.       ", null, null, p2Color);

                    Draw("   ,-_-..", Console.LargestWindowWidth - 53, 22, ConsoleColor.DarkYellow);
                    Draw("____     /", null, null, p2Color);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("|  `    :");
                    Console.ForegroundColor = p2Color;
                    Console.Write("__..-'\\     ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 23);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("  /,'-.__");
                    Console.ForegroundColor = p2Color;
                    Console.Write("\\\\  ``-./ ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(":`      ;       ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("\\    ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 24);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("  `");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\\");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" `");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\\");
                    Console.ForegroundColor = p2Color;
                    Console.Write("  `\\\\  \\ :  ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(   `  /");
                    Console.ForegroundColor = p2Dark;
                    Console.Write("  ,   `. ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("\\   ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 25);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("    \\` \\  ");
                    Console.ForegroundColor = p2Color;
                    Console.Write(" \\\\   | ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" | `   : ");
                    Console.ForegroundColor = p2Dark;
                    Console.Write(" :     .\\");
                    Console.ForegroundColor = p2Color;
                    Console.Write(" \\  ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 26);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("     \\ `\\");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("_  ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("))  :  ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(";     |  ");
                    Console.ForegroundColor = p2Dark;
                    Console.Write("|      ): ");
                    Console.ForegroundColor = p2Color;
                    Console.Write(": ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 27);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("    (`-.-'\\ ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("||  |\\");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" \\   ` ; ");
                    Console.ForegroundColor = p2Dark;
                    Console.Write(" ;       | ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("| ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 28);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("     \\-_   `");
                    Console.ForegroundColor = p2Dark;
                    Console.Write(";;._   ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("( `  / ");
                    Console.ForegroundColor = p2Dark;
                    Console.Write(" /_       | ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("| ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 29);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("      `-.-.");
                    Console.ForegroundColor = p2Dark;
                    Console.Write("// ,'`-._");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("\\__/");
                    Console.ForegroundColor = p2Dark;
                    Console.Write("_,'         ; ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("| ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 30);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("         \\");
                    Console.ForegroundColor = p2Dark;
                    Console.Write(":: :     /     `     ,   /  ");
                    Console.ForegroundColor = p2Color;
                    Console.Write("| ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 31);
                    Console.ForegroundColor = p2Dark;
                    Console.Write("          || |    (        ,' /   /  ");
                    Console.ForegroundColor = p2Color;
                    Console.Write(" | ");

                    Console.SetCursorPosition(Console.LargestWindowWidth - 53, 32);
                    Console.ForegroundColor = p2Dark;
                    Console.Write("          ||                ,'   /   ");
                    Console.ForegroundColor = p2Color;
                    Console.Write(" | ");
                }
                void DrawStats(bool online=false) {
                    if(online) { 
                        Draw("Fetching your HP", 0, 0, ConsoleColor.White);
                        Draw(netManager.Send("11000100").GetAwaiter().GetResult(), 0, 0, ConsoleColor.White);
                        yourHp = Convert.ToInt32(StringToBinary(netManager.Send("11000100").GetAwaiter().GetResult()), 2);
                        Draw("Fetching your Mana", 0, 1, ConsoleColor.White);
                        yourMana = Convert.ToInt32(StringToBinary(netManager.Send("11000101").GetAwaiter().GetResult()), 2);
                        Draw("Fetching your Stamina", 0, 2, ConsoleColor.White);
                        yourStamina = Convert.ToInt32(StringToBinary(netManager.Send("11000110").GetAwaiter().GetResult()), 2);
                        Draw("Fetching enemy's HP", 0, 3, ConsoleColor.White);
                        enemyHp = Convert.ToInt32(StringToBinary(netManager.Send("11000111").GetAwaiter().GetResult()), 2);
                        Draw("Fetching enemy's Mana", 0, 4, ConsoleColor.White);
                        enemyMana = Convert.ToInt32(StringToBinary(netManager.Send("11001000").GetAwaiter().GetResult()), 2);
                        Draw("Fetching enemy's Stamina", 0, 5, ConsoleColor.White);
                        enemyStamina = Convert.ToInt32(StringToBinary(netManager.Send("11001001").GetAwaiter().GetResult()), 2);
                        Draw("Fetching enemy's Name", 0, 6, ConsoleColor.White);
                        enemyname = netManager.Send("11001001").GetAwaiter().GetResult();
                        Draw(yourHp.ToString() + "               ", 0, 0, ConsoleColor.White);
                        Draw(yourMana.ToString() + "               ", 0, 1, ConsoleColor.White);
                        Draw(yourStamina.ToString() + "               ", 0, 2, ConsoleColor.White);
                        Draw(enemyHp.ToString() + "               ", 0, 3, ConsoleColor.White);
                        Draw(enemyMana.ToString() + "               ", 0, 4, ConsoleColor.White);
                        Draw(enemyStamina.ToString() + "               ", 0, 5, ConsoleColor.White);
                        Draw(enemyname + "               ", 0, 6, ConsoleColor.White);

                        string tmpEnName = enemyname + " Stats";
                        string tmpYoName = yourname + " Stats";
                        Draw("──────────────────", 17, 1, p1Dark);
                        Draw(tmpYoName, 27 - (tmpYoName.Length/2), 1, p1Dark);
                        Draw("HP:                                ", 17, 2, ConsoleColor.Green);
                        Draw($"{yourHp}%", 31, 2, p1Color);
                        Draw("Mana:                              ", 17, 3, ConsoleColor.Cyan);
                        Draw($"{yourMana}%", 31, 3, p1Color);
                        Draw("Stamina:                           ", 17, 4, ConsoleColor.DarkYellow);
                        Draw($"{yourStamina}%", 31, 4, p1Color);
                        Draw("──────────────────", 17, 5, p1Dark);

                        Draw("──────────────────", Console.LargestWindowWidth - 36, 1, p2Dark);
                        Draw(tmpEnName, 27 - (tmpEnName.Length / 2), 1, p2Dark);
                        Draw("HP:                                ", Console.LargestWindowWidth - 36, 2, ConsoleColor.Green);
                        Draw($"{enemyHp}%", Console.LargestWindowWidth - 22, 2, p2Color);
                        Draw("Mana:                              ", Console.LargestWindowWidth - 36, 3, ConsoleColor.Cyan);
                        Draw($"{enemyMana}%", Console.LargestWindowWidth - 22, 3, p2Color);
                        Draw("Stamina:                           ", Console.LargestWindowWidth - 36, 4, ConsoleColor.DarkYellow);
                        Draw($"{enemyStamina}%", Console.LargestWindowWidth - 22, 4, p2Color);
                        Draw("──────────────────", Console.LargestWindowWidth - 36, 5, p2Dark);
                    } else {
                        Draw("────Your Stats────", 17, 1, p1Dark);
                        Draw("HP:                                ", 17, 2, ConsoleColor.Green);
                        Draw($"{yourHp}%", 31, 2, p1Color);
                        Draw("Mana:                              ", 17, 3, ConsoleColor.Cyan);
                        Draw($"{yourMana}%", 31, 3, p1Color);
                        Draw("Stamina:                           ", 17, 4, ConsoleColor.DarkYellow);
                        Draw($"{yourStamina}%", 31, 4, p1Color);
                        Draw("──────────────────", 17, 5, p1Dark);

                        Draw("────Enemy Stats───", Console.LargestWindowWidth - 36, 1, p2Dark);
                        Draw("HP:                                ", Console.LargestWindowWidth - 36, 2, ConsoleColor.Green);
                        Draw($"{enemyHp}%", Console.LargestWindowWidth - 22, 2, p2Color);
                        Draw("Mana:                              ", Console.LargestWindowWidth - 36, 3, ConsoleColor.Cyan);
                        Draw($"{enemyMana}%", Console.LargestWindowWidth - 22, 3, p2Color);
                        Draw("Stamina:                           ", Console.LargestWindowWidth - 36, 4, ConsoleColor.DarkYellow);
                        Draw($"{enemyStamina}%", Console.LargestWindowWidth - 22, 4, p2Color);
                        Draw("──────────────────", Console.LargestWindowWidth - 36, 5, p2Dark);
                    }
                }

                //// Offline Game Begin //// 
                while (enemyHp­ >= 1 && onGoingGame == true && yourHp­ >= 1 && !onlineMode)
                {
                    Console.Title = "Rendering";
                    // Render the Player and enemy's Stats
                    Console.CursorVisible = false;


                    DrawStats();
                    // Render the Player and enemy's Players

                    DrawCharacters();
                    curPos = 37;

                    while (yourTurn)
                    {
                        Console.ForegroundColor = p1Color;
                        Console.Title = "Your Turn!";
                        Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                        Console.Write("       Your Turn!       ");
                        Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                        Console.Write("------------------------");
                        Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                        Console.Write("                        ");
                        if (concealAttacks)
                        {
                            curPos = 0;
                            Draw("1        Attack          30% Stamina; 0-15 Dmg          \n2     Magic Attack       50% Stamina; 30% Mana; 35 Dmg  \n3        Defend          Nothing required; 0-5 Def      \n4        Shield          -10% Stamina; 0-20 Def         \n5     Magic Shield       -20% Mana; -40% Stamina; 45 Def\n6         Pray           Nothing required; Good luck!   \n7         Heal           -50% Mana; -10% Stamina; +30HP ", consoleWidthCenter - 12, 37, p1Color);
                        }
                        else
                        {
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            if (curPos == 37)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("         Attack          30% Stamina; 0-15 Dmg          ");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("         Attack                                         ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            if (curPos == 38)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("      Magic Attack       50% Stamina; 30% Mana; 35 Dmg  ");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("      Magic Attack                                      ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            if (curPos == 39)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("         Defend          Nothing required; 0-5 Def      ");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("         Defend                                         ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            if (curPos == 40)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("         Shield          -10% Stamina; 0-20 Def         ");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("         Shield                                         ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            if (curPos == 41)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("      Magic Shield       -20% Mana; -40% Stamina; 45 Def");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("      Magic Shield                                      ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            if (curPos == 42)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("          Pray           Nothing required; Good luck!   ");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("          Pray                                          ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            if (curPos == 43)
                            {
                                Console.ForegroundColor = p1Dark;
                                Console.Write("          Heal           -50% Mana; -10% Stamina; +30HP ");
                                Console.ForegroundColor = p1Color;
                            }
                            else
                            {
                                Console.Write("          Heal                                          ");
                            }
                        }
                        Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                        Console.Write("                        ");
                        Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                        Console.Write("------------------------");
                        Console.SetCursorPosition(consoleWidthCenter - 12, curPos);
                        Console.ForegroundColor = p1Dark;
                        Console.Write("> ");
                        ConsoleKey nextinput = Console.ReadKey(intercept: true).Key;
                        if (concealAttacks)
                        {
                            if(nextinput == ConsoleKey.D1 || nextinput == ConsoleKey.NumPad1)
                            {
                                yourAttack = "attack";
                                yourTurn = false;
                            }
                            else if (nextinput == ConsoleKey.D2 || nextinput == ConsoleKey.NumPad2)
                            {
                                yourAttack = "magicAttack";
                                yourTurn = false;
                            }
                            else if (nextinput == ConsoleKey.D3 || nextinput == ConsoleKey.NumPad3)
                            {
                                yourAttack = "defend";
                                yourTurn = false;
                            }
                            else if (nextinput == ConsoleKey.D4 || nextinput == ConsoleKey.NumPad4)
                            {
                                yourAttack = "shield";
                                yourTurn = false;
                            }
                            else if (nextinput == ConsoleKey.D5 || nextinput == ConsoleKey.NumPad5)
                            {
                                yourAttack = "magicShield";
                                yourTurn = false;
                            }
                            else if (nextinput == ConsoleKey.D6 || nextinput == ConsoleKey.NumPad6)
                            {
                                yourAttack = "pray";
                                yourTurn = false;
                            }
                            else if (nextinput == ConsoleKey.D7 || nextinput == ConsoleKey.NumPad7)
                            {
                                yourAttack = "heal";
                                yourTurn = false;
                            }
                        }
                        else
                        {
                            if (nextinput == ConsoleKey.DownArrow || nextinput == ConsoleKey.S)
                            {
                                if (curPos != 43)
                                {
                                    curPos++;
                                }
                                else
                                {
                                    curPos = 37;
                                }
                            }
                            else if (nextinput == ConsoleKey.UpArrow || nextinput == ConsoleKey.W)
                            {
                                if (curPos != 37)
                                {
                                    curPos--;
                                }
                                else
                                {
                                    curPos = 43;
                                }
                            }
                            else if (nextinput == ConsoleKey.RightArrow || nextinput == ConsoleKey.Enter || nextinput == ConsoleKey.Spacebar || nextinput == ConsoleKey.D)
                            {
                                if (curPos == 37) yourAttack = "attack";
                                else if (curPos == 38) yourAttack = "magicAttack";
                                else if (curPos == 39) yourAttack = "defend";
                                else if (curPos == 40) yourAttack = "shield";
                                else if (curPos == 41) yourAttack = "magicShield";
                                else if (curPos == 42) yourAttack = "pray";
                                else if (curPos == 43) yourAttack = "heal";
                                yourTurn = false;
                            }
                            if (debugCheats)
                            {
                                if (nextinput == ConsoleKey.D2)
                                {
                                    enemyHp = enemyHp - 12;
                                    enemyStamina = enemyStamina - 12;
                                    enemyMana = enemyMana - 12;
                                    Console.Title = Convert.ToString(enemyHp);
                                    DrawStats();
                                }
                                else if (nextinput == ConsoleKey.D1)
                                {
                                    yourHp = yourHp - 12;
                                    yourStamina = yourStamina - 12;
                                    yourMana = yourMana - 12;
                                    Console.Title = Convert.ToString(yourHp);
                                    DrawStats();
                                }
                                else if (nextinput == ConsoleKey.D4)
                                {
                                    enemyHp = -2147483000;
                                    enemyStamina = -2147483000;
                                    enemyMana = -2147483000;
                                    Console.Title = Convert.ToString(enemyHp);
                                    DrawStats();
                                }
                                else if (nextinput == ConsoleKey.D3)
                                {
                                    yourHp = -2147483000;
                                    yourStamina = -2147483000;
                                    yourMana = -2147483000;
                                    Console.Title = Convert.ToString(yourHp);
                                    DrawStats();
                                }
                            }
                        }
                    }
                    curPos = 37;
                    while (yourTurn == false)
                    {
                        Console.Title = $"{enemyname}'s Turn";
                        void draw()
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            if (curPos == 37)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("         Attack          30% Stamina; 0-15 Dmg          ");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("         Attack                                         ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            if (curPos == 38)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("      Magic Attack       50% Stamina; 30% Mana; 35 Dmg  ");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("      Magic Attack                                      ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            if (curPos == 39)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("         Defend          Nothing required; 0-5 Def      ");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("         Defend                                         ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            if (curPos == 40)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("         Shield          -10% Stamina; 0-20 Def         ");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("         Shield                                         ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            if (curPos == 41)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("      Magic Shield       -20% Mana; -40% Stamina; 45 Def");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("      Magic Shield                                      ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            if (curPos == 42)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("          Pray           Nothing required; Good luck!   ");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("          Pray                                          ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            if (curPos == 43)
                            {
                                Console.ForegroundColor = p2Dark;
                                Console.Write("          Heal           -50% Mana; -10% Stamina; +30HP ");
                                Console.ForegroundColor = p2Color;
                            }
                            else
                            {
                                Console.Write("          Heal                                          ");
                            }
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, curPos);
                            Console.ForegroundColor = p2Dark;
                            Console.Write("> ");
                        }

                        draw();
                        if (multiplayerMode == false)
                        {
                            enemyAtt = random.Next(100);
                            Thread.Sleep(random.Next(250, 2000));
                            curPos = random.Next(37, 42);
                            draw();
                            Thread.Sleep(random.Next(250, 2000));
                            curPos = random.Next(37, 42);
                            draw();
                            Thread.Sleep(random.Next(250, 2000));
                            curPos = random.Next(37, 42);
                            draw();
                            Thread.Sleep(random.Next(250, 2000));
                            if (enemyHp <= 20 && enemyStamina <= 10 && enemyMana < 50)
                            {
                                if (enemyAtt <= 60) curPos = 42;
                                else if (enemyAtt > 60 && enemyAtt <= 70) curPos = 37;
                                else if (enemyAtt > 70 && enemyAtt <= 80) curPos = 38;
                                else if (enemyAtt > 80 && enemyAtt <= 90) curPos = 39;
                                else if (enemyAtt > 90 && enemyAtt <= 100) curPos = 40;
                                draw();
                            }
                            else if (enemyHp <= 50 && enemyMana >= 50)
                            {
                                if (enemyAtt <= 50) curPos = 43;
                                else if (enemyAtt > 50 && enemyAtt <= 60) curPos = 37;
                                else if (enemyAtt > 60 && enemyAtt <= 70) curPos = 38;
                                else if (enemyAtt > 70 && enemyAtt <= 80) curPos = 39;
                                else if (enemyAtt > 80 && enemyAtt <= 90) curPos = 40;
                                else if (enemyAtt > 90 && enemyAtt <= 100) curPos = 42;
                                draw();
                            }
                            else if (enemyStamina >= 30)
                            {
                                if (enemyAtt <= 52) curPos = 37;
                                else if (enemyAtt > 52 && enemyAtt <= 68) curPos = 39;
                                else if (enemyAtt > 68 && enemyAtt <= 84) curPos = 40;
                                else if (enemyAtt > 84 && enemyAtt <= 100) curPos = 41;
                                draw();
                            }
                            else
                            {
                                if (enemyAtt <= 80) curPos = 39;
                                else if (enemyAtt > 80 && enemyAtt <= 100) curPos = 42;
                                draw();
                            }
                            Thread.Sleep(random.Next(250, 2000));
                            if (curPos == 37) enemyAttack = "attack";
                            else if (curPos == 38) enemyAttack = "magicAttack";
                            else if (curPos == 39) enemyAttack = "defend";
                            else if (curPos == 40) enemyAttack = "shield";
                            else if (curPos == 41) enemyAttack = "magicShield";
                            else if (curPos == 42) enemyAttack = "pray";
                            else if (curPos == 43) enemyAttack = "heal";
                            yourTurn = true;
                        }
                        else
                        {
                            ConsoleKey nextinput = Console.ReadKey(intercept: true).Key;
                            if (nextinput == ConsoleKey.DownArrow || nextinput == ConsoleKey.S)
                            {
                                if (curPos != 43)
                                {
                                    curPos++;
                                    draw();
                                }
                                else
                                {
                                    curPos = 37;
                                }
                            }
                            else if (nextinput == ConsoleKey.UpArrow || nextinput == ConsoleKey.W)
                            {
                                if (curPos != 37)
                                {
                                    curPos--;
                                    draw();
                                }
                                else
                                {
                                    curPos = 43;
                                }
                            }
                            else if (nextinput == ConsoleKey.RightArrow || nextinput == ConsoleKey.Enter || nextinput == ConsoleKey.Spacebar || nextinput == ConsoleKey.D)
                            {
                                if (curPos == 37) enemyAttack = "attack";
                                else if (curPos == 38) enemyAttack = "magicAttack";
                                else if (curPos == 39) enemyAttack = "defend";
                                else if (curPos == 40) enemyAttack = "shield";
                                else if (curPos == 41) enemyAttack = "magicShield";
                                else if (curPos == 42) enemyAttack = "pray";
                                else if (curPos == 43) enemyAttack = "heal";
                                yourTurn = true;
                            }
                        }
                    }
                    int yourIncomingDmg = 0;
                    int enemyIncomingDmg = 0;
                    Console.Title = "Rollout";

                    Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                    Console.Write("                                                        ");
                    Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                    Console.Write("                                                        ");
                    Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                    Console.Write("                                                        ");
                    Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                    Console.Write("                                                        ");
                    Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                    Console.Write("                                                        ");
                    Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                    Console.Write("                                                        ");
                    Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                    Console.Write("                                                        ");

                    if (enemyAttack == "pray" || yourAttack == "pray")
                    {
                        if (enemyAttack == "pray")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("          Pray          ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("    Enemy is Praying    ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------"); ;
                            Thread.Sleep(1000);
                            int worked = random.Next(100);
                            if (worked <= 20)
                            {
                                enemyHp = enemyHp + 20;
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Success         ");
                            }
                            else if (worked > 20)
                            {
                                enemyHp = enemyHp - 10;
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "pray")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("      Your's Turn       ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("          Pray          ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("     You are Praying    ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int worked = random.Next(100);
                            if (worked <= 20)
                            {
                                yourHp = yourHp + 20;
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Success         ");
                            }
                            else if (worked > 20)
                            {
                                yourHp = yourHp - 10;
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                    }
                    if (enemyAttack == "heal" || yourAttack == "heal")
                    {
                        if (enemyAttack == "heal")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("          Heal          ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("    Enemy is Healing    ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            if (enemyMana >= 50)
                            {
                                enemyHp = enemyHp + 30;
                                enemyMana = enemyMana - 50;
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Success         ");
                                enemyStamina -= 10;
                            }
                            else if (enemyMana < 50)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "heal")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("      Your's Turn       ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("          Heal          ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("     You are Healing    ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            if (yourMana >= 50)
                            {
                                yourHp = yourHp + 30;
                                yourMana = yourMana - 50;
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Success         ");
                                enemyStamina -= 10;
                            }
                            else if (yourMana < 50)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    if (enemyAttack == "attack" || yourAttack == "attack")
                    {
                        if (enemyAttack == "attack")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("         Attack         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("   Enemy is Attacking   ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(15);
                            int luck = random.Next(0, 100);
                            if (randomCrits)
                            {
                                if(luck­<=3) {
                                    dmg *= 3;
                                }
                            }
                            if (enemyStamina >= 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                if (randomSpread)
                                {
                                    if (luck­ <= 3)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    Console.Write($"         {dmg} Dmg        ");
                                    Console.ForegroundColor = p2Color;
                                    yourIncomingDmg += dmg;
                                }
                                else
                                {
                                    if (luck­ <= 3)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write($"         30 Dmg        ");
                                        yourIncomingDmg += 30;
                                    }
                                    else
                                    {
                                        Console.Write($"         10 Dmg        ");
                                        yourIncomingDmg += 10;
                                    }
                                    Console.ForegroundColor = p2Color;
                                }
                                enemyStamina -= 30;
                            }
                            else if (enemyStamina < 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "attack")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("      Your's Turn       ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("         Attack         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("    You are Attacking   ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(15);
                            int luck = random.Next(0, 100);
                            if (randomCrits)
                            {
                                if (luck­ <= 3)
                                {
                                    dmg *= 3;
                                }
                            }
                            if (yourStamina >= 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                if (randomSpread)
                                {
                                    if (luck­ <= 3)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    Console.Write($"         {dmg} Dmg        ");
                                    Console.ForegroundColor = p1Color;
                                    enemyIncomingDmg += dmg;
                                }
                                else
                                {
                                    if (luck­ <= 3)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write($"         30 Dmg        ");
                                        enemyIncomingDmg += 30;
                                    }
                                    else
                                    {
                                        Console.Write($"         10 Dmg        ");
                                        enemyIncomingDmg += 10;
                                    }
                                    Console.ForegroundColor = p1Color;
                                }
                                yourStamina -= 30;
                            }
                            else if (yourStamina < 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    if (enemyAttack == "shield" || yourAttack == "shield")
                    {
                        if (enemyAttack == "shield")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("         Shield         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("   The Enemy Shielded   ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(20);
                            if (enemyStamina >= 10)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                if (randomSpread)
                                {
                                    Console.Write($"         {dmg} Dmg        ");
                                    enemyIncomingDmg -= dmg;
                                }
                                else
                                {
                                    Console.Write($"         10 Dmg        ");
                                    enemyIncomingDmg -= 10;
                                }
                                enemyStamina -= 10;
                            }
                            else if (enemyStamina < 10)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "shield")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("       Your Turn        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("         Shield         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("      You Shielded      ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(20);
                            if (yourStamina >= 10)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                if (randomSpread)
                                {
                                    Console.Write($"        {dmg} Def       ");
                                    yourIncomingDmg -= dmg;
                                }
                                else
                                {
                                    Console.Write($"        10 Def       ");
                                    yourIncomingDmg -= 10;
                                }
                                yourStamina -= 10;
                            }
                            else if (yourStamina < 10)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    if (enemyAttack == "magicAttack" || yourAttack == "magicAttack")
                    {
                        if (enemyAttack == "magicAttack")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("      Magic Attack      ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write(" The Enemy Sends a Beam ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);

                            if (enemyStamina >= 50 && enemyMana >= 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write($"        35 Dmg       ");
                                yourIncomingDmg += 35;
                                enemyMana -= 30;
                                enemyStamina -= 50;
                            }
                            else if (enemyStamina < 50 || enemyMana < 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "magicAttack")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("       Your Turn        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("      Magic Attack      ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("     You Send a Beam    ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);

                            if (yourStamina >= 50 && yourMana >= 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write($"        35 Dmg       ");
                                enemyIncomingDmg += 35;
                                yourMana -= 30;
                                yourStamina -= 50;
                            }
                            else if (yourStamina < 50 || yourMana < 30)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    if (enemyAttack == "defend" || yourAttack == "defend")
                    {
                        if (enemyAttack == "defend")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("         Defend         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("    The Enemy Defends   ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(5);
                            if (enemyIncomingDmg >= 35)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write($"{dmg} Def (-30% Stamina)");
                                enemyIncomingDmg -= dmg;
                                enemyStamina -= 30;

                            }
                            else
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write($"        {dmg} Def       ");
                                enemyIncomingDmg -= dmg;
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "defend")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("       Your Turn        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("         Defend         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("       You Defend       ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(5);
                            if (yourIncomingDmg >= 35)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                if (randomSpread)
                                {
                                    Console.Write($"{dmg} Def (-30% Stamina)");
                                    yourIncomingDmg -= dmg;
                                }
                                else
                                {
                                    Console.Write($"3 Def (-30% Stamina)");
                                    yourIncomingDmg -= 3;
                                }
                                yourStamina -= 30;

                            }
                            else
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                if (randomSpread)
                                {
                                    Console.Write($"        3 Def       ");
                                    yourIncomingDmg -= 3;
                                }
                                else
                                {
                                    Console.Write($"        {dmg} Def       ");
                                    yourIncomingDmg -= dmg;
                                }

                            }
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    if (enemyAttack == "magicShield" || yourAttack == "magicShield")
                    {
                        if (enemyAttack == "magicShield")
                        {
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("    Opponent's Turn     ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("      Magic Shield      ");
                            Console.SetCursorPosition(consoleWidthCenter - 13, 39);
                            Console.Write("  Enemy Protects itself ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            int dmg = random.Next(20, 35);
                            if (enemyMana >= 20 && enemyStamina >= 40)
                            {
                                if (enemyMana >= 20 && enemyStamina >= 40)
                                {
                                    Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                    Console.Write($"         45 Def         ");
                                    enemyIncomingDmg -= 45;
                                    enemyStamina -= 40;
                                    enemyMana -= 20;
                                }
                            }
                            else if (enemyStamina < 40 || enemyMana < 20)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        if (yourAttack == "magicShield")
                        {
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 12, 34);
                            Console.Write("       Your Turn        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 35);
                            Console.Write("------------------------");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 36);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 37);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 38);
                            Console.Write("      Magic Shield      ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 39);
                            Console.Write("  You Protect yourself  ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                            Console.Write("         XXXXXX         ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 41);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 42);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 43);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 44);
                            Console.Write("                        ");
                            Console.SetCursorPosition(consoleWidthCenter - 12, 45);
                            Console.Write("------------------------");
                            Thread.Sleep(1000);
                            if (yourMana >= 20 && yourStamina >= 40)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write($"         45 Def         ");
                                yourIncomingDmg -= 45;
                                yourStamina -= 40;
                                yourMana -= 20;
                            }
                            else if (yourStamina < 40 || yourMana < 20)
                            {
                                Console.SetCursorPosition(consoleWidthCenter - 12, 40);
                                Console.Write("         Failed         ");
                            }
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    if (enemyIncomingDmg < 0)
                    {
                        enemyIncomingDmg = 0;
                    }
                    if (yourIncomingDmg < 0)
                    {
                        yourIncomingDmg = 0;
                    }
                    yourHp -= yourIncomingDmg;
                    enemyHp -= enemyIncomingDmg;

                    if (enemyStamina < staminaAmount)
                    {
                        enemyStamina += 10;
                    }
                    if (yourStamina < staminaAmount)
                    {
                        yourStamina += 10;
                    }
                    if (enemyMana < manaAmount)
                    {
                        enemyMana += 2;
                    }
                    if (yourMana < manaAmount)
                    {
                        yourMana += 2;
                    }
                    if (enemyHp <= 0 && yourHp <= 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(consoleWidthCenter - 11, consoleHeightCenter - 2);
                        Console.Write("|''||''|             ");
                        Console.SetCursorPosition(consoleWidthCenter - 11, consoleHeightCenter - 1);
                        Console.Write("   ||     ''         ");
                        Console.SetCursorPosition(consoleWidthCenter - 11, consoleHeightCenter);
                        Console.Write("   ||     ||  .|''|, ");
                        Console.SetCursorPosition(consoleWidthCenter - 11, consoleHeightCenter + 1);
                        Console.Write("   ||     ||  ||..|| ");
                        Console.SetCursorPosition(consoleWidthCenter - 11, consoleHeightCenter + 2);
                        Console.Write("  .||.   .||. `|...  ");
                        Console.Title = "Tie (?)";
                        Thread.Sleep(1000);
                        Console.ReadKey();
                        onGoingGame = false;
                    }
                    if (multiplayerMode && onGoingGame)
                    {
                        if (yourHp <= 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 33, consoleHeightCenter - 2);
                            Console.Write("'\\\\  //`                       '||`                              ");
                            Console.SetCursorPosition(consoleWidthCenter - 33, consoleHeightCenter - 1);
                            Console.Write("  \\\\//                          ||                               ");
                            Console.SetCursorPosition(consoleWidthCenter - 33, consoleHeightCenter);
                            Console.Write("   ||    .|''|, '||  ||`        ||  .|''|, ('''' .|''|,          ");
                            Console.SetCursorPosition(consoleWidthCenter - 33, consoleHeightCenter + 1);
                            Console.Write("   ||    ||  ||  ||  ||         ||  ||  ||  `'') ||..||          ");
                            Console.SetCursorPosition(consoleWidthCenter - 33, consoleHeightCenter + 2);
                            Console.Write("  .||.   `|..|'  `|..'|.       .||. `|..|' `...' `|...  .. .. .. ");
                            Console.Title = "You Lose...";
                            Thread.Sleep(1000);
                            Console.ReadKey();
                            onGoingGame = false;
                        }
                        else if (enemyHp <= 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 30, consoleHeightCenter - 2);
                            Console.Write("'\\\\  //`                                                || ");
                            Console.SetCursorPosition(consoleWidthCenter - 30, consoleHeightCenter - 1);
                            Console.Write("  \\\\//                                     ''           ||");
                            Console.SetCursorPosition(consoleWidthCenter - 30, consoleHeightCenter);
                            Console.Write("   ||    .|''|, '||  ||`       '\\\\    //`  ||  `||''|,  ||");
                            Console.SetCursorPosition(consoleWidthCenter - 30, consoleHeightCenter + 1);
                            Console.Write("   ||    ||  ||  ||  ||          \\\\/\\//    ||   ||  ||  ||");
                            Console.SetCursorPosition(consoleWidthCenter - 30, consoleHeightCenter + 2);
                            Console.Write("  .||.   `|..|'  `|..'|.          \\/\\/    .||. .||  ||. ..");
                            Console.Title = "You Win!";
                            Thread.Sleep(1000);
                            Console.ReadKey();
                            onGoingGame = false;
                        }
                    }
                    else if (onGoingGame)
                    {
                        if (yourHp <= 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = p2Color;
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter - 2);
                            Console.Write("'||'''|,  ''|,       '||      ||`                     ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter - 1);
                            Console.Write(" ||   || '  ||        ||      ||   ''                 ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter);
                            Console.Write(" ||...|'   .|'        ||  /\\  ||   ||  `||''|,  ('''' ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter + 1);
                            Console.Write(" ||       //           \\\\//\\\\//    ||   ||  ||   `'') ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter + 2);
                            Console.Write(".||      ((...          \\/  \\/    .||. .||  ||. `...' ");
                            Console.Title = "P2 Wins!";
                            Thread.Sleep(1000);
                            Console.ReadKey();
                            onGoingGame = false;
                        }
                        else if (enemyHp <= 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = p1Color;
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter - 2);
                            Console.Write("'||'''|,  ||        '||      ||`                     ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter - 1);
                            Console.Write(" ||   || '||         ||      ||   ''                 ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter);
                            Console.Write(" ||...|'  ||         ||  /\\  ||   ||  `||''|,  ('''' ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter + 1);
                            Console.Write(" ||       ||          \\\\//\\\\//    ||   ||  ||   `'') ");
                            Console.SetCursorPosition(consoleWidthCenter - 27, consoleHeightCenter + 2);
                            Console.Write(".||      .||.          \\/  \\/    .||. .||  ||. `...' ");
                            Console.Title = "P1 Wins!";
                            Thread.Sleep(1000);
                            Console.ReadKey();
                            onGoingGame = false;
                        }
                    }
                }

                //// Online Game Begin ////
                void StartOnlineGame() {
                    Console.Title = "Fetching game info...";
                    string p2ColorT = StringToBinary(netManager.Send("11000011").GetAwaiter().GetResult());
                    if (p2ColorT == "11010001") p2Color = ConsoleColor.Blue;
                    else if (p2ColorT == "11010010") p2Color = ConsoleColor.Green;
                    else if (p2ColorT == "11010011") p2Color = ConsoleColor.Cyan;
                    else if (p2ColorT == "11010100") p2Color = ConsoleColor.Red;
                    else if (p2ColorT == "11010101") p2Color = ConsoleColor.Magenta;
                    else if (p2ColorT == "11010110") p2Color = ConsoleColor.Yellow;
                    Draw(p2ColorT, 0, 0, ConsoleColor.White);
                    Draw(p2Color.ToString(), 0, 1, ConsoleColor.White);
                    Console.ReadKey();
                    ConsoleColor p1Dark = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "Dark" + p1Color, true);
                    ConsoleColor p2Dark = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), "Dark" + p2Color, true);
                    DrawCharacters();
                    DrawStats(true);
                    while (onlineMode) {
                        
                        
                        Console.Title = "Rendering";
                        
                        // Render the Player and enemy's Stats
                        Console.CursorVisible = false;
                    }
                }
            }
        }
    }
    public class NetManager {
        public NetManager() { }
        public async Task<string> Send(string request, bool isByte = true) {
            var hostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("middlemouse.click");
            IPAddress hostIpAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(hostIpAddress, 42069);

            using Socket client = new Socket(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            try {
                await client.ConnectAsync(ipEndPoint);
            } catch (Exception ex) {
                return "Connection error: " + ex.Message;
            }

            var response = "";
            while (true) {
                if (isByte) {
                    byte[] messageBytes = new byte[] { Convert.ToByte(request, 2) };
                    _ = await client.SendAsync(messageBytes, SocketFlags.None);
                } else {
                    var messageBytes = Encoding.UTF8.GetBytes(request);
                    _ = await client.SendAsync(messageBytes, SocketFlags.None);
                }
                var buffer = new byte[12];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                if (buffer[1] == 0) {
                    response = Convert.ToString(buffer).PadLeft(8, '0');
                } else {
                    response = Encoding.UTF8.GetString(buffer, 0, received);
                }
                foreach (var item in buffer) {
                    Console.WriteLine(item.ToString());
                }
                break;
            }
            Console.ReadKey();
            client.Shutdown(SocketShutdown.Both);
            return response;
        }
    }
}
