using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MediatRTest.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [AllowNull]
        public string RoleCode { get; set; }
    }
}
