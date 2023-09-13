using System.ComponentModel.DataAnnotations;

namespace Backend.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
