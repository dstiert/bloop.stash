using Bloop.Plugin;

namespace Bloop.Stash
{
    public class PluginInput
    {
        private PluginInput() {}

        public static PluginInput Parse(Query query)
        {
            var cmd = new PluginInput();
            if(!string.IsNullOrEmpty(query.FirstSearch))
            {
                var projRepo = query.FirstSearch.Split('/');
                if(projRepo.Length > 0)
                {
                    cmd.ProjectKey = projRepo[0];
                }
                if(projRepo.Length > 1)
                {
                    cmd.RepoSlug = projRepo[1];
                }
            }

            if (!string.IsNullOrEmpty(query.SecondSearch))
            {
                cmd.CommandName = query.SecondSearch;
            }
            return cmd;
        }

        public string ProjectKey { get; set; }

        public string RepoSlug { get; set; }

        public string CommandName { get; set; }
    }
}
