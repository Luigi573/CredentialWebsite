using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    [Table("Maestros")]
    public class Teacher : IdentityUser
    {
        [Required][Display(Name = "Nombre")]
        [Column("nombre")]
        public string Name { get; set; } = String.Empty;
    }
}