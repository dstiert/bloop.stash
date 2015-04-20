namespace Wox.Stash.Model
{
    public class Repo
    {
        public string Slug { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public Project Project { get; set; }

        public RepoLinks Links { get; set; }
    }
}