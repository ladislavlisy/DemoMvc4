using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;
using PayrollLibrary.Business.Libs;

namespace PayrollLibrary.Business.Results
{
    public class TaxReliefResult : PayrollResult
    {
        public TaxReliefResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public decimal taxRelief;

        public override decimal TaxRelief() { return taxRelief; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.taxRelief = GetDecimalOrZeroValue(values, "tax_relief");
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"tax_relief", TaxRelief().ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            return TaxRelief().ToString() + " CZK";
        }

        public override string ExportValueResult()
        {
            string amountString = TaxRelief().ToString("#0");
            string formatAmount = amountString.FormatAmount();
            return formatAmount + " CZK";
        }
    }
}
