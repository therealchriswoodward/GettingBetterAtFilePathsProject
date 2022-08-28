using System;
using System.IO;
using System.Collections.Generic;

namespace GettingBetterAtFilePaths
{
    class Program
    {
        static void Main(string[] args)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var testDirectoryPath = Path.Combine(desktopPath, "Test");
            var testTextFilePath = Path.Combine(testDirectoryPath, "test.txt");
            
            if(!Directory.Exists(testTextFilePath))
            {
                Directory.CreateDirectory(testDirectoryPath);
            }

            using var file = File.Open(testTextFilePath, FileMode.Append);
            using var writer = new StreamWriter(file);

            Console.WriteLine("Hello!\nThis program will create a folder on your desktop called \"Test\"");
            Console.WriteLine("It will also add a text file to that folder where we are going to save\nwhatever you type.");
            Console.WriteLine("You can type whatever you want. Hit enter for each thing you type.");
            Console.WriteLine("Hit enter twice to stop adding things to the file.");
            try
            {
                while (true)
                {
                    var line = Console.ReadLine();
                    writer.WriteLine(line);
                    if (line.Length == 0)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("That didn't work.");
            }
            finally
            {
                Console.WriteLine("Finally code block");
            }                       
            writer.Close();

            Console.WriteLine("Would you like for me to read that back to you? Y/N?");

            var readAgain = Console.ReadLine().ToUpper();
            while(readAgain != "Y" || readAgain != "N")
            {
                if (readAgain == "Y")
                {
                    string text = File.ReadAllText(testTextFilePath);
                    Console.WriteLine(text);
                    break;
                }
                else if (readAgain == "N")
                {
                    Console.WriteLine("Ok. Thank you for participating.");
                }
                else
                {
                    Console.WriteLine("Y/N?");
                }
            }          
        }
    }
}