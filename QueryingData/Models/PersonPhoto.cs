namespace QueryingData.Models
{
    public class PersonPhoto
    {
        public int PersonPhotoId { get; set; }
        public string Caption { get; set; }
        public byte[] Photo { get; set; }

        // Se BloggingContext for Fluent API code
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
