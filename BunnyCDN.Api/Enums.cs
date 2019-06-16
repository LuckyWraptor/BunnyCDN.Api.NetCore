using System;

namespace BunnyCDN.Api
{
    /// <summary>
    /// Billing Type enumerable
    /// </summary>
    public enum BillingType
    {
        /// Bitcoin payment
        Bitcoin = 1,
        /// Credit card payment
        CreditCard = 2,
        /// Monthly usage subtraction
        MonthlyUsage = 3,
        /// Coupon validation
        CouponCode = 5
    }
}