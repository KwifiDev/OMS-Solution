using CommunityToolkit.Mvvm.Messaging.Messages;
using OMS.UI.Models.Tables;

namespace OMS.UI.Services.ModelTransfer
{
    public class UserChangedMessage : ValueChangedMessage<UserModel?>
    {
        public UserChangedMessage(UserModel? user) : base(user)
        {
        }
    }
}
