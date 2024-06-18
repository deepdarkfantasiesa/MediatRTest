using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MediatRTest.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [AllowNull]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
