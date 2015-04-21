using System.Collections.Generic;

namespace Wox.Stash.Dto
{
    public class PagedResult<T>
    {
        public int size { get; set; }

        public int limit { get; set; }

        public bool isLastPage { get; set; }

        public List<T> values { get; set; }

        public int start { get; set; }

        public int nextPageStart { get; set; }
    }
}