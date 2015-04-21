using System;
using System.Collections.Generic;
using RestSharp;
using Wox.Stash.Dto;
using Wox.Stash.Model;
using System.Net;

namespace Wox.Stash
{
    public class StashClient
    {
        private readonly IRestClient _client;

        public StashClient(string endpoint)
        {
            _client = new RestClient(endpoint);
        }

        public List<Project> GetProjects()
        {
            var request = new RestRequest("/rest/api/1.0/projects", Method.GET);
            return ExecuteRequest<PagedResult<Project>>(request).Data.values;
        }

        public List<Repo> GetRepos(string projectKey)
        {
            var request = new RestRequest("/rest/api/1.0/projects/{projectKey}/repos", Method.GET);
            request.AddUrlSegment("projectKey", projectKey);
            return ExecuteRequest<PagedResult<Repo>>(request).Data.values;
        }

        private IRestResponse<T> ExecuteRequest<T>(IRestRequest request) where T : new()
        {
            request.AddQueryParameter("limit", "1000");
            var response = this._client.Execute<T>(request);
            this.ProcessResponse(response);
            return response;
        }

        private IRestResponse ExecuteRequest(IRestRequest request)
        {
            var response = this._client.Execute(request);
            this.ProcessResponse(response);
            return response;
        }

        private void ProcessResponse(IRestResponse response)
        {
            switch (response.ResponseStatus)
            {
                case ResponseStatus.None:
                case ResponseStatus.Aborted:
                case ResponseStatus.Error:
                    throw new Exception("Non-HTTP error contacting stash", response.ErrorException);
                case ResponseStatus.TimedOut:
                    throw new TimeoutException("Connection to stash timed out.");
                case ResponseStatus.Completed:
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return;
                    }

                    throw new Exception(string.Format("Stash request failed. HTTP {0} Content: {1}", response.StatusCode, response.Content));
            }
        }

    }
}
