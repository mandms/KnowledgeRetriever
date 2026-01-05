using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LangChain.Splitters.Text;
using System.Text;

namespace DataProcessingWorker.IngestionService
{
    public static class DocxParser
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

        public static IReadOnlyList<string> ChunkText(string text, int chunkSize = 500, int overlap = 50)
        {
            var recursiveSplitter = new RecursiveCharacterTextSplitter(
                chunkSize: chunkSize,
                chunkOverlap: overlap,
                separators: ["\n\n", "\n", ". ", "? ", "! ", "; ", ", ", " ", ""]
            );
            var chunks = recursiveSplitter.SplitText(text);

            return chunks;
        }
    }
}