using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;
using PayrollLibrary.Business.Libs;

namespace PayrollLibrary.Business.Results
{
    public class TaxAdvanceResult : PayrollResult
    {
        public TaxAdvanceResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public decimal payment;

        public override decimal Payment() { return payment; }

        public decimal afterReliefA;

        public override decimal AfterReliefA() { return afterReliefA; }

        public decimal afterReliefC;

        public override decimal AfterReliefC() { return afterReliefC; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.payment = GetDecimalOrZeroValue(values, "payment");
            this.afterReliefA = GetDecimalOrZeroValue(values, "after_reliefA");
            this.afterReliefC = GetDecimalOrZeroValue(values, "after_reliefC");
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"payment", Payment().ToString()},
                {"after_reliefA", AfterReliefA().ToString()},
                {"after_reliefC", AfterReliefC().ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
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
