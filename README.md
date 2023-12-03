# CSVParser

A simple .NET console application that prompts the user for a file name, searches the current (project) working directory for a .csv file with that name, and then parses the CSV file.

If the file does not exist within the current working directory, the program will output an error message to the user.

If the file does exist, the program will output a list of valid email addresses and a list of invalid email addresses.

CSV file structure will be in the form of: FirstName, LastName, EmailAddress

_Built with .NET 8_

## Usage Examples

![File is found correctly and parsed.](./CSVParser1.gif)

![File is not found.](./CSVParser2.gif)
