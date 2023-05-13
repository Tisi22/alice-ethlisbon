using System;
using System.Collections.Generic;

public class GetVoucher
{
    [Serializable]
    public class GetVoucherResponse
    {
        public string amount;
        public string signature;
    }

    [System.Serializable]
    public class Voucher
    {
        public string amount;
    }
}
