using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;
using PayrollLibrary.Business.Libs;

namespace PayrollLibrary.Business.Results
{
    public class PaymentResult : PayrollResult
    {
        public PaymentResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public decimal payment;

        public override decimal Payment() { return payment; }

        public uint InterestCode { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.payment = GetDecimalOrZeroValue(values, "payment");
            this.InterestCode = GetUIntOrZeroValue(values, "interest_code");
        }

        public bool Interest()
        {
            return InterestCode != 0;
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

        public virtual string XmlValue()
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
