using OMS.UI.Models;
using OMS.UI.Services.StatusManagement;

namespace OMS.UI.ViewModels.UserControls.Interfaces
{
    public interface IFindPersonViewModel
    {
        Task FindPerson();
        string? PersonId { set; }
        PersonModel? Person { get; }
        SearchStatus Status { get; }
    }
}
