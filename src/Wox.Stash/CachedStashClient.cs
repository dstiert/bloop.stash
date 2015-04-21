using System;
using System.Collections.Generic;
using Wox.Stash.Model;

namespace Wox.Stash
{
    public class CachedStashClient : StashClient
    {
        private List<Project> _projectCache;
        private DateTime _projectAge;

        private readonly Dictionary<string, List<Repo>> _repoCache;
        private readonly Dictionary<string, DateTime> _repoAge;

        public CachedStashClient(string endpoint) : base(endpoint)
        {
            _repoCache = new Dictionary<string, List<Repo>>();
            _repoAge = new Dictionary<string, DateTime>();
        }

        public override List<Project> GetProjects()
        {
            if (_projectCache == null || _projectAge - DateTime.UtcNow > TimeSpan.FromMinutes(5))
            {
                _projectCache = base.GetProjects();
                _projectAge = DateTime.UtcNow;
            }
            return _projectCache;
        }

        public override List<Repo> GetRepos(string projectKey)
        {
            DateTime t;
            if (!_repoAge.TryGetValue(projectKey, out t) || t - DateTime.UtcNow > TimeSpan.FromMinutes(5))
            {
                _repoCache[projectKey] = base.GetRepos(projectKey);
                _repoAge[projectKey] = DateTime.UtcNow;
            }
            return _repoCache[projectKey];
        }
    }
}