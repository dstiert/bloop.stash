using Bloop.Plugin;
using Bloop.Stash.Commands;
using Bloop.Stash.Model;

namespace Bloop.Stash
{
    public class Converter
    {
        private string _actionKeyword;
        private IPublicAPI _api;

        public Converter(string actionKeyword, IPublicAPI api)
        {
            _actionKeyword = actionKeyword;
            _api = api;
        }

        public Result ToResult(Project project)
        {
            return new Result
            {
                Action = ctx =>
                {
                    _api.ChangeQuery(_actionKeyword + " " + project.key + "/", true);
                    return false;
                },
                Title = project.key,
                SubTitle = project.name,
                IcoPath = "images\\icon.png"
            };
        }

        public Result ToResult(Repo repo)
        {
            return new Result
            {
                Action = ctx =>
                {
                    _api.ChangeQuery(_actionKeyword + " " + repo.Project.key + "/" + repo.Slug + " ", true);
                    return false;
                },
                Title = repo.Name,
                SubTitle = repo.Slug,
                IcoPath = "images\\icon.png"
            };
        }

        public Result ToResult(ICommand cmd, Repo repo)
        {
            return new Result
            {
                Action = ctx =>
                    {
                        cmd.Execute(repo);
                        return true;
                    },
                Title = cmd.Name
            };
        }
    }
}
