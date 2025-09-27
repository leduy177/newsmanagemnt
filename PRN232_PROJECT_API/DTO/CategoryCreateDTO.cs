using System.Text.Json.Serialization;

namespace PRN232_PROJECT_API.DTO
{
    public class CategoryCreateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
