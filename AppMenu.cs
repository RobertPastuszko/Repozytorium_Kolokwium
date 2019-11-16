using System;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

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
            int lettersInFile = 0;
            int wordsInFile = 0;
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
                                StreamReader file = new StreamReader("6.TXT");
                                lettersInFile = CountLetters(file);
                            }
                            catch (FileNotFoundException)
                            {
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                lettersInFile = CountWords();
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

        static int CountLetters(StreamReader file)
        {
            string fileReaded = file.ReadToEnd();
            Regex regex = new Regex(@"[^A-Za-z]");
            fileReaded.Trim();
            fileReaded = regex.Replace(fileReaded, "");
            int len = fileReaded.Length;
            return len;
        }
        static int CountWords()
        {
            if (!File.Exists("6.Txt"))
            {
                throw new FileNotFoundException();
            }

            string fileRead = new StreamReader("6.Txt").ReadToEnd();
            bool HasWordEnded = true;
            int counter = 0;
            foreach (char c in fileRead)
            {
                if(HasWordEnded && Char.IsLetter(c))
                {
                    counter++;
                    HasWordEnded = false;
                }
                if(!HasWordEnded && Char.IsWhiteSpace(c))
                {
                    HasWordEnded = true;
                }
            }
            return counter;
        }


        static public void Main(String[] args)
        {
            MainMenu();
        }
    }
}
