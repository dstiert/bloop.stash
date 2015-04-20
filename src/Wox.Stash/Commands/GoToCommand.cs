using Wox.Stash.Model;
using Wox.Plugin;

namespace Wox.Stash.Commands
{
    [Command]
    public class GoToCommand : ICommand
    {
        public string Name { get { return "Go To"; } }

        public void Execute(Repo repo, IPublicAPI api)
        {
            System.Diagnostics.Process.Start(repo.Links.Self[0].Href);
        }
    }
}
