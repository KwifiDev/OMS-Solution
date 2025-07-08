using OMS.Common.Enums;

namespace OMS.BL.Models.Hybrid
{
    public class CheckDiscountAppliedModel
    {
        public required int ServiceId { get; set; }

        public required EnClientType ClientType { get; set; }
    }
}
