using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    public class Teacher : IdentityUser
    {
        [Required][Display(Name = "Nombre Completo")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10-100 caracteres")]
        [Column("nombre")]
        public string Name { get; set; } = String.Empty;
        [Required][Display(Name = "Centro")]
        [Column("ID_Centro")]
        public int SchoolId { get; set; }
        public School? School { get; set; }
    }
}