using System;
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
    public class SavingsPensionsConcept : PayrollConcept
    {
        static readonly uint TAG_AMOUNT_BASE = PayTagGateway.REF_INSURANCE_SOCIAL_BASE.Code;

        public SavingsPensionsConcept(uint tagCode, IDictionary<string, object> values)
            : base(PayConceptGateway.REFCON_SAVINGS_PENSIONS, tagCode)
        {
            InitValues(values);
        }

        public uint InterestCode { get; private set; }

        public override void InitValues(IDictionary<string, object> values)
        {
            this.InterestCode = GetUIntOrZeroValue(values, "interest_code");
        }

        public override PayrollConcept CloneWithValue(uint code, IDictionary<string, object> values)
        {
            PayrollConcept newConcept = (SavingsPensionsConcept)this.Clone();
            newConcept.InitCode(code);
            newConcept.InitValues(values);
            return newConcept;
        }

        public override PayrollTag[] PendingCodes()
        {
            return new PayrollTag[] {
                new InsuranceSocialBaseTag()
            };
        }

        public override PayrollTag[] SummaryCodes()
        {
            return new PayrollTag[] {
                new IncomeNettoTag()
            };
        }

        public override uint TypeOfResult()
        {
            return TypeResult.TYPE_RESULT_DEDUCTION;
        }

        public override uint CalcCategory()
        {
            return PayrollConcept.CALC_CATEGORY_NETTO;
        }

        public override PayrollResult Evaluate(PayrollPeriod period, PayTagGateway tagConfig, IDictionary<TagRefer, PayrollResult> results)
        {
            decimal paymentIncome = 0m;

            if (!Interest())
            {
                paymentIncome = 0m;
            }
            else
            {
                IncomeBaseResult resultIncome = (IncomeBaseResult)GetResultBy(results, TAG_AMOUNT_BASE);

                paymentIncome = Math.Max(0m, resultIncome.EmployeeBase());
            }
            decimal contPaymentValue = InsuranceContribution(period, paymentIncome);

            var resultValues = new Dictionary<string, object>() { { "payment", contPaymentValue } };
            return new PaymentDeductionResult(TagCode, Code, this, resultValues);
        }

        private decimal InsuranceContribution(PayrollPeriod period, decimal paymentIncome)
        {
            decimal pensionFactor = PensionSavingsFactor(period);

            decimal contPaymentValue = FixInsuranceRoundUp(
                BigMulti(paymentIncome, pensionFactor));

            return contPaymentValue;
        }

        public bool Interest()
        {
            return InterestCode != 0;
        }

        public decimal PensionSavingsFactor(PayrollPeriod period)
        {
            decimal factor = 0m;
            if (period.Year() < 2013)
            {
                factor = 0m;
            }
            else
            {
                factor = 3.5m;
            }
            return decimal.Divide(factor, 100);
        }

        public override void ExportXml(XmlWriter xmlBuilder)
        {
            var attributes = new Dictionary<string, string>() {
                {"interest_code", InterestCode.ToString()}
            };
            xmlBuilder.WriteStartElement("spec_value");
            ExportXmlAttributes(xmlBuilder, attributes);
            xmlBuilder.WriteEndElement();
        }

        #region ICloneable Members

        public override object Clone()
        {
            SavingsPensionsConcept other = (SavingsPensionsConcept)this.MemberwiseClone();
            return other;
        }

        #endregion
    }
}
