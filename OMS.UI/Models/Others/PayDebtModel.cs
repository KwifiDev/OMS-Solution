using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Others
{
    public class PayDebtModel : BaseModel
    {

        private int _debtId;
        private string? _notes;
        private int _createdByUserId;
        private EnPayDebtStatus _enPayDebtStatus = EnPayDebtStatus.Empty;

        public int DebtId
        {
            get => _debtId;
            set => SetProperty(ref _debtId, value);
        }


        [MinLength(5, ErrorMessage = "الملاحظات على الاقل مكونة من خمس احرف")]
        public string? Notes
        {
            get => string.IsNullOrWhiteSpace(_notes) ? null : _notes;
            set => SetProperty(ref _notes, value);
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;
            set => SetProperty(ref _createdByUserId, value);
        }
        public EnPayDebtStatus PayDebtStatus
        {
            get => _enPayDebtStatus;
            set => SetProperty(ref _enPayDebtStatus, value);
        }
    }
}
