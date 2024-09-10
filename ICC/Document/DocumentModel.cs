using ICC.Condition;
using ICC.Header;
using System.Collections.Generic;

namespace ICC.Document
{
    public class DocumentModel
    {
        public HeaderModel Header{ get; set; }
        public IEnumerable<ConditionModel> Conditions { get; set; }

        public DocumentModel(HeaderModel header, IEnumerable<ConditionModel> conditions)
        {
            Header = header;
            Conditions = conditions;
        }
    }
}
