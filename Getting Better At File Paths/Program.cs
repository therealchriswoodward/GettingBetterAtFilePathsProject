// REVIEW: These are not used so could be removed
//  The reason is that your project has ImplicitUsings enabled
//  so these are automatically already in scope.
using System;
using System.IO;
using System.Collections.Generic;

namespace GettingBetterAtFilePaths
{
    class Program
    {
        static void Main(string[] args)
        {
            // REVIEW: This is something many will disagree upon but that's
            //  what's great about programming. No objective truths and we are
            //  free to have opinions.
            //  I like that you just shove all code in the main function.
            //  I do of course use functions but I personally think it can
            //  be easier to follow a function like this.
            //  Reasons for using functions for me is that I would like to avoid
            //  repeating code or I want to unit test a portion of the code.
            //  Making the function small is not part of my decision matrix but
            //  is very important to others.
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var testDirectoryPath = Path.Combine(desktopPath, "Test");
            var testTextFilePath = Path.Combine(testDirectoryPath, "test.txt");
            
            if(!Directory.Exists(testTextFilePath))
            {
                // REVIEW: Reading the doc CreateDirectory does nothing
                //  if the directory already exists so you could possible remove
                //  Directory.Exists test
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
                    // REVIEW: line might be null which can cause
                    //  this line to fail
                    //  VS warns me about this since your project
                    //  has Nullable enabled
                    if (line.Length == 0)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                // REVIEW: For discarding the exception feels wrong
                //  I have been exposed too many times that the exception
                //  is thrown a way and then I sit with a production issue
                //  that I can't solve because of it
                //  It could be that you don't need to catch exceptions here.
                //  If you handle the null return from ReadLine the only 
                //  reason the code in the try block should fail if something
                //  really bad happened and then it is ok if the program crashes.
                //  IMHO one should write try ... catch very rarely in code
                //  and prefer to crash completely. In reality one needs to pick
                //  the approach that works but that is my preference
                Console.WriteLine("That didn't work.");
            }
            finally
            {
                Console.WriteLine("Finally code block");
            }                       
            // REVIEW: The writer should be automatically closed since you
            //  use using var writer = ...
            //  There can be reasons for explicitly closing in order to release
            //  the resource as early as possible.
            //  If would prefer to use scopes for that.
            writer.Close();

            Console.WriteLine("Would you like for me to read that back to you? Y/N?");

            // REVIEW: Console.ReadLine() might return null causing a crash
            // REVIEW: I would use ToUpperInvariant to not be surprised if this
            //  programming is running on a Culture you haven't tested.
            // REVIEW: .Trim() might be a good idea to handle if someone
            //  puts in a few spaces by mistake
            var readAgain = Console.ReadLine().ToUpper();
            // REVIEW: Since you use while(true) above what about using it
            //  here as well and if in the inner test there's no match you
            //  break out of the loop?
            while(readAgain != "Y" || readAgain != "N")
            {
                // REVIEW: Perhaps a switch would be easier to read and handle
                //  multiple cases such as YES and NO as well?
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