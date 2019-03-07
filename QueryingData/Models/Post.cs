using System.Collections.Generic;

namespace QueryingData.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public int BlogId { get; set; }
        public int? AuthorId { get; set; }
        public Blog Blog { get; set; }
        public Person Author { get; set; }
        public List<PostTag> Tags { get; set; }
    }
}
