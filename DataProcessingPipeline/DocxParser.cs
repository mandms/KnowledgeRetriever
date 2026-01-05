using AllMiniLmL6V2Sharp;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LangChain.Extensions;
using LangChain.Splitters.Text;
using Microsoft.SemanticKernel.Text;
using System.Text;

public class DocxParser
{
    public static string ParseDocx(string filePath)
    {
        var text = new StringBuilder();

        using (var doc = WordprocessingDocument.Open(filePath, false))
        {
            var body = doc.MainDocumentPart?.Document.Body;
            if (body == null) return string.Empty;

            foreach (var paragraph in body.Elements<Paragraph>())
            {
                foreach (var run in paragraph.Elements<Run>())
                {
                    foreach (var textElement in run.Elements<Text>())
                    {
                        text.Append(textElement.Text + " ");
                    }
                }
                text.AppendLine();
            }
        }

        return text.ToString();
    }

    public static IReadOnlyList<string> ChunkText(string text, int chunkSize = 250, int overlap = 50)
    {

        var recursiveSplitter = new RecursiveCharacterTextSplitter(
            chunkSize: chunkSize,
            chunkOverlap: overlap,
            separators: ["\n\n", "\n", ". ", "? ", "! ", "; ", ", ", " ", ""]
        );
        var chunks = recursiveSplitter.SplitText(text);

        return chunks;

        //var chunks = new List<string>();

        //// Очистите текст от лишних переносов
        //text = text.Replace("\r\n", " ").Replace("\n", " ");
        //while (text.Contains("  ")) text = text.Replace("  ", " ");

        //var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        //Console.WriteLine($"Debug: Total words after cleaning: {words.Length}");

        //for (int i = 0; i < words.Length; i += chunkSize - overlap)
        //{
        //    var chunkWords = words.Skip(i).Take(chunkSize).ToArray();
        //    if (chunkWords.Length == 0) continue;

        //    var chunk = string.Join(" ", chunkWords);

        //    // Только чанки разумной длины
        //    if (chunk.Length > 100)
        //    {
        //        chunks.Add(chunk);
        //        Console.WriteLine($"Debug: Created chunk {chunks.Count} with {chunkWords.Length} words, {chunk.Length} chars");
        //    }
        //}
    }
}