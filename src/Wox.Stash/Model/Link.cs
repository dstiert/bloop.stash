using RestSharp.Deserializers;

namespace Wox.Stash.Model
{
    public class Link
    {
        public string Url { get; set; }
        
        [DeserializeAs(Name = "rel")]
        public string Relative { get; set; }
    }
}
