using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;

namespace PayrollLibrary.Business.Results
{
    public class TermHoursResult : PayrollResult
    {
        public TermHoursResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public int Hours { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.Hours = GetIntOrZeroValue(values, "hours");
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"hours", Hours.ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            return Hours.ToString() + " hours";
        }

        public override string ExportValueResult()
        {
            return Hours.ToString() + " hours";
        }
    }
}
