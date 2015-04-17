using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Wox.Plugin;

namespace Wox.Stash
{
    public class StashPlugin : IPlugin, ISettingProvider
    {
        private StashClient _client;
        private IPublicAPI _api;

        public List<Result> Query(Query query)
        {
            return _client.GetProjects().Select(p => new Result
            {
                Action = ctx =>
                {
                    _api.ChangeQuery(query.RawQuery + " " + p.key, true);
                    return false;
                },
                Title = p.key,
                SubTitle = p.name
            }).ToList();
        }

        public void Init(PluginInitContext context)
        {
            _client = new StashClient("https://mcpstash.cimpress.net/");
            _api = context.API;
        }

        public Control CreateSettingPanel()
        { 
            return new Label() { Content = "SETTINGS" };
        }
    }
}
