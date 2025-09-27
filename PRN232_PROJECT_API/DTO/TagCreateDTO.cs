using System.Text.Json.Serialization;

namespace PRN232_PROJECT_API.DTO
{
    public class TagCreateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
