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

        var fileArgument = new Argument<FileInfo>
        (
            name: "file",
            description: "The file to read from."
        ){Arity = ArgumentArity.ExactlyOne};
        
        rootCommand.Add(byteCounter);
        rootCommand.Add(fileArgument);
        
        rootCommand.SetHandler((option, file) =>
        {
            if (option)
            {
                if (!file.Exists)
                { 
                    Console.WriteLine("File not found.");
                }
                else
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
            }
            
           
        }, byteCounter, fileArgument);
        
        await rootCommand.InvokeAsync(args);
    }
}