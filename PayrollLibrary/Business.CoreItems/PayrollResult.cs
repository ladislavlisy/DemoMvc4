using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PayrollLibrary.Business.Symbols;

namespace PayrollLibrary.Business.CoreItems
{
    public class PayrollResult
    {
        public PayrollResult(uint code, uint conceptCode, PayrollConcept conceptItem)
        {
            this.TagCode = code;
            this.ConceptCode = conceptCode;
            this.Concept = conceptItem;
        }

        public virtual void InitValues(IDictionary<string, object> values)
        {
        }

        public uint TagCode { get; private set; }

        public uint ConceptCode { get; private set; }

        public PayrollConcept Concept { get; private set; }

        public bool SummaryFor(uint code)
        {
            uint[] summaryCodes = Concept.SummaryCodes().Select(x => x.Code).ToArray();
            return summaryCodes.Any(x => x==code);
        }

        protected void ExportXmlAttributes(XmlWriter xmlBuilder, IDictionary<string, string> attributes)
        {
            foreach (var a in attributes)
            {
                xmlBuilder.WriteStartAttribute(a.Key);
                xmlBuilder.WriteString(a.Value);
                xmlBuilder.WriteEndAttribute();
            }
        }

        public void ExportXmlTagRefer(TagRefer tagRefer, XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"period_base", tagRefer.PeriodBase.ToString()},
                {"code", tagRefer.Code.ToString()},
                {"code_order", tagRefer.CodeOrder.ToString()}
            };

            xmlBuilder.WriteStartElement("reference");
            foreach (var a in attributes)
            {
                xmlBuilder.WriteStartAttribute(a.Key);
                xmlBuilder.WriteString(a.Value);
                xmlBuilder.WriteEndAttribute();
            }
            xmlBuilder.WriteEndElement();
        }

        public void ExportXmlConcept(XmlWriter xmlBuilder)
        {
            Concept.ExportXml(xmlBuilder);
        }

        public virtual void ExportXmlResult(XmlWriter xmlBuilder)
        {
        }

        public virtual string ExportValueResult()
        {
            return @"";
        }

        public void ExportXmlNames(PayrollName tagName, PayrollTag tagItem, PayrollConcept tagConcept, XmlWriter xmlElement)
        {
            var attributes = new Dictionary<string, string>() {
                {"tag_name", tagItem.Name}, {"category", tagConcept.Name}
            };

            xmlElement.WriteStartElement("item");
            foreach (var a in attributes)
            {
                xmlElement.WriteStartAttribute(a.Key);
                xmlElement.WriteString(a.Value);
                xmlElement.WriteEndAttribute();
            }
             
            xmlElement.WriteStartElement("title");
            xmlElement.WriteString(tagName.Title);
            xmlElement.WriteEndElement();
                
            xmlElement.WriteStartElement("description");
            xmlElement.WriteString(tagName.Description);
            xmlElement.WriteEndElement();
                
            xmlElement.WriteStartElement("group");
            foreach (var g in tagName.GetGroups())
            {
                xmlElement.WriteStartAttribute(g.Key);
                xmlElement.WriteString(g.Value);
                xmlElement.WriteEndAttribute();
            }
            xmlElement.WriteEndElement();
                
            ExportXmlConcept(xmlElement);
            ExportXmlResult(xmlElement);

            xmlElement.WriteEndElement();
        }

        public void ExportXml(TagRefer tagRefer, PayrollName tagName, PayrollTag tagItem, PayrollConcept tagConcept, XmlWriter xmlElement)
        {
            ExportXmlTagRefer(tagRefer, xmlElement);
            ExportXmlNames(tagName, tagItem, tagConcept, xmlElement);
        }

        public IDictionary<string, string> ExportTitleValue(TagRefer tagRefer, PayrollName tagName, PayrollTag tagItem, PayrollConcept tagConcept)
        {
            return new Dictionary<string, string>() {
                { "title", tagName.Title }, 
                { "value", ExportValueResult() }, 
                { "image", ExportTypeOfResult(tagItem, tagConcept).ToString() } 
            };
        }

        public uint ExportTypeOfResult(PayrollTag tagItem, PayrollConcept tagConcept)
        {
            uint typeOfResult = TypeResult.TYPE_RESULT_SUMMARY;
            if (tagItem.TypeOfResult() != TypeResult.TYPE_RESULT_NULL)
            {
                typeOfResult = tagItem.TypeOfResult();
            }
            else if (tagConcept.TypeOfResult() != TypeResult.TYPE_RESULT_NULL)
            {
                typeOfResult = tagConcept.TypeOfResult();
            }
            else if (this.TypeOfResult() != TypeResult.TYPE_RESULT_NULL)
            {
                typeOfResult = this.TypeOfResult();
            }
            return typeOfResult;
        }

        protected virtual uint TypeOfResult()
        {
            return TypeResult.TYPE_RESULT_NULL;
        }

        virtual public decimal Payment()
        {
            return decimal.Zero;
        }

        virtual public decimal Deduction()
        {
            return decimal.Zero;
        }

        virtual public decimal TaxRelief()
        {
            return decimal.Zero;
        }

        virtual public decimal IncomeBase()
        {
            return decimal.Zero;
        }

        virtual public decimal EmployeeBase()
        {
            return decimal.Zero;
        }

        virtual public decimal EmployerBase()
        {
            return decimal.Zero;
        }

        virtual public decimal AfterReliefA()
        {
            return decimal.Zero;
        }

        virtual public decimal AfterReliefC()
        {
            return decimal.Zero;
        }

        virtual public decimal Amount()
        {
            return decimal.Zero;
        }

        #region get values from hash 

        protected int GetIntOrZeroValue(IDictionary<string, object> values, string key)
        {
            object obj = null;
            bool value = values.TryGetValue(key, out obj);

            if (!value || obj == null || !(obj is int)) return 0;
            return (int)obj;
        }

        protected uint GetUIntOrZeroValue(IDictionary<string, object> values, string key)
        {
            object obj = null;
            bool value = values.TryGetValue(key, out obj);

            if (!value || obj == null || !(obj is uint)) return 0;
            return (uint)obj;
        }

        protected decimal GetDecimalOrZeroValue(IDictionary<string, object> values, string key)
        {
            object obj = null;
            bool value = values.TryGetValue(key, out obj);

            if (!value || obj == null || !(obj is decimal)) return decimal.Zero;
            return (decimal)obj;
        }

        protected DateTime? GetDateOrNullValue(IDictionary<string, object> values, string key)
        {
            object obj = null;
            bool value = values.TryGetValue(key, out obj);

            if (!value || obj == null || !(obj is DateTime)) return null;
            return (DateTime)obj;
        }

        protected int[] GetArrayOfIntOrEmptyValue(IDictionary<string, object> values, string key)
        {
            object obj = null;
            bool value = values.TryGetValue(key, out obj);
            
            if (!value || obj == null || !(obj is int[])) return new int[0];
            return (int[])obj;
        }

        #endregion
    }
}
