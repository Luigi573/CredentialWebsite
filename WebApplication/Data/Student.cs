using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required][Display(Name = "Nombre(s)")]
        [Column("nombres")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Name { get; set; }
        [Required][Display(Name = "Apellidos")]
        [Column("apellidos")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string FamilyName { get; set; }
        [Required]
        [Column("curp")]
        public string CURP { get; set; } = String.Empty;
        [Required][Display(Name = "Periodo Escolar")]
        [Column("periodoEscolar")]
        public string SchoolPeriod { get; set; }
        [Required] [Display(Name = "Semestre")]
        [Column("semestre")]
        public int Semester { get; set; }
        [Column("activo")]
        public bool IsActive { get; set; } = true;
        public string? NSS { get; set; }
        [Display(Name = "Tipo de Sangre")]
        [Column("tipoSangre")]
        public string? BloodType { get; set; }
        [Display(Name = "Tutor")]
        [Column("tutor")]
        public string? TutorName { get; set; }
        [Display(Name = "Teléfono Tutor")]
        [Column("telefonoTutor")]
        public string? TutorPhone { get; set; }
        [Display(Name = "Foto de perfil")]
        [Column("imagen")]
        public string? ProfilePictureUrl { get; set; }
    }
}
