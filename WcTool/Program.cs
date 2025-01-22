using System.CommandLine;
using System.Diagnostics;

namespace WcTool;

class Program
{
    public static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("ccwc");
        var byteCounter = new Option<bool>
            (
                aliases: new[] { "-c" },
                description: "Counts the number of bytes in the file."
            );

        var lineCounter = new Option<bool>
            (
                aliases: new[] {"-l"},
                description: "Counts the number of lines in the file."
            );

        var wordCounter = new Option<bool>
            (
                aliases: new[] {"-w"},
                description: "Counts the total number of words in the file."
            );

        var characterCounter = new Option<bool>
            (
                aliases: new[] { "-m" },
                description: "Counts the total characters in the file."
            );

        var infoOption = new Option<bool>
            (
                aliases: new[] {"-i", "--info"},
                description: "Shows the basic information and properties of the file. "
            );

        var openFileoption = new Option<bool>
        (
            aliases: new[] { "-o", "--open-File" },
            description: "Opens the file using the console."
        );
        
        var fileArgument = new Argument<FileInfo>
        (
            name: "file",
            description: "The file / filepath to read from."
        ){Arity = ArgumentArity.ExactlyOne};
        
        rootCommand.Add(byteCounter);
        rootCommand.Add(lineCounter);
        rootCommand.Add(wordCounter);
        rootCommand.Add(characterCounter);
        rootCommand.Add(infoOption);
        rootCommand.Add(openFileoption);
        rootCommand.Add(fileArgument);
        
        rootCommand.SetHandler((bOption, lOption, wOption, mOption, infoOptionValue, openOption, file) =>
        {
          
            if (!file.Exists)
            { 
                Console.WriteLine("File not found.");
            }
            else
            {
                if (bOption)
                {
                    try
                    {
                        long fileSize = new FileInfo(file.FullName).Length;
                        Console.WriteLine($"{fileSize}, {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (lOption)
                {
                    try
                    {
                        int totalLines = FileLineCounter(new FileInfo(file.FullName));
                        Console.WriteLine($"   {totalLines}, {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (wOption)
                {
                    try
                    {
                        int totalWords = WordCounter(new FileInfo(file.FullName));
                        Console.WriteLine($"   {totalWords}, {file}");
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (mOption)
                {
                    try
                    {
                        int totalCharacters = CharacterCounter(new FileInfo(file.FullName));
                        Console.WriteLine($"   {totalCharacters}, {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (infoOptionValue)
                {
                    try
                    {
                        //Finding the name of the file
                       string[] fileFullname = file.FullName.Split("/");
                       int lastIndex = fileFullname.Length - 1;
                       string fileName = fileFullname[lastIndex];
                       
                       //finding the type of the file
                       string[] wType = fileName.Split(".");
                       string type = wType[1];
                       
                       //Finding the file path
                       string[] findPath = file.FullName.Split(fileName);
                       string path = findPath[0];
                       
                       //Finding the size of the file
                       long fileSize = new FileInfo(file.FullName).Length;
                       
                       //Getting the time of creation of the file
                       DateTime creationTime = File.GetCreationTime(file.FullName);
                       //Getting the last access time of the file
                       DateTime lastAccessTime = File.GetLastAccessTime(file.FullName);
                       //Getting the last writeTime of the file
                       DateTime lastWriteTime = File.GetLastWriteTime(file.FullName);
                       
                       
                       Console.WriteLine($"   Name: {fileName}");
                       Console.WriteLine($"   Type: .{type}");
                       Console.WriteLine($"   Location: {path}");
                       Console.WriteLine($"   Size: {fileSize/1000} KB ({fileSize} bytes)");
                       Console.WriteLine();
                       Console.WriteLine($"   Creation Time: {creationTime}");
                       Console.WriteLine($"   Last Access: {lastAccessTime}");
                       Console.WriteLine($"   Last Write Time: {lastWriteTime}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (openOption)
                {
                    try
                    {
                        //Finding the name of the file
                        string[] fileFullname = file.FullName.Split("/");
                        int lastIndex = fileFullname.Length - 1;
                        string fileName = fileFullname[lastIndex];
                       
                        //finding the type of the file
                        string[] wType = fileName.Split(".");
                        string type = wType[1];

                        using Process myProcess = new Process();

                        if (type == ".txt")
                        {
                            try
                            {
                                Console.WriteLine($"   Attempting to execute {fileName}");
                                myProcess.StartInfo.FileName = "notepad.exe";
                                myProcess.StartInfo.Arguments = file.FullName;
                                myProcess.StartInfo.UseShellExecute = true;
                                myProcess.Start();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                       //Add another one here for pdf
                       //add
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (!bOption &&!lOption && !wOption && !mOption && !infoOptionValue && !openOption)
                {
                    try
                    {
                        
                        int totalLines = FileLineCounter(new FileInfo(file.FullName));
                        int totalWords = WordCounter(new FileInfo(file.FullName));
                        int totalCharacters = CharacterCounter(new FileInfo(file.FullName));
                        Console.WriteLine($"   {totalLines} {totalWords} {totalCharacters} {file}");
                       
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                
                
                
            }
            
            
            
           
        }, byteCounter, lineCounter, wordCounter, characterCounter, infoOption, openFileoption, fileArgument);
        
        await rootCommand.InvokeAsync(args);
    }
    
    private static int FileLineCounter(FileInfo file)
    {
        int lineCounter = 0;
        try
        {
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                while ((reader.ReadLine()) != null)
                {
                    lineCounter++;
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return lineCounter;
       
    }

    private static int WordCounter(FileInfo file)
    {
        int counter = 0;

        try
        {
            using var reader = new StreamReader(file.FullName);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                counter += line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading file: {e.Message}");
            throw;
        }

        return counter;
    }

    private static int CharacterCounter(FileInfo file)
    {
        int counter = 0;
        try
        {
            using var reader = new StreamReader(file.FullName);
            string? line;
            
     
            while ((line = reader.ReadLine()) != null)
            {
               counter += line.Length;
               counter++;
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return counter;
    }
    
}