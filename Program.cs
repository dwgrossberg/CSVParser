using System;
using System.IO;

namespace CSVParser;

class Program
{
    static void Main(string[] args)
    {
        // Get csv file name from user
        Console.WriteLine("What is the name of the file you would like to search for?");
        var fileName = Console.ReadLine();
        try
        {
            // Search project working directory for matching csv file
            string directory = Environment.CurrentDirectory;
            string filePath = $"{directory}/{fileName}.csv";
            Console.Write(filePath);

            // Extract emails from csv and save in a list
            StreamReader reader = null;
            if (File.Exists(filePath)){
                reader = new StreamReader(File.OpenRead(filePath));
                List<string> emails = new List<string>();
                // Loop through all rows within csv file
                while (!reader.EndOfStream){
                    var line = reader.ReadLine();
                    Console.WriteLine(line);
                    var values = line.Split(',');
                    // Loop through all columns in csv file
                    foreach (var item in values){
                        emails.Add(item);
                        Console.WriteLine(item);
                    }
                    foreach (var coloumn1 in emails){
                        //Console.WriteLine(coloumn1);
                    }
                }
            } else {
                Console.WriteLine("File doesn't exist");
            }
            Console.ReadLine();


        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        
    }

}
