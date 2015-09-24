using Bloop.Stash.Model;

namespace Bloop.Stash.Commands
{
    [Command]
    public class GoToCommand : ICommand
    {
        public string Name { get { return "Go To"; } }

        public void Execute(Repo repo)
        {
            System.Diagnostics.Process.Start(repo.Links.Self[0].Href);
        }
    }
}
