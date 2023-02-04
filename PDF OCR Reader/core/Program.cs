// See https://aka.ms/new-console-template for more information
//PDF_PATH


Console.WriteLine("Hello, World!");
Core.PDFExtract extract = new Core.PDFExtract();

await extract.GetFile();
