using AutoMapper;
using OMS.Common.Enums;
using OMS.UI.APIs.Dtos.StoredProcedureParams;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Services.WinLogger;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;


namespace OMS.UI.APIs.Services.Tables
{
    public class AccountService : GenericApiService<AccountDto, AccountModel>, IAccountService
    {
        public AccountService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Accounts, logService)
        {
        }

        public async Task<AccountModel?> GetByClientIdAsync(int clientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/by-client/{clientId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return null;
                }

                if (response.StatusCode == HttpStatusCode.NoContent) return null;

                var dto = await response.Content.ReadFromJsonAsync<AccountDto>();
                return dto != null ? _mapper.Map<AccountModel>(dto) : null;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public async Task<bool> StartTransactionAsync(AccountTransactionModel model)
        {
            try
            {
                var dto = _mapper.Map<AccountTransactionDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/transactions", dto);

                if (response.IsSuccessStatusCode)
                {
                    var accountTransactionDto = await response.Content.ReadFromJsonAsync<AccountTransactionDto>();
                    return (model.TransactionStatus = accountTransactionDto!.TransactionStatus) == EnAccountTransactionStatus.Success;
                }

                LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                return false;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }

        }

    }
}
