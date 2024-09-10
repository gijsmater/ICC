using ICC.Condition;
using ICC.Header;
using System.Collections.Generic;
using System.IO;

namespace ICC.Document
{
    public class DocumentReader
    {
        private readonly string documentPath;

        public DocumentReader(string documentPath)
        {
            this.documentPath = documentPath;
        }

        public DocumentModel GetDocument()
        {
            using (var sr = new StreamReader(documentPath))
            {
                string line = sr.ReadLine();
                var header = line.ToHeaderModel();
                var lineModels = new List<ConditionModel>();

                int index = 0;
                while (line != null)
                {
                    if (index != 0)
                    {
                        lineModels.Add(line.ToConditionModel());
                    }
                    line = sr.ReadLine();
                    index++;
                }
                return new DocumentModel(header, lineModels);
            }
        }
    }
}
