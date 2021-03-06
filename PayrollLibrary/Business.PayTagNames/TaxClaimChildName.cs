using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollLibrary.Business.CoreItems;
using PayrollLibrary.Business.Core;

namespace PayrollLibrary.Business.PayTagNames
{
    class TaxClaimChildName : PayrollName
    {
        public TaxClaimChildName()
            : base(PayTagGateway.REF_UNKNOWN,
                "Tax benefit claim - child", "Tax benefit claim - child",
                PayNameGateway.VPAYGRP_TAX_SOURCE, PayNameGateway.HPAYGRP_UNKNOWN)
        {
        }
    }
}
