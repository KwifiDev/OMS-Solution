using OMS.UI.Models;

namespace OMS.UI.ViewModels.UserControls.Interfaces
{
    public interface IFindPersonViewModel
    {
        Task FindPerson();
        string? PersonId { set; }
        PersonModel? Person { get; }
    }
}
