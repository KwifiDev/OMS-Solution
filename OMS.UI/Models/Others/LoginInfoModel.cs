namespace OMS.UI.Models.Others
{
    public class LoginInfoModel
    {
        public UserLoginModel UserLogin { get; set; } = null!;
        public TokenModel TokenInfo { get; set; } = null!;
        public IEnumerable<string>? Claims { get; set; }
    }

}
