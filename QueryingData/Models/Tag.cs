using System.Collections.Generic;

namespace QueryingData.Models
{
    public class Tag
    {
        public string TagId { get; set; }

        public List<PostTag> Posts { get; set; }
    }
}
