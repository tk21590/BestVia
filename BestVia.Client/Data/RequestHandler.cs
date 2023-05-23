using BestVia.Models;
using Blazored.SessionStorage;
using Newtonsoft.Json;
using Radzen;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BestVia.Client.Data
{
    public class RequestHandler : DelegatingHandler
    {
        private readonly NotificationService _notificationService;
        private ISessionStorageService _sessionStorageService;

        public RequestHandler(NotificationService notificationService, ISessionStorageService sessionStorageService)
        {
            _notificationService = notificationService;
            _sessionStorageService = sessionStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt_token = await _sessionStorageService.GetItemAsStringAsync("jwt_token");
            if (!string.IsNullOrEmpty(jwt_token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt_token.Replace("\"",string.Empty));
            }
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ApiRespone apiRespone = JsonConvert.DeserializeObject<ApiRespone>(content);

                    Helper.ShowNotification(_notificationService, apiRespone);
                }
                catch (Exception)
                {

                    throw;
                }
            

            }
            return response;
        }
    }
}
