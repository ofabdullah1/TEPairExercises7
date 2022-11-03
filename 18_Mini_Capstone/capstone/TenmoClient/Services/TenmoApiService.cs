using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;
       

        public TenmoApiService(string apiUrl) : base(apiUrl)
        {
            ApiUrl = apiUrl;

        }

        public Account GetAccount()
        {
            RestRequest request = new RestRequest($"{ApiUrl}account");
            IRestResponse<Account> response = client.Get<Account>(request);
           
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }

        }

        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest($"{ApiUrl}tenmo_user");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }





        // Add methods to call api here...


    }
}
