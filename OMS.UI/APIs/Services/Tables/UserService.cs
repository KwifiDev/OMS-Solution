using AutoMapper;
using OMS.UI.APIs.Dtos.Hybrid;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class UserService : GenericApiService<UserDto, UserModel>, IUserService
    {

        public UserService(IHttpClientFactory httpClientFactory, IMapper mapper)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Users)
        {
        }

        public async Task<UserLoginModel?> GetByUsernameAndPasswordAsync(string username, string password)
        {

            var requestUserDto = new RequestLoginDto { Username = username, Password = password };

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

                var responseLoginDto = await response.Content.ReadFromJsonAsync<ResponseLoginDto>();

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
    }
}
