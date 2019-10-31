using App172S.Helpers;
using App172S.Models.Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App172S.Services
{
    public class ApiService
    {
        public ApiService()
        {

        }

        public static async Task<ServiceResponse> Get<T>(string url, string token = "", string tokenType = "Bearer", int timeOut = 60)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(timeOut);

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("{0} {1}", tokenType, token));
                }

                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse()
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(responseData);
                return new ServiceResponse()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Result = res
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public static async Task<ServiceResponse> Post<T>(byte[] data, string url, string token = "", string tokenType = "Bearer", int timeOut = 300)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(timeOut);

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("{0} {1}", tokenType, token));
                }
                var myContent = JsonConvert.SerializeObject(data);
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(data);
                content.Add(baContent);
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse()
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(responseData);
                return new ServiceResponse()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Result = res
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public static async Task<ServiceResponse> Post<T>(string url, string token = "", string tokenType = "Bearer", int timeOut = 300)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(timeOut);

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("{0} {1}", tokenType, token));
                }
                var response = await client.PostAsync(url,null);
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse()
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(responseData);
                return new ServiceResponse()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Result = res
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
