using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;
using PayrollLibrary.Business.Libs;

namespace PayrollLibrary.Business.Results
{
    public class IncomeBaseResult : PayrollResult
    {
        public IncomeBaseResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public decimal incomeBase;

        public override decimal IncomeBase() { return incomeBase; }

        public decimal employerBase;

        public override decimal EmployerBase() { return employerBase; }
        
        public decimal employeeBase;

        public override decimal EmployeeBase() { return employeeBase; }

        public uint InterestCode { get; private set; }

        public uint MinimumAsses { get; private set; }

        public uint DeclareCode  { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.incomeBase = GetDecimalOrZeroValue(values, "income_base");
            this.employerBase = GetDecimalOrZeroValue(values, "employer_base");
            this.employeeBase = GetDecimalOrZeroValue(values, "employee_base");
            this.InterestCode = GetUIntOrZeroValue(values, "interest_code");
            this.MinimumAsses = GetUIntOrZeroValue(values, "minimum_asses");
            this.DeclareCode  = GetUIntOrZeroValue(values, "declare_code");
        }

        public bool Interest()
        {
            return InterestCode != 0;
        }

        public bool Declared()
        {
            return DeclareCode != 0;
        }

        public bool MinimumAssesment()
        {
            return MinimumAsses != 0;
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"income_base", IncomeBase().ToString()},
                {"employee_base", EmployeeBase().ToString()},
                {"employer_base", EmployerBase().ToString()},
                {"declare_code", DeclareCode.ToString()},
                {"interest_code", InterestCode.ToString()},
                {"minimum_asses", MinimumAsses.ToString()}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            return IncomeBase().ToString() + " CZK";
        }

        public override string ExportValueResult()
        {
            string amountString = IncomeBase().ToString("#0");
            string formatAmount = amountString.FormatAmount();
            return formatAmount + " CZK";
        }
    }
}
