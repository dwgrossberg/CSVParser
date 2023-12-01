using System;
using System.IO;

namespace CSVParser;

class Program
{
    static void Main(string[] args)
    {
        // Get csv file name from user
        Console.WriteLine("What is the name of the file you would like to search for?");
        Console.WriteLine();
        var fileName = Console.ReadLine();
        Console.WriteLine();
        if (fileName is not null)
        {
            // Save valid and invalid emails as string arrays
            (string[], string[]) emails = parseCSVFile(fileName);
            string[] validEmails = emails.Item1;
            string[] invalidEmails = emails.Item2;
            // Print valid emails
            foreach (var email in validEmails)
            {
                Console.WriteLine(email);
            }
            Console.WriteLine();
            // Print invalid emails
            foreach (var email in invalidEmails)
            {
                Console.WriteLine(email);
            }
            Console.WriteLine();
        }
    }

    public static (string[], string[]) parseCSVFile(string fileName)
    {
        string[] error = { "Error:" };
        string[] fileNotFound = { "Provided file name does not exist." };
        try
        {
            // Search project working directory for matching csv file
            string directory = Environment.CurrentDirectory;
            string filePath = $"{directory}/{fileName}.csv";

            // Parse csv file
            StreamReader ? reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                List<string> emails = new List<string>();
                // Loop through all rows within csv file
                while (!reader.EndOfStream){
                    var line = reader.ReadLine();
                    if (line is not null) 
                    {
                        var values = line.Split(',');
                        // Extract emails from csv
                        emails.Add(values[2]);
                    }   
                }
                return (emails.ToArray(), emails.ToArray());
            } 
            else
            {
                Console.WriteLine("Error: File doesn't exist.");
                Console.WriteLine();
                return (error, fileNotFound);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return (error, fileNotFound);
        }
    }

}
