using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;
using PayrollLibrary.Business.Libs;

namespace PayrollLibrary.Business.Results
{
    public class AmountResult : PayrollResult
    {
        public AmountResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public decimal amount;

        public override decimal Amount() { return amount; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.amount = GetDecimalOrZeroValue(values, "amount");
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"amount", Amount().ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            return Amount().ToString() + " CZK";
        }

        public override string ExportValueResult()
        {
            string amountString = Amount().ToString("#0");
            string formatAmount = amountString.FormatAmount(); 
            return formatAmount + " CZK";
        }
    }
}
