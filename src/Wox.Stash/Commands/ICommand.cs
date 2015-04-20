using Wox.Stash.Model;
using Wox.Plugin;

namespace Wox.Stash.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(Repo repo, IPublicAPI api);
    }
}
