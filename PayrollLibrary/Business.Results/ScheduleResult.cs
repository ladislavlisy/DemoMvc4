using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;

namespace PayrollLibrary.Business.Results
{
    public class ScheduleResult : PayrollResult
    {
        public ScheduleResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public int[] WeekSchedule { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.WeekSchedule = GetArrayOfIntOrEmptyValue(values, "week_schedule");
        }

        public int Hours()
        {
            int weekHours = 0;
            if (WeekSchedule != null)
            {
                weekHours = WeekSchedule.Aggregate(0, (agr, dh) => (agr + dh));
            }
            return weekHours;
        }

        public override void ExportXmlResult(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"week_schedule", string.Join(",", WeekSchedule.Select(x => x.ToString()).ToArray())}
            };
            xmlBuilder.WriteStartElement("value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteString(XmlValue());
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            return Hours().ToString() + " hours";
        }

        public override string ExportValueResult()
        {
            return Hours().ToString() + " hours";
        }
    }
}
