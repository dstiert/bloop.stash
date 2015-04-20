using System;
using Wox.Plugin;

namespace Wox.Stash
{
    public class Command
    {
        private Command() {}

        public static Command Parse(Query query)
        {
            var cmd = new Command();
            if(query.ActionParameters.Count > 0)
            {
                var projRepo = query.ActionParameters[0].Split('/');
                if(projRepo.Length > 0)
                {
                    cmd.ProjectKey = projRepo[0];
                }
                if(projRepo.Length > 1)
                {
                    cmd.RepoSlug = projRepo[1];
                }
            }
            return cmd;
        }

        public string ProjectKey { get; set; }

        public string RepoSlug { get; set; }

        public CommandType? Operation { get; set; }
    }
}
