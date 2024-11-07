using System.ComponentModel.DataAnnotations;

namespace TBP.Domain.Entites
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
