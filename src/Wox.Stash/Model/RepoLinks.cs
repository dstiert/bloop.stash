using System.Collections.Generic;

namespace Wox.Stash.Model
{
    public class RepoLinks
    {
        public List<CloneLink> Clone { get; set; }

        public List<Link> Self { get; set; }
    }
}
