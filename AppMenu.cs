using System;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;

namespace MaxSuperHiperMegaRambo5
{
    /// <summary>
    /// This is main menu of ours school project
    /// </summary>

    public class Menu
    {
        private static readonly char[] alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                                   'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        public static void MainMenu()
        {
            int cndt = 0;
            int lettersInFile = 0;
            int wordsInFile = 0;
            int punctuationsInFile = 0;
            int sentencesInFile = 0;
            Dictionary<char, int> report;

            GetFile();

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
                                GetFile();
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
                                Console.WriteLine("There are {0} letters in given file.", lettersInFile);
                                file.Close();
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
                                wordsInFile = CountWords();
                                Console.WriteLine("There are {0} words in given file.", wordsInFile);
                            }
                            catch (FileNotFoundException)
                            {
                            }
                            break;
                        }
                   
                    case 4:
                        {
                            try
                            {
                                StreamReader file = new StreamReader("6.txt");
                                punctuationsInFile = CountPunctuation(file);
                                Console.WriteLine("There are {0} punctuation marks in given file.", punctuationsInFile);
                                file.Close();
                            }
                            catch (FileNotFoundException)
                            {
                            }
                            break;
                        }						
                    case 5:
                        {
                            try
                            {
                                StreamReader file = new StreamReader("6.txt");
                                sentencesInFile = CountSentences(file);
                                Console.WriteLine("There are {0} sentences in given file.", sentencesInFile);
                                file.Close();
                            }
                            catch (FileNotFoundException)
                            {
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                StreamReader file = new StreamReader("6.TXT");
                                report = Report(file);
                                foreach (KeyValuePair<char, int> item in report)
                                {
                                    Console.WriteLine("Letter: {0}\tQuantity: {1}", item.Key, item.Value);
                                }
                            }
                            catch (FileNotFoundException)
                            {
                            }
                            break;
                        }
                    case 7:
                        {
                            try
                            {
                                StreamReader file = new StreamReader("6.TXT");
                                Statistics(file);
                                if (File.Exists("statystyki.txt"))
                                    Console.WriteLine("Statistics succesfully written to file \"statystyki.txt\"");
                                else
                                    Console.WriteLine("Statistics weren't succesfully written to file.");
                                file.Close();
                            }
                            catch (FileNotFoundException)
                            {
                            }
                            break;
                        }
                    case 8:
                        {
                            Exit();
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
            if (File.Exists("6.txt"))
            {
                string fileReaded = file.ReadToEnd();
                Regex regex = new Regex(@"[^A-Za-z]");
                fileReaded.Trim();
                fileReaded = regex.Replace(fileReaded, "");
                int len = fileReaded.Length;
                file.Close();
                return len;
            }
            else
                throw new FileNotFoundException();
        }

        static void Exit()
        {
            if (File.Exists("6.txt"))
            {
                File.Delete("6.txt");
            }
            if (File.Exists("statystyki.txt"))
            {
                File.Delete("statystyki.txt");
            }
        }

        static int CountWords()
        {
            if (!File.Exists("6.Txt"))
            {
                throw new FileNotFoundException();
            }

            using (StreamReader stream = new StreamReader("6.Txt"))
            {
                string fileRead = stream.ReadToEnd().ToString();
                bool HasWordEnded = true;
                int counter = 0;
                foreach (char c in fileRead)
                {
                    if (HasWordEnded && Char.IsLetter(c))
                    {
                        counter++;
                        HasWordEnded = false;
                    }
                    if (!HasWordEnded && Char.IsWhiteSpace(c))
                    {
                        HasWordEnded = true;
                    }
                }
                return counter;
            }
        }

        static void GetFile()
        {
            using (WebClient Client = new WebClient())
            {
                try
                {
                    Client.DownloadFile("https://s3.zylowski.net/public/input/6.txt", @"6.txt");
                }
                catch (WebException)
                {
                    Console.WriteLine("File could not be downloaded");
                }
            }
        }

        static Dictionary<char, int> Report(StreamReader file)
        {
            if (File.Exists("6.txt"))
            {
                string fileReaded = file.ReadToEnd();
                Regex regex = new Regex(@"[^A-Za-z]");
                fileReaded = fileReaded.Trim();
                fileReaded = regex.Replace(fileReaded, "");
                fileReaded = fileReaded.ToUpper();
                Dictionary<char, int> report = new Dictionary<char, int>();
                foreach (char letter in alphabet)
                {
                    int counter = 0;
                    foreach (char letterInFile in fileReaded)
                    {
                        if (letter == letterInFile)
                            counter++;
                    }
                    report.Add(letter, counter);
                }
                file.Close();
                return report;
            }
            else
                throw new FileNotFoundException();
        }

		static int CountSentences(StreamReader file)
        {
            if (!File.Exists("6.txt"))
            {
                throw new FileNotFoundException();
            }

            string fileReaded = file.ReadToEnd();
            string[] sentences = fileReaded.Split('.', '?', '!');
            List<string> nonEmptySentence = new List<string>();
            foreach(string sentence in sentences)
            {
                if (!String.IsNullOrWhiteSpace(sentence))
                {
                    nonEmptySentence.Add(sentence);
                }
            }
            int len = nonEmptySentence.Count;
            return len;
        }

        static int CountPunctuation(StreamReader file)
        {
            if (!File.Exists("6.Txt"))
            {
                throw new FileNotFoundException();
            }

            string fileReaded = file.ReadToEnd();
            fileReaded.Trim();
            int count = 0;
            foreach (char c in fileReaded)
            {
                if (Char.IsPunctuation(c))
                    count++;
            }
            return count;
        }

        static void Statistics(StreamReader file)
        {
            if (!File.Exists("6.Txt"))
            {
                throw new FileNotFoundException();
            }
            
            StreamWriter stats = new StreamWriter("statystyki.txt");
            stats.WriteLine("There are {0} letters in given file.", CountLetters(file));
            stats.WriteLine();
            stats.WriteLine("There are {0} words in given file.", CountWords());
            stats.WriteLine();
            stats.WriteLine("There are {0} punctuation marks in given file.", CountPunctuation(file));
            stats.WriteLine();
            stats.WriteLine("There are {0} sentences in given file.", CountSentences(file));
            Dictionary<char, int> report = new Dictionary<char, int>();
            report = Report(file);
            foreach (KeyValuePair<char, int> item in report)
            {
                stats.WriteLine("Letter: {0}\tQuantity: {1}", item.Key, item.Value);
            }
            stats.Close();
        }

        static public void Main(String[] args)
        {
            MainMenu();
        }
    }
}
