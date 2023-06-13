using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public static int GetRandomId()
        {
            return (int)Guid.NewGuid().GetHashCode();
        }
    }

}
