using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;
using PayrollLibrary.Business.Libs;

namespace PayrollLibrary.Business.Results
{
    public class PaymentDeductionResult : PaymentResult
    {
        public PaymentDeductionResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem, values)
        {
            InitValues(values);
        }

        public override decimal Deduction() { return payment; }

        public override void InitValues(IDictionary<string, object> values)
        {
            base.InitValues(values);
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"payment", Payment().ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public override string XmlValue()
        {
            return Payment().ToString() + " CZK";
        }

        public override string ExportValueResult()
        {
            string amountString = Payment().ToString("#0");
            string formatAmount = amountString.FormatAmount();
            return formatAmount + " CZK";
        }
    }
}
