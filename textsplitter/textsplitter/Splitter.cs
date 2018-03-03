using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace textsplitter
{
    internal sealed class Splitter
    {
        public async Task SplitText(string[] args)
        {
            StreamWriter output = null;
            string line;
            const int MAX_LINES_PER_FILE = 100;
            int currentLine = 0;
            int currentFileNumber = 0;
            using (var inputFile = new StreamReader(args[0]))
            {

                while (!inputFile.EndOfStream)
                {
                    line = await inputFile.ReadLineAsync();
                    //save line in output file
                    bool isLineEmpty = string.IsNullOrEmpty(line);
                    if (!isLineEmpty)
                    {
                        if (output == null)
                        {
                            if (!Directory.Exists("textsplitter"))
                            {
                                Directory.CreateDirectory("textsplitter");
                            }
                            string outputFilename = $@".\textsplitter\textsplitter-{currentFileNumber + 1}.txt";
                            Console.WriteLine($"Writing to {outputFilename}");
                            output = new StreamWriter(outputFilename, false,
                                inputFile.CurrentEncoding);
                        }

                    }

                    currentLine++;

                    if (currentLine > MAX_LINES_PER_FILE)
                    {
                        output.Dispose();
                        output = null;
                        currentLine = 0;
                        currentFileNumber++;
                    }
                    else if (!isLineEmpty)
                    {

                        await output.WriteAsync($"{(currentLine > 1 ? Environment.NewLine : "")}{line}");

                    }
                }


            }
        }
    }
}
