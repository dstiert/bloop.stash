using System.Linq;
using System.IO;
using Wox.Stash.Model;
using Wox.Stash.Settings;
using Wox.Plugin;

namespace Wox.Stash.Commands
{
    [Command]
    public class CloneCommand : ICommand
    {
        public string Name { get { return "Clone"; } }

        public void Execute(Repo repo, IPublicAPI api)
        {
            api.ShellRun(string.Format("git clone {0} {1}",
                repo.Links.Clone.Single(l => l.Name == PluginSettings.Instance.CloneMethod),
                Path.Combine(PluginSettings.Instance.CloneDestination, repo.Slug)), false);
            System.Diagnostics.Process.Start(repo.Links.Self[0].Href);
        }
    }
}
