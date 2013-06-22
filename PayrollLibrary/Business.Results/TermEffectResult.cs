using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;

namespace PayrollLibrary.Business.Results
{
    public class TermEffectResult : PayrollResult
    {
        public TermEffectResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public uint DayOrdFrom { get; private set; }

        public uint DayOrdEnd  { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.DayOrdFrom = GetUIntOrZeroValue(values, "day_ord_from");
            this.DayOrdEnd  = GetUIntOrZeroValue(values, "day_ord_end");
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"day_ord_from", DayOrdFrom.ToString()},
                {"day_ord_end", DayOrdEnd.ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            if (DayOrdFrom > 0 && DayOrdEnd > 0)
            {
                return DayOrdFrom.ToString() + " - " + DayOrdEnd.ToString();
            }
            else
            {
                return "whole period";
            }
        }

        public override string ExportValueResult()
        {
            if (DayOrdFrom > 0 && DayOrdEnd > 0)
            {
                return DayOrdFrom.ToString() + " - " + DayOrdEnd.ToString();
            }
            else
            {
                return "whole period";
            }
        }
    }
}
