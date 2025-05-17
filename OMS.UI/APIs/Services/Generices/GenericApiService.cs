using AutoMapper;
using OMS.UI.APIs.Services.Interfaces.Tables;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;

namespace OMS.UI.APIs.Services.Generices
{
    public class GenericApiService<TDto, TModel> : GenericViewApiService<TDto, TModel>, IGenericApiService<TDto, TModel>
        where TDto : class
        where TModel : class
    {

        public GenericApiService(HttpClient httpClient, IMapper mapper, string endpoint) : base(httpClient, mapper, endpoint)
        {
        }

        public virtual async Task<bool> AddAsync(TModel model)
        {
            try
            {
                var dto = _mapper.Map<TDto>(model);
                var response = await _httpClient.PostAsJsonAsync(_endpoint, dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}"));
                    return false;
                }

                var createdDto = await response.Content.ReadFromJsonAsync<TDto>();
                if (createdDto != null) SetNewPrimaryKey(createdDto, model);
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(int id, TModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", _mapper.Map<TDto>(model));

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في تحديث البيانات في الخادم.\nStatus Code: {response.StatusCode}"));
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

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"Failed to delete item with ID {id}. Status Code: {response.StatusCode}"));
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

        public virtual async Task<bool> IsExistAsync(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Head, $"{_httpClient.BaseAddress}{_endpoint}/{id}");
                var response = await _httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        private void SetNewPrimaryKey(TDto dto, TModel model)
        {
            try
            {
                PropertyInfo? modelKeyProperty = typeof(TModel).GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

                if (modelKeyProperty == null) return;

                PropertyInfo? dtoKeyProperty = typeof(TDto).GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

                if (dtoKeyProperty == null) return;

                object? newId = dtoKeyProperty.GetValue(dto);
                modelKeyProperty.SetValue(model, newId);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

    }
}
