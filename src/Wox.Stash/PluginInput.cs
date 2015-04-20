using System;
using Wox.Plugin;

namespace Wox.Stash
{
    public class PluginInput
    {
        private PluginInput() {}

        public static PluginInput Parse(Query query)
        {
            var cmd = new PluginInput();
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

            if(query.ActionParameters.Count > 1)
            {
                cmd.CommandName = query.ActionParameters[1];
            }
            return cmd;
        }

        public string ProjectKey { get; set; }

        public string RepoSlug { get; set; }

        public string CommandName { get; set; }
    }
}
