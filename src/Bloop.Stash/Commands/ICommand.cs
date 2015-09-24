using Bloop.Stash.Model;

namespace Bloop.Stash.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(Repo repo);
    }
}
