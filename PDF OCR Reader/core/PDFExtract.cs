
using System;

using IronOcr;

namespace Core
{
    /// <summary>
    /// Extract pdf data
    /// </summary>
    public class PDFExtract
    {
        /// <summary>
        /// find the file
        /// </summary>
        public async Task GetFile()
        {

            string? pdfPath = Environment.GetEnvironmentVariable("PDF_PATH", EnvironmentVariableTarget.User); //Security. let me know if you want to access a PDF...

            string? resultPath = Environment.GetEnvironmentVariable("OUTPUT_RESULT", EnvironmentVariableTarget.User);

            if (pdfPath == null)
            {
                Console.WriteLine("path cannot be found."); // if the environment variable could not be found
                return;
            }

            if (resultPath == null)
            {
                Console.WriteLine("result path could not be found");
                return;
            }

            DirectoryInfo inputInfo = new DirectoryInfo(pdfPath);

            if (!inputInfo.Exists)//check to see if the file exists.. return if the file does not exist
            {
                Console.WriteLine("Could not find file");
                return;
            }

            

            foreach (var x in inputInfo.EnumerateFiles())
            {

                string fileName = x.Name.Split('.')[0];

                fileName = fileName + ".txt";

                var path = Path.Combine(resultPath, fileName);

                Console.WriteLine(path);
                Console.WriteLine(x.FullName.ToString());

                await OCRFile(x.FullName, path.ToString());

    
            }

            Console.WriteLine("done. File written to \'" + resultPath + "\'");
        }

        private async Task OCRFile(string path, string resultPath)
        {
            var Ocr = new IronOcr.IronTesseract();
            var results = await Ocr.ReadAsync(path);
            var text = results.Text;

            using (FileStream stream = new FileStream(resultPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        await writer.WriteAsync(text[i]);
                    }
                }
            }
        }
    }
}