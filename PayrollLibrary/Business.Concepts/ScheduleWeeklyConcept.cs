﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using PayrollLibrary.Business.Core;
using PayrollLibrary.Business.PayTags;
using PayrollLibrary.Business.Results;
using System.Xml;
using PayrollLibrary.Business.Symbols;

namespace PayrollLibrary.Business.Concepts
{
    public class ScheduleWeeklyConcept : PayrollConcept
    {
        public ScheduleWeeklyConcept(uint tagCode, IDictionary<string, object> values)
            : base(PayConceptGateway.REFCON_SCHEDULE_WEEKLY, tagCode)
        {
            InitValues(values);
        }

        public int HoursWeekly { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.HoursWeekly = GetIntOrZeroValue(values, "hours_weekly");
        }

        public override PayrollConcept CloneWithValue(uint code, IDictionary<string, object> values)
        {
            PayrollConcept newConcept = (ScheduleWeeklyConcept)this.Clone();
            newConcept.InitCode(code);
            newConcept.InitValues(values);
            return newConcept;
        }

        public override uint TypeOfResult()
        {
            return TypeResult.TYPE_RESULT_SCHEDULE;
        }

        public override uint CalcCategory()
        {
            return PayrollConcept.CALC_CATEGORY_TIMES;
        }

        public override PayrollResult Evaluate(PayrollPeriod period, PayTagGateway tagConfig, IDictionary<TagRefer, PayrollResult> results)
        {
            int[] hoursWeek = ComputeResultValue(period, HoursWeekly);
            var resultValues = new Dictionary<string, object>() { { "week_schedule", hoursWeek } };
            return new ScheduleResult(TagCode, Code, this, resultValues);
        }

        private int[] ComputeResultValue(PayrollPeriod period, int hoursWeekly)
        {
            int hoursDaily = hoursWeekly / 5;
            int[] hoursWeek = new int[] { hoursDaily, hoursDaily, hoursDaily, hoursDaily, hoursDaily, 0, 0 };
            return hoursWeek;
        }

        public override void ExportXml(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"hours_weekly", HoursWeekly.ToString()}
            };
            xmlBuilder.WriteStartElement("spec_value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteEndElement();
        }

        public string XmlValue()
        {
            return HoursWeekly.ToString() + " hours";
        }

        public override string ExportValueResult()
        {
            return HoursWeekly.ToString() + " hours";
        }

        #region ICloneable Members

        public override object Clone()
        {
            ScheduleWeeklyConcept other = (ScheduleWeeklyConcept)this.MemberwiseClone();
            return other;
        }

        #endregion
    }
}
