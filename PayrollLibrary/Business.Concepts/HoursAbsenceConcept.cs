using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using PayrollLibrary.Business.Core;
using PayrollLibrary.Business.PayTags;
using PayrollLibrary.Business.Results;
using System.Xml;

namespace PayrollLibrary.Business.Concepts
{
    class HoursAbsenceConcept : PayrollConcept
    {
        public HoursAbsenceConcept(uint tagCode, IDictionary<string, object> values)
            : base(PayConceptGateway.REFCON_HOURS_ABSENCE, tagCode)
        {
            InitValues(values);
        }

        public int Hours { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.Hours = GetIntOrZeroValue(values, "hours");
        }

        public override PayrollConcept CloneWithValue(uint code, IDictionary<string, object> values)
        {
            PayrollConcept newConcept = (HoursAbsenceConcept)this.Clone();
            newConcept.InitCode(code);
            newConcept.InitValues(values);
            return newConcept;
        }

        public override PayrollTag[] PendingCodes()
        {
            return new PayrollTag[] { 
                new TimesheetWorkTag() 
            };
        }

        public override uint CalcCategory()
        {
            return PayrollConcept.CALC_CATEGORY_TIMES;
        }

        public override PayrollResult Evaluate(PayrollPeriod period, PayTagGateway tagConfig, IDictionary<TagRefer, PayrollResult> results)
        {
            var resultValues = new Dictionary<string, object>() { { "hours", Hours } };
            return new TermHoursResult(TagCode, Code, this, resultValues);
        }

        public override void ExportXml(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"hours", Hours.ToString()}
            };
            xmlBuilder.WriteStartElement("spec_value");
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

        #region ICloneable Members

        public override object Clone()
        {
            HoursAbsenceConcept other = (HoursAbsenceConcept)this.MemberwiseClone();
            return other;
        }

        #endregion
    }
}
