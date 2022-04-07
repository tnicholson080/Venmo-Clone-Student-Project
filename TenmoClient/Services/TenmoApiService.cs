using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        public decimal GetBalance()
        {
            RestRequest request = new RestRequest($"account/balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            CheckForError(response);
            return response.Data;
        }

        public RequestTransfer AddTransfer(RequestTransfer transfer)
        {
            RestRequest request = new RestRequest("transfer");
            request.AddJsonBody(transfer);
            IRestResponse<RequestTransfer> response = client.Post<RequestTransfer>(request);


            CheckForError(response);
            return response.Data;
        }

        public List<ApiUser> GetAllUsers()
        {
            RestRequest request = new RestRequest("users");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new HttpRequestException("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            return response.Data;
        }

        public List<RequestTransfer> GetAllTransfers()
        {
            RestRequest request = new RestRequest("transfer");
            IRestResponse<List<RequestTransfer>> response = client.Get<List<RequestTransfer>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new HttpRequestException("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            return response.Data;

        }

        public string GetUsernameByAccountId(int accountId)
        {
            RestRequest request = new RestRequest($"users/{accountId}");
            IRestResponse<string> response = client.Get<string>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new HttpRequestException("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            return response.Data;
        }
        // Add methods to call api here...


    }
}
