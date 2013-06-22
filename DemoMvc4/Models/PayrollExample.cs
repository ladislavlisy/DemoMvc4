using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayrollLibrary.Business.Core;
using PayrollLibrary.Business.CoreItems;

namespace DemoMvc4.Models
{
    public class PayrollExample
    {
        static readonly uint INTEREST_YES = 1;
        static readonly uint INTEREST_NON = 1;
        static readonly uint INTEREST_TWO = 2;

        public PayrollExample()
        {
            DateTime periodNow = DateTime.Now;

            Period = new PayrollPeriod((uint)periodNow.Year, (byte)periodNow.Month);

            PayTags = new PayTagGateway();

            PayConcepts = new PayConceptGateway();

            PayProcess = new PayrollProcess(PayTags, PayConcepts, Period);

            PayNames = new PayNameGateway();
            PayNames.LoadModels();
        }

        private PayrollProcess PayProcess { get; set; }

        private PayNameGateway PayNames { get; set; }

        private PayTagGateway PayTags { get; set; }

        private PayConceptGateway PayConcepts { get; set; }

        private PayrollPeriod Period { get; set; }

        public bool CreateWorkingSchedule(int hoursWeekly, DateTime? dateFrom, DateTime? dateEnd)
        {
            var schedule_work_value = new Dictionary<string, object>() { { "hours_weekly", hoursWeekly } };
            var schedule_term_value = new Dictionary<string, object>() { { "date_from", dateFrom }, { "date_end", dateEnd } };

            PayProcess.AddTerm(PayTagGateway.REF_SCHEDULE_WORK, schedule_work_value);
            PayProcess.AddTerm(PayTagGateway.REF_SCHEDULE_TERM, schedule_term_value);
            return true;
        }

        public bool CreateSalary(decimal salary)
        {
            var salary_amount_value = new Dictionary<string, object>() { { "amount_monthly", salary } };
            PayProcess.AddTerm(PayTagGateway.REF_SALARY_BASE, salary_amount_value);
            return true;
        }

        public bool CreateTaxDeclaration(bool tax_yes, bool tax_declaration)
        {
            uint interest_code = (tax_yes ? INTEREST_YES : INTEREST_NON);
            uint declared_code = (tax_declaration ? INTEREST_YES : INTEREST_NON);
            var interest_value = new Dictionary<string, object>() { { "interest_code", interest_code }, { "declare_code", declared_code } };

            PayProcess.AddTerm(PayTagGateway.REF_TAX_INCOME_BASE, interest_value);
            return true;
        }

        public bool CreateHealthInsurance(bool ins_yes, bool min_yes)
        {
            uint interest_code = (ins_yes ? INTEREST_YES : INTEREST_NON);
            uint minimum_asses = (min_yes ? INTEREST_YES : INTEREST_NON);
            var interest_value = new Dictionary<string, object>() { { "interest_code", interest_code }, { "minimum_asses", minimum_asses } };

            PayProcess.AddTerm(PayTagGateway.REF_INSURANCE_HEALTH_BASE, interest_value);
            PayProcess.AddTerm(PayTagGateway.REF_INSURANCE_HEALTH, interest_value);
            PayProcess.AddTerm(PayTagGateway.REF_TAX_EMPLOYERS_HEALTH, interest_value);
            return true;
        }

        public bool CreateSocialInsurance(bool ins_yes)
        {
            uint interest_code = (ins_yes ? INTEREST_YES : INTEREST_NON);
            var interest_value = new Dictionary<string, object>() { { "interest_code", interest_code } };

            PayProcess.AddTerm(PayTagGateway.REF_INSURANCE_SOCIAL_BASE, interest_value);
            PayProcess.AddTerm(PayTagGateway.REF_INSURANCE_SOCIAL, interest_value);
            PayProcess.AddTerm(PayTagGateway.REF_TAX_EMPLOYERS_SOCIAL, interest_value);
            return true;
        }

        public bool CreateTaxBenefits(bool payer_yes, bool disab1_yes, bool disab2_yes, bool disab3_yes, bool study_yes, uint childCount )
        {
            uint payer_code = (payer_yes ? INTEREST_YES : INTEREST_NON);
            uint disx1_code = (disab1_yes ? INTEREST_YES : INTEREST_NON);
            uint disx2_code = (disab2_yes ? INTEREST_YES : INTEREST_NON);
            uint disx3_code = (disab3_yes ? INTEREST_YES : INTEREST_NON);
            uint study_code = (study_yes ? INTEREST_YES : INTEREST_NON);

            var relief_payer = new Dictionary<string, object>() { { "relief_code", payer_code } };
            var relief_disab = new Dictionary<string, object>() {
                { "relief_code_1", disx1_code }, { "relief_code_2", disx2_code }, { "relief_code_3", disx3_code } 
            };
            var relief_study = new Dictionary<string, object>() { { "relief_code", study_code } };
            var relief_child = new Dictionary<string, object>() { { "relief_code", INTEREST_YES } };

            PayProcess.AddTerm(PayTagGateway.REF_TAX_CLAIM_PAYER, relief_payer);
            PayProcess.AddTerm(PayTagGateway.REF_TAX_CLAIM_DISABILITY, relief_disab);
            PayProcess.AddTerm(PayTagGateway.REF_TAX_CLAIM_STUDYING, relief_study);
            for (uint index = 0; index < childCount; index++)
            {
                PayProcess.AddTerm(PayTagGateway.REF_TAX_CLAIM_CHILD, relief_child);
            }

            return true;
        }

        public bool CreateIncomeCalculation()
        {
            var empty_value = new Dictionary<string, object>() { };

            PayProcess.AddTerm(PayTagGateway.REF_INCOME_GROSS, empty_value);
            var result_tag = PayProcess.AddTerm(PayTagGateway.REF_INCOME_NETTO, empty_value);
            var result = PayProcess.Evaluate(result_tag);
            return true;
        }


    }
}