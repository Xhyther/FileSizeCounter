using System.CommandLine;

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
                description: "Shows the basic information about the file."
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
        rootCommand.Add(fileArgument);
        
        rootCommand.SetHandler((boption, loption, woption, moption, infoOptionValue, file) =>
        {
          
            if (!file.Exists)
            { 
                Console.WriteLine("File not found.");
            }
            else
            {
                if (boption)
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

                if (loption)
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

                if (woption)
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

                if (moption)
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
                       string[] fileFullname = file.FullName.Split("/");
                       int lastIndex = fileFullname.Length - 1;
                       string fileName = fileFullname[lastIndex];
                       Console.WriteLine($"   {fileName}, {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (!boption &&!loption && !woption && !moption && !infoOptionValue)
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
            
            
            
           
        }, byteCounter, lineCounter, wordCounter, characterCounter, infoOption, fileArgument);
        
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