using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.Core;
using PayrollLibrary.Business.CoreItems;

namespace PayrollLibrary.Business.Export
{
    public class PayrollResultsExporter
    {
        public PayrollResultsExporter(string company, string department, string person, string personNumber, PayrollProcess payroll)
        {
            PayrollNames = new PayNameGateway();
            PayrollNames.LoadModels();

            EmployerName = company;
            EmployeeDept = department;
            EmployeeName = person;
            EmployeeNumb = personNumber;

            Payroll = payroll;
            PPeriod = payroll.Period;
            Results = payroll.GetResults();
        }

        public PayrollProcess Payroll { get; private set; }

        public string EmployerName { get; private set; }
        public string EmployeeDept { get; private set; }
        public string EmployeeName { get; private set; }
        public string EmployeeNumb { get; private set; }

        public PayNameGateway PayrollNames { get; private set; }
        public PayrollPeriod PPeriod { get; private set; }
        public IDictionary<TagRefer, PayrollResult> Results { get; private set; }
 
    
        public bool MatchGroup(TagRefer tagResult, PayrollResult valResult, string grpPosition, PayNameGateway payNames)
        {
            PayrollName tagName = payNames.FindName(tagResult.Code);

            return tagName.isMatchVGroup(grpPosition);
        }

        public IDictionary<string, string> ItemExport(TagRefer tagResult, PayrollResult valResult, PayrollProcess payroll, PayNameGateway payNames)
        {
            PayrollTag tagItem = payroll.FindTag(valResult.TagCode);
            PayrollConcept tagConcept = payroll.FindConcept(valResult.ConceptCode);
            PayrollName tagName = payNames.FindName(tagResult.Code);

            return valResult.ExportTitleValue(tagResult, tagName, tagItem, tagConcept);
        }

        public IDictionary<string, string>[] GetSourceExport(IDictionary<TagRefer, PayrollResult> results, string grpPosition)
        {
            return results.Where((x) => (this.MatchGroup(x.Key, x.Value, grpPosition, PayrollNames)))
                .Select((x) => (this.ItemExport(x.Key, x.Value, Payroll, PayrollNames))).ToArray();
        }

        public IDictionary<string, string>[] GetSourceScheduleExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_SCHEDULE);
        }

        public IDictionary<string, string>[] GetSourcePaymentsExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_PAYMENTS);
        }

        public IDictionary<string, string>[] GetSourceTaxSourceExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_TAX_SOURCE);
        }

        public IDictionary<string, string>[] GetSourceTaxIncomeExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_TAX_INCOME);
        }

        public IDictionary<string, string>[] GetSourceInsIncomeExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_INS_INCOME);
        }

        public IDictionary<string, string>[] GetSourceTaxResultExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_TAX_RESULT);
        }

        public IDictionary<string, string>[] GetSourceInsResultExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_INS_RESULT);
        }

        public IDictionary<string, string>[] GetSourceSummaryExport()
        {
            return GetSourceExport(Results, PayNameGateway.VPAYGRP_SUMMARY);
        }

        public IDictionary<string, string>[] GetSourceEarningsExport()
        {
            IDictionary<string, string>[] partTime = GetSourceExport(Results, PayNameGateway.VPAYGRP_SCHEDULE);
            IDictionary<string, string>[] partPays = GetSourceExport(Results, PayNameGateway.VPAYGRP_PAYMENTS);
            return partTime.Concat(partPays).ToArray();
        }

        public IDictionary<string, string>[] GetSourceTaxInsIncomeExport()
        {
            IDictionary<string, string>[] partIns = GetSourceExport(Results, PayNameGateway.VPAYGRP_INS_INCOME);
            IDictionary<string, string>[] partTax = GetSourceExport(Results, PayNameGateway.VPAYGRP_TAX_INCOME);
            return partIns.Concat(partTax).ToArray();
        }

        public IDictionary<string, string>[] GetSourceTaxInsResultExport()
        {
           IDictionary<string, string>[] partIns = GetSourceExport(Results, PayNameGateway.VPAYGRP_INS_RESULT);
           IDictionary<string, string>[] partTax = GetSourceExport(Results, PayNameGateway.VPAYGRP_TAX_RESULT);
           return partIns.Concat(partTax).ToArray();
        }

        public IDictionary<string, string>[] GetSourceGrossIncomeExport()
        {
           IDictionary<string, string>[] partIncome = GetSourceExport(Results, PayNameGateway.VPAYGRP_SUMMARY);
           return new Dictionary<string, string>[] { partIncome.ElementAt(0).ToDictionary( key => key.Key, value => value.Value ) };
        }

        public IDictionary<string, string>[] GetSourceNettoIncomeExport()
        {
           IDictionary<string, string>[] partIncome = GetSourceExport(Results, PayNameGateway.VPAYGRP_SUMMARY);
           return new Dictionary<string, string>[] { partIncome.ElementAt(1).ToDictionary( key => key.Key, value => value.Value ) };
        }
    }
}
