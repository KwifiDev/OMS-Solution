using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables;

public class RevenueModel : BaseModel
{
    private int _revenueId;
    private decimal _amount;
    private string? _notes;
    private DateOnly _createdAt;

    [Key]
    public int RevenueId
    {
        get => _revenueId;
        set
        {
            SetProperty(ref _revenueId, value);
            OnPropertyChanged(nameof(RevenueIdDisplay));
        }
    }

    [Required(ErrorMessage = "سعر الخدمة مطلوب")]
    [Range(typeof(decimal), "10000", "10000000", ErrorMessage = "(العائد المادي يجب ان يكون بين(10,000 - 10,000,000")]
    public decimal Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    [MinLength(5, ErrorMessage = "الملاحظات على الاقل مكونة من 5 احرف")]
    public string? Notes
    {
        get => string.IsNullOrWhiteSpace(_notes) ? null : _notes;
        set
        {
            SetProperty(ref _notes, value);
            OnPropertyChanged(nameof(NotesDisplay));
        }
    }

    public DateOnly CreatedAt
    {
        get => _createdAt;
        set => SetProperty(ref _createdAt, value);
    }

    public string RevenueIdDisplay => _revenueId > 0 ? _revenueId.ToString() : "لا يوجد";
    public string NotesDisplay => string.IsNullOrWhiteSpace(_notes) ? "لا يوجد ملاحظات" : _notes;
}
