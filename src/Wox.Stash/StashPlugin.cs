using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System;
using Wox.Plugin;
using Wox.Stash.Settings;
using System.IO;
using Wox.Stash.Commands;

namespace Wox.Stash
{
    public class StashPlugin : IPlugin, ISettingProvider
    {
        private Converter _converter;
        private StashClient _client;
        private IPublicAPI _api;
        private IList<ICommand> _commands;

        public static string ActionKeyword;

        public List<Result> Query(Query query)
        {
            if(string.IsNullOrEmpty(PluginSettings.Instance.StashUrl))
            {
                return OpenSettingsResult();
            }

            try
            {
                var cmd = PluginInput.Parse(query);

                var projects = _client.GetProjects();

                if (string.IsNullOrEmpty(cmd.ProjectKey))
                {
                    return projects.Select(_converter.ToResult).ToList();
                }

                if (!projects.Any(p => p.key.ToLower() == cmd.ProjectKey.ToLower()))
                {
                    return projects.Where(p => p.key.ToLower().Contains(cmd.ProjectKey.ToLower())).Select(_converter.ToResult).ToList();
                }

                var repos = _client.GetRepos(cmd.ProjectKey);

                if (string.IsNullOrEmpty(cmd.RepoSlug))
                {
                    return repos.Select(_converter.ToResult).ToList();
                }

                var repo = repos.SingleOrDefault(r => r.Slug == cmd.RepoSlug);

                if (repo != null)
                {
                    return _commands.Select(c => _converter.ToResult(c, repo)).ToList();
                }

                return repos.Where(r => r.Slug.ToLower().Contains(cmd.RepoSlug.ToLower())).Select(_converter.ToResult).ToList();
            }
            catch(Exception e)
            {
                return new List<Result> { new Result { Title = "Error", SubTitle = e.Message } };
            }
        }

        public void Init(PluginInitContext context)
        {
            PluginSettings.LoadSettings(Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "settings.json"));
            _commands = GetCommandList();
            PluginSettings.Instance.SettingsChanged = OnSettingsChanged;
            _client = new StashClient(PluginSettings.Instance.StashUrl);
            _api = context.API;
            _converter = new Converter(context.CurrentPluginMetadata.ActionKeyword, _api);
        }

        public Control CreateSettingPanel()
        { 
            return new PluginSettingsControl();
        }

        private void OnSettingsChanged()
        {
            _client = new StashClient(PluginSettings.Instance.StashUrl);
        }

        private List<Result> OpenSettingsResult()
        {
            return new List<Result> { new Result 
                { 
                    Action = ctx => { _api.OpenSettingDialog(); return true; },
                    Title = "Open Settings",
                    SubTitle = "Configure Stash Plugin",
                    IcoPath = "images\\icon.png"
                }}.ToList();
        }

        private List<ICommand> GetCommandList()
        {
            return System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(CommandAttribute), false).Any())
                .Select(t => Activator.CreateInstance(t))
                .Cast<ICommand>()
                .ToList();
        }
    }
}
