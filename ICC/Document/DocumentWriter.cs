using ICC.Condition;
using ICC.Header;
using System.Collections.Generic;
using System.IO;

namespace ICC.Document
{
    public class DocumentWriter
    {
        private readonly string documentPath;

        public DocumentWriter(string documentPath)
        {
            this.documentPath = documentPath;
        }

        public void Write(DocumentModel model)
        {
            using (var sw = new StreamWriter(documentPath))
            {
                sw.Write(model.Header.ToString());
                foreach (var condition in model.Conditions)
                {
                    sw.Write(condition.ToString());
                }
            }
        }

        public void WriteHeader(HeaderModel header)
        {
            using (var sw = new StreamWriter(documentPath))
            {
                sw.Write(header.ToString());
            }
        }

        public void WriteConditions(IEnumerable<ConditionModel> conditions)
        {
            using (var sw = new StreamWriter(documentPath))
            {
                foreach (var condition in conditions)
                {
                    sw.Write(condition.ToString());
                }
            }
        }
    }
}
