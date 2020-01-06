﻿using System;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace MaxSuperHiperMegaRambo5
{
    /// <summary>
    /// This is main menu of ours school project
    /// </summary>

    public class Menu
    {
        private static readonly char[] alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                                   'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        private static readonly char[] vowels = { 'a', 'e', 'y', 'i', 'o', 'ą', 'ę', 'u', 'ó' };

        public static void MainMenu()
        {

            int cndt = 0;
            int lettersInFile = 0;
            int wordsInFile = 0;
            int punctuationsInFile = 0;
            int sentencesInFile = 0;
            Dictionary<char, int> report;

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
                        GetFile();
                        break;
                    case 2:
                        {
                            try
                            {
                                lettersInFile = CountLetters(out int consonants, out int vowel);
                                Console.WriteLine("There are {0} letters in given file.", lettersInFile);
                            }
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
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
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }

                    case 4:
                        {
                            try
                            {
                                punctuationsInFile = CountPunctuation();
                                Console.WriteLine("There are {0} punctuation marks in given file.", punctuationsInFile);
                            }
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;
                        }
                    case 5:
                        {
                            try
                            {
                                sentencesInFile = CountSentences();
                                Console.WriteLine("There are {0} sentences in given file.", sentencesInFile);
                            }
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                report = Report();
                                foreach (KeyValuePair<char, int> item in report)
                                {
                                    Console.WriteLine("Letter: {0}\tQuantity: {1}", item.Key, item.Value);
                                }
                            }
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                    case 7:
                        {
                            try
                            {
                                Statistics();
                                if (File.Exists("statystyki.txt"))
                                    Console.WriteLine("Statistics succesfully written to file \"statystyki.txt\"");
                                else
                                    Console.WriteLine("Statistics weren't succesfully written to file.");
                            }
                            catch (FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
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

        static int CountLetters(out int consontantsCount, out int vowelCount)
        {
            using (StreamReader stream = new StreamReader("6.Txt"))
            {
                int len = 0;
                consontantsCount = 0;
                vowelCount = 0;

                string fileReaded = stream.ReadToEnd();

                foreach (char c in fileReaded)
                {
                    if (Char.IsLetter(c))
                    {
                        len++;
                        if (vowels.Contains(c))
                        {
                            vowelCount++;
                        }
                        else
                        {
                            consontantsCount++;
                        }
                    }
                }
                return len;
            }
        }

        static void Exit()
        {
            if (File.Exists("6.txt"))
            {
                Console.WriteLine("Deleting file 6.txt");
                File.Delete("6.txt");
            }
            if (File.Exists("statystyki.txt"))
            {
                Console.WriteLine("Deleting file statystyki.txt");
                File.Delete("statystyki.txt");
            }
        }

        static int CountWords()
        {
            using (StreamReader stream = new StreamReader("6.Txt"))
            {
                string fileRead = stream.ReadToEnd().ToString();
                int counter = 0;

                string[] splitted = fileRead.Trim().Split(' ');
                foreach (string str in splitted)
                {
                    if (str.Length > 1)
                    {
                        counter++;
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

        static Dictionary<char, int> Report()
        {
            using (StreamReader stream = new StreamReader("6.Txt"))
            {
                string fileReaded = stream.ReadToEnd();
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
                return report;
            }
        }

        static int CountSentences()
        {
            using (StreamReader stream = new StreamReader("6.Txt"))
            {
                string fileReaded = stream.ReadToEnd();
                string[] sentences = fileReaded.Split('.', '?');
                List<string> nonEmptySentence = new List<string>();
                foreach (string sentence in sentences)
                {
                    if (!String.IsNullOrWhiteSpace(sentence))
                    {
                        nonEmptySentence.Add(sentence);
                    }
                }
                int len = nonEmptySentence.Count;
                return len;
            }
        }

        static int CountPunctuation()
        {
            using (StreamReader stream = new StreamReader("6.Txt"))
            {
                string fileReaded = stream.ReadToEnd();
                fileReaded.Trim();
                int count = 0;
                foreach (char c in fileReaded)
                {
                    if (c == '.' || c == '?')
                        count++;
                }
                return count;
            }
        }

        static void Statistics()
        {
            using (StreamWriter stats = new StreamWriter("statystyki.txt"))
            {
                stats.WriteLine("There are {0} letters in given file.", CountLetters(out int consonants, out int vowels));
                stats.WriteLine();
                stats.WriteLine("There are {0} consonants in given file.", consonants);
                stats.WriteLine();
                stats.WriteLine("There are {0} vowels in given file.", vowels);
                stats.WriteLine();
                stats.WriteLine("There are {0} words in given file.", CountWords());
                stats.WriteLine();
                stats.WriteLine("There are {0} punctuation marks in given file.", CountPunctuation());
                stats.WriteLine();
                stats.WriteLine("There are {0} sentences in given file.", CountSentences());
                stats.WriteLine();
                Dictionary<char, int> report = new Dictionary<char, int>();
                report = Report();
                foreach (KeyValuePair<char, int> item in report)
                {
                    stats.WriteLine("Letter: {0}\tQuantity: {1}", item.Key, item.Value);
                }
            }
        }

        static public void Main(String[] args)
        {
            MainMenu();
        }
    }
}
