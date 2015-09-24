using System.Diagnostics;
using System.Linq;
using System.IO;
using Bloop.Stash.Model;
using Bloop.Stash.Settings;

namespace Bloop.Stash.Commands
{
    [Command]
    public class CloneCommand : ICommand
    {
        public string Name { get { return "Clone"; } }

        public void Execute(Repo repo)
        {
            var command = string.Format("/C git clone {0} {1}",
                repo.Links.Clone.Single(l => l.Name == PluginSettings.Instance.CloneMethod).Href,
                Path.Combine(PluginSettings.Instance.CloneDestination, repo.Slug));

            Process.Start("cmd.exe", command);
        }
    }
}
