using System;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CSVParser;

class Program
{
    static void Main(string[] args)
    {
        // Get csv file name from user.
        Console.WriteLine("What is the name of the file you would like to search for?");
        Console.WriteLine();
        var fileName = Console.ReadLine();
        Console.WriteLine();
        if (fileName is not null)
        {
            // Save valid and invalid emails as string arrays.
            (string[], string[]) emails = ParseCSVFile(fileName);
            string[] validEmails = emails.Item1;
            string[] invalidEmails = emails.Item2;
            // Check for error message.
            if ((IsValidEmail(validEmails[0])) == false)
            {
                Console.WriteLine(validEmails[0]);
                Console.WriteLine(invalidEmails[0]);
            }
            // Otherwise print email lists.
            else
            {
                // Print valid emails.
                Console.WriteLine("VALID EMAIL ADDRESSES:");
                foreach (var email in validEmails)
                {
                    Console.WriteLine(email);
                }
                Console.WriteLine();
                // Print invalid emails.
                Console.WriteLine("INVALID EMAIL ADDRESSES:");
                foreach (var email in invalidEmails)
                {
                    Console.WriteLine(email);
                }
            }
            Console.WriteLine();            
        }
    }

    public static (string[], string[]) ParseCSVFile(string fileName)
    {
        string[] error = { "ERROR-------------------------------------------------" };
        string[] fileNotFound = { "Provided file name does not exist in current directory." };
        try
        {
            // Search project working directory for matching csv file.
            string directory = Environment.CurrentDirectory;
            string filePath = $"{directory}{Path.DirectorySeparatorChar}{fileName}.csv";

            // Parse csv file.
            StreamReader ? reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                List<string> emails = new List<string>();
                // Loop through all rows within csv file.
                while (!reader.EndOfStream){
                    var line = reader.ReadLine();
                    if (line is not null) 
                    {
                        var values = line.Split(',');
                        // Extract emails from csv - found in column index 2.
                        emails.Add(values[2]);
                    }   
                }
                // Validate emails and save accordingly.
                List<string> validEmails = new List<string>();
                List<string> invalidEmails = new List<string>();
                foreach (var email in emails)
                {
                    if (IsValidEmail(email))
                    {
                        validEmails.Add(email);
                    }
                    else
                    {
                        invalidEmails.Add(email);
                    }
                }
                return (validEmails.ToArray(), invalidEmails.ToArray());
            } 
            else
            {
                return (error, fileNotFound);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return (error, fileNotFound);
        }
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}
