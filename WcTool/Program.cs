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

        var fileArgument = new Argument<FileInfo>
        (
            name: "file",
            description: "The file to read from."
        ){Arity = ArgumentArity.ExactlyOne};
        
        rootCommand.Add(byteCounter);
        rootCommand.Add(lineCounter);
        rootCommand.Add(wordCounter);
        rootCommand.Add(fileArgument);
        
        rootCommand.SetHandler((boption, loption, file) =>
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
                        Console.WriteLine($"{totalLines}, {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                
            }
            
            
            
           
        }, byteCounter, lineCounter, fileArgument);
        
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
}