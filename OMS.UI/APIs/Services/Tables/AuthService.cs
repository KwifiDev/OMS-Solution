using AutoMapper;
using OMS.UI.APIs.Dtos.Hybrid;
using OMS.UI.APIs.Dtos.Hybrid.OMS.API.Dtos.Hybrid;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace OMS.UI.APIs.Services.Tables
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public AuthService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _endpoint = ApiEndpoints.Authentication;
        }


        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            try
            {
                var dto = _mapper.Map<ChangePasswordDto>(model);

                var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/changepassword", dto);

                if (!response.IsSuccessStatusCode)
                {

                    LogError(new Exception($"حدث خطأ اثناء تعيين كلمة المرور.\nStatus Code: {response.StatusCode}"));
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<bool> CreateUserAsync(UserModel model)
        {
            try
            {
                var dto = _mapper.Map<UserDto>(model);

                var response = await _httpClient.PostAsJsonAsync(_endpoint, dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                int newUserId = await response.Content.ReadFromJsonAsync<int>();
                if (newUserId > 0) model.UserId = newUserId;
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var dto = _mapper.Map<RegisterDto>(model);

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/register", dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"اسم المستخدم او كملة المرور غير صحيحة.\nStatus Code: {response.StatusCode}"));
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<UserLoginModel?> SignInAsync(string username, string password)
        {
            var requestUserDto = new LoginDto { Username = username, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/login", requestUserDto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"اسم المستخدم او كملة المرور غير صحيحة.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var responseLoginDto = await response.Content.ReadFromJsonAsync<ResponseLoginDto>();

                return _mapper.Map<UserLoginModel>(responseLoginDto);

            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesByUserIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/userroles/{userId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<string>();
                }

                if (response.StatusCode == HttpStatusCode.NoContent) return Enumerable.Empty<string>();

                var roles = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
                return roles ?? Enumerable.Empty<string>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<string>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public async Task<bool> AddUserToRoleAsync(int userId, string roleName)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/usertorole", new { UserId = userId, RoleName = roleName });

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<bool> RemoveUserFromRoleAsync(int userId, string roleName)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/userfromrole", new { UserId = userId, RoleName = roleName });

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<bool> IsUserInRoleAsync(int userId, string roleName)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/userinrole", new { UserId = userId, RoleName = roleName });

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<bool> ChangeUserRolesAsync(int userId, ICollection<string> rolesToAdd, ICollection<string> rolesToRemove)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/changeuserroles", new { UserId = userId, RolesToAdd = rolesToAdd, RolesToRemove = rolesToRemove });

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }



        private void LogError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}", "Response Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
