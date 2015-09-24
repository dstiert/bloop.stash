using System.Collections.Generic;

namespace Bloop.Stash.Model
{
    public class RepoLinks
    {
        public List<Link> Clone { get; set; }

        public List<Link> Self { get; set; }
    }
}
