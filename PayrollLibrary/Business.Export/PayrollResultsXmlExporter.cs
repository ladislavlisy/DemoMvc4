using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PayrollLibrary.Business.Core;
using PayrollLibrary.Business.CoreItems;

namespace PayrollLibrary.Business.Export
{
    public class PayrollResultsXmlExporter : PayrollResultsExporter
    {
        public PayrollResultsXmlExporter(string company, string department, string person, string personNumber, PayrollProcess payroll)
            : base(company, department, person, personNumber, payroll)
        {
        }

        public string ExportXml()
        {
            string periodDescription = PPeriod.Description();
            StringBuilder result = new StringBuilder();
            XmlWriter xmlBuilder = XmlWriter.Create(result);

            xmlBuilder.WriteStartDocument();
            xmlBuilder.WriteStartElement("payslips");
            xmlBuilder.WriteStartElement("payslip");
            xmlBuilder.WriteStartElement("employee");
            xmlBuilder.WriteStartAttribute("personnel_number");
            xmlBuilder.WriteString(this.EmployeeNumb);
            xmlBuilder.WriteEndAttribute();
            xmlBuilder.WriteStartAttribute("common_name");
            xmlBuilder.WriteString(this.EmployeeName);
            xmlBuilder.WriteEndAttribute();
            xmlBuilder.WriteStartAttribute("department");
            xmlBuilder.WriteString(this.EmployeeDept);
            xmlBuilder.WriteEndAttribute();
            xmlBuilder.WriteEndElement();

            xmlBuilder.WriteStartElement("employer");
            xmlBuilder.WriteStartAttribute("common_name");
            xmlBuilder.WriteString(this.EmployerName);
            xmlBuilder.WriteEndAttribute();
            xmlBuilder.WriteEndElement();

            xmlBuilder.WriteStartElement("results");
            xmlBuilder.WriteStartAttribute("period");
            xmlBuilder.WriteString(periodDescription);
            xmlBuilder.WriteEndAttribute();

            foreach (var o in this.Results)
            {
                var tagResult = o.Key;
                var valResult = o.Value;
                xmlBuilder.WriteStartElement("result");
                ItemExportXml(Payroll, PayrollNames, tagResult, valResult, xmlBuilder);
                xmlBuilder.WriteEndElement();
            }
            xmlBuilder.WriteEndElement();

            xmlBuilder.WriteEndElement();
            xmlBuilder.WriteEndDocument();

            //Write the XML to file and close the writer.
            xmlBuilder.Flush();
            xmlBuilder.Close();

            return result.ToString();
        }

        private void ItemExportXml(PayrollProcess payroll, PayNameGateway payNames, PayrollLibrary.Business.CoreItems.TagRefer tagResult, PayrollLibrary.Business.CoreItems.PayrollResult valResult, XmlWriter xmlBuilder)
        {
            PayrollTag tagItem = payroll.FindTag(valResult.TagCode);
            PayrollConcept tagConcept = payroll.FindConcept(valResult.ConceptCode);
            PayrollName tagName = payNames.FindName(tagResult.Code);

            valResult.ExportXml(tagResult, tagName, tagItem, tagConcept, xmlBuilder);
        }
    }
}
