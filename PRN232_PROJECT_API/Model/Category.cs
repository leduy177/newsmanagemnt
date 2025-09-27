namespace PRN232_PROJECT_API.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
