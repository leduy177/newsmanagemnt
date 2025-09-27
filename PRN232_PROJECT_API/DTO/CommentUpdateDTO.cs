using System.Text.Json.Serialization;

namespace PRN232_PROJECT_API.DTO
{
    public class CommentUpdateDTO
    {
       
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }

    }
}
