
# WcTool (ccwc)

**WcTool** is a command-line utility written in C# for counting and displaying file information, including bytes, lines, words, characters, and more.

## Features

- Count the number of bytes, lines, words, and characters in a file.
- Display basic file information, including file type, size, location, creation time, and more.

## Usage

```bash
WcTool <file> [options]
```
or

```bash
WcTool [options] <file>
```

### Arguments

- **`<file>`**: The file or filepath to read from (required).

### Options

| Option           | Description                                                        |
|------------------|--------------------------------------------------------------------|
| `-c`             | Counts the number of bytes in the file.                           |
| `-l`             | Counts the number of lines in the file.                           |
| `-w`             | Counts the total number of words in the file.                     |
| `-m`             | Counts the total characters in the file.                          |
| `-i`, `--info`   | Shows the basic information and properties of the file.           |
| `--version`      | Show version information.                                         |
| `-?`, `-h`, `--help` | Show help and usage information.                              |

### Example Commands

1. **Count lines in a file:**
   ```bash
   WcTool sample.txt -l
   ```

2. **Count words in a file:**
   ```bash
   WcTool sample.txt -w
   ```

3. **Display basic file information:**
   ```bash
   WcTool sample.txt -i
   ```

4. **Count bytes, lines, words, and characters simultaneously:**
   ```bash
   WcTool sample.txt
   ```

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Xhyther/WCTOOL.git
   ```
2. Build the project:
   - Open the solution file in Visual Studio or your preferred IDE.
   - Build the project in `Release` mode.

3. Navigate to the `bin/Release` directory to find the executable.

## How It Works

WcTool leverages the **System.CommandLine** library for parsing commands and options. It provides options to perform various operations on a given file, such as counting lines, words, or characters, and fetching detailed file information.

## Requirements

- .NET 6.0 or later
- Windows, Linux, or macOS

## Contributing

Contributions are welcome! Feel free to submit a pull request or open an issue for suggestions and improvements.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


---
