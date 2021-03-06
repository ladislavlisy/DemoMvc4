using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using System.Xml;

namespace PayrollLibrary.Business.Results
{
    public class UnknownResult : PayrollResult
    {
        public UnknownResult(uint code, uint conceptCode, PayrollConcept conceptItem, IDictionary<string, object> values)
            : base(code, conceptCode, conceptItem)
        {
            InitValues(values);
        }

        public override void InitValues(IDictionary<string, object> values)
        {
        }
    }
}
