using System.Collections.Generic;
using RestSharp;
using Wox.Stash.Dto;
using Wox.Stash.Model;

namespace Wox.Stash
{
    public class StashClient
    {
        private readonly IRestClient _client;

        public StashClient(string endpoint)
        {
            _client = new RestClient("https://mcpstash.cimpress.net/");
        }

        public List<Project> GetProjects()
        {
            var request = new RestRequest("/rest/api/1.0/projects", Method.GET);
            return _client.Execute<PagedResult<Project>>(request).Data.values;
        }

        public List<Repo> GetRepos(string projectKey)
        {
            var request = new RestRequest("/rest/api/1.0/projects/{projectKey}/repos", Method.GET);
            request.AddParameter("projectKey", projectKey);
            return _client.Execute<PagedResult<Repo>>(request).Data.values;
        }

    }
}
