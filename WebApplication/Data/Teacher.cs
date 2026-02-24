using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required][Display(Name = "Nombre")]
        [Column("nombre")]
        public string Name { get; set; }
        [Required][Display(Name = "Correo")]
        public string Email { get; set; }
        [Required][Display(Name = "Contraseña")]
        [MaxLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}