using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Services.StatusManagement
{
    public class SearchStatus : BaseStatus
    {
        public SearchStatus()
        {
            ClickContent = "بحث";
            IsModifiable = true;
        }
    }
}
