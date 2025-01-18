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

        var fileArgument = new Argument<FileInfo>
        (
            name: "file",
            description: "The file to read from."
        ){Arity = ArgumentArity.ExactlyOne};
        
        rootCommand.Add(byteCounter);
        rootCommand.Add(lineCounter);
        rootCommand.Add(fileArgument);
        
        rootCommand.SetHandler((Boption, Loption, file) =>
        {
          
            if (!file.Exists)
            { 
                Console.WriteLine("File not found.");
            }
            else
            {
                if (Boption)
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

                if (Loption)
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
        int LineCounter = 0;
        try
        {
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    LineCounter++;
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return LineCounter;
       
    }
}