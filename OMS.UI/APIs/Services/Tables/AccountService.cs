using AutoMapper;
using OMS.UI.APIs.Dtos.StoredProcedureParams;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;


namespace OMS.UI.APIs.Services.Tables
{
    public class AccountService : GenericApiService<AccountDto, AccountModel>, IAccountService
    {


        public AccountService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Accounts)
        {
        }

        public async Task<AccountModel?> GetByClientIdAsync(int clientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/clientid/{clientId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم للمعرف: {clientId}.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var dto = await response.Content.ReadFromJsonAsync<AccountDto>();
                return dto != null ? _mapper.Map<AccountModel>(dto) : null;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public async Task<bool> DepositIntoAccountAsync(AccountTransactionModel model)
        {
            try
            {
                var dto = _mapper.Map<AccountTransactionDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/deposit", dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}"));
                    return false;
                }

                var accountTransactionDto = await response.Content.ReadFromJsonAsync<AccountTransactionDto>();
                
                return accountTransactionDto?.TransactionStatus == Common.Enums.EnAccountTransactionStatus.Success;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }

        }

        public async Task<bool> WithdrawFromAccountAsync(AccountTransactionModel model)
        {
            try
            {
                var dto = _mapper.Map<AccountTransactionDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/withdraw", dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}"));
                    return false;
                }

                var accountTransactionDto = await response.Content.ReadFromJsonAsync<AccountTransactionDto>();

                return accountTransactionDto?.TransactionStatus == Common.Enums.EnAccountTransactionStatus.Success;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

    }
}
