using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using RestSharp;
using Wox.Plugin;
using Wox.Stash.Dto;
using Wox.Stash.Model;

namespace Wox.Stash
{
    public class StashPlugin : IPlugin, ISettingProvider
    {
        private IRestClient client;

        public List<Result> Query(Query query)
        {
            var request = new RestRequest("/rest/api/1.0/projects");
            var result = this.client.Execute<PagedResult<Project>>(request);

            return result.Data.values.Select(p => new Result
            {
                Action = ctx => false,
                Title = p.key,
                SubTitle = p.name
            }).ToList();
        }

        public void Init(PluginInitContext context)
        {
            this.client = new RestClient("https://mcpstash.cimpress.net/");
        }

        public Control CreateSettingPanel()
        { 
            return new Label() { Content = "SETTINGS" };
        }
    }
}
