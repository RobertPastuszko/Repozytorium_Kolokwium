using System;
using System.Threading;

namespace MaxSuperHiperMegaRambo5
{
    /// <summary>
    /// This is main menu of ours school project
    /// </summary>

    public class Menu
    {
        public static void MainMenu()
        {
            int cndt = 0;
            do
            {
                Console.WriteLine("Select option 1-8:");
                Console.WriteLine("1. Download file");
                Console.WriteLine("2. Count letters in file");
                Console.WriteLine("3. Count words in file");
                Console.WriteLine("4. Count punctuation marks in file");
                Console.WriteLine("5. Count sentences in file");
                Console.WriteLine("6. Generate report on usage of letters (A-Z)");
                Console.WriteLine("7. Save statistics from above points to file \"statystyki.txt\"");
                Console.WriteLine("8. Quit");
                var read = Console.ReadLine();
                try
                {
                    cndt = Convert.ToInt32(read);
                }
                catch (FormatException)
                {
                }
                Console.WriteLine("You chose {0}", read);
                if (cndt < 1 || cndt > 8) Console.WriteLine("It's not a proper choice");
                switch (cndt)
                {
                    case 1:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 5:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 7:
                        {
                            try
                            {
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                    case 8:
                        {
                            Console.WriteLine("Program terminated");
                            Thread.Sleep(1280);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            } while (cndt != 8);
        }


        static public void Main(String[] args)
        {
            MainMenu();
        }
    }
}
