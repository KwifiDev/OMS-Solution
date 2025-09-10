using AutoMapper;
using OMS.Common.Dtos.Hybrid;
using OMS.Common.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Services.WinLogger;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class UserService : GenericApiService<UserDto, UserModel>, IUserService
    {

        public UserService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Users, logService)
        {
        }


        public async Task<bool> UpdateMyUserAsync(int id, UserModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/updatemyuser/{id}", _mapper.Map<UserDto>(model));

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في تحديث البيانات في الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
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

        public async Task<UserLoginModel?> GetUserLoginByPersonIdAsync(int personId)
        {

            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{personId}/login");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في المعرف الشخصي.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var responseLoginDto = await response.Content.ReadFromJsonAsync<ResultLoginDto>();

                return _mapper.Map<UserLoginModel>(responseLoginDto);

            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }

        }

        public async Task<UserModel?> GetByPersonIdAsync(int personId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{personId}/personid");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في المعرف الشخصي.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var userDto = await response.Content.ReadFromJsonAsync<UserDto>();

                return _mapper.Map<UserModel>(userDto);

            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }


        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{personId}/id");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في المعرف الشخصي.\nStatus Code: {response.StatusCode}"));
                    return -1;
                }

                return await response.Content.ReadFromJsonAsync<int>();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return -1;
            }
        }

        public async Task<bool> UpdateUserActivationStatus(int userId, bool isActive)
        {
            try
            {
                var userActivationDto = new UserActivationDto { UserId = userId, IsActive = isActive };

                var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/updateactivation", userActivationDto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"حدث خطأ اثناء تحديث المستخدم.\nStatus Code: {response.StatusCode}\nContent: {await response.Content.ReadAsStringAsync()}"));
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

        public async Task<bool> CheckUsernameAvailable(int userId, string username)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/checkusernameavailable", new { UserId = userId, Username = username });

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Conflict)
                        LogError(new Exception($"اسم المستخدم محجوز من قبل مستخدم اخر.\nStatus Code: {response.StatusCode}\nInfo: {userId}, {username}"));
                    else
                        LogError(new Exception($"حدث خطأ اثناء التحقق من وجود اسم المستخدم.\nStatus Code: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}"));
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
    }
}
