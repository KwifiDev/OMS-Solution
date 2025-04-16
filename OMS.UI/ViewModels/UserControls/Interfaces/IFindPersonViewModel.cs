using OMS.UI.Models;
using OMS.UI.Services.StatusManagement;
using static OMS.UI.ViewModels.UserControls.FindPersonViewModel;

namespace OMS.UI.ViewModels.UserControls.Interfaces
{
    public interface IFindPersonViewModel
    {
        event EventHandler<PersonFoundEventArgs>? PersonFound;
        Task FindPerson();
        string? PersonId { set; }
        PersonModel? Person { get; }
        SearchStatus Status { get; }
    }
}
