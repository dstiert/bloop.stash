using Wox.Stash.Model;
namespace Wox.Stash.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(Repo repo);
    }
}
