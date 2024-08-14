using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using ColorCode;
using System.Net.Http;
using System.Text;
using System.Reflection.Metadata;

class Program
{
    static void Main(string[] args)
    {
        string folderPath = @"D:\COS20007\Week_6\Iretation4\IdentifiableObject\IdentifiableObjectTest"; // Replace with your folder path
        var files = Directory.GetFiles(folderPath, "*.cs");

        string pdfPath = $@"D:\COS20007\Week_6\Iretation4\IdentifiableObject\test.pdf"; // Replace with your desired path

        using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
        {
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, stream);

            pdfDoc.Open();

            for (int i = 0; i < files.Length; i++)
            {
                string content = File.ReadAllText(files[i]);
                string htmlContent = ConvertToHtml(content, Path.GetFullPath(files[i]));

                var htmlWorker = new HTMLWorker(pdfDoc);
                htmlWorker.Parse(new StringReader(htmlContent));

                // Add a new page if it's not the last file
                if (i < files.Length - 1)
                {
                    pdfDoc.NewPage();
                }
            }

            pdfDoc.Close();
            stream.Close();
        }
    }
    static string ConvertToHtml(string code, string filename)
    {
        int count = 0;
        var colorizer = new CodeColorizer();
        string colorizedCode = colorizer.Colorize(code, Languages.CSharp);
        string htmlWithLineBreaksAndSpaces = colorizedCode.Replace("\n", "<br>").Replace(" ", "&nbsp;").Replace("span&nbsp;", "span ").Replace("div&nbsp;", "div ");
        var builder = new StringBuilder();
        builder.Append(filename + "<br");
        for (int i = 3; i < htmlWithLineBreaksAndSpaces.Length; i++)
        {
            builder.Append(htmlWithLineBreaksAndSpaces[i]);
            if (htmlWithLineBreaksAndSpaces[i - 3] == '<' &&
                htmlWithLineBreaksAndSpaces[i - 2] == 'b' &&
                htmlWithLineBreaksAndSpaces[i - 1] == 'r' &&
                htmlWithLineBreaksAndSpaces[i] == '>')
            {
                count++;
                builder.Append("<span style=\"color:#4DAABF;\">" + count + "&nbsp;&nbsp;&nbsp;</span>");
            }
        }
        Console.WriteLine(builder.ToString());
        string htmlWithFontSize = "<div style=\"font-size: 8px;\">" + builder + "</div>"; // Change 8px to your desired font size

        return htmlWithFontSize;
    }
}