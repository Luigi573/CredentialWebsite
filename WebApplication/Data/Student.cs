using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    [Table("Alumnos")]
    public class Student
    {
        [Key]
        [Column("ID_Alumno")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("ID_Centro")]
        public int CenterId { get; set; } = 1;
        [Required][Display(Name = "Nombre(s)")]
        [Column("nombres")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Name { get; set; } = String.Empty;
        [Required][Display(Name = "Apellidos")]
        [Column("apellidos")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string FamilyName { get; set; } = String.Empty;
        [Required]
        [Column("curp")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "La CURP debe tener 18 caracteres.")]
        public string CURP { get; set; } = String.Empty;
        [Required][Display(Name = "Periodo Escolar")]
        [Column("periodoEscolar")]
        [RegularExpression(@"^(202[2-9]|20[3-9]\d)-(202[2-9]|20[3-9]\d)$", ErrorMessage = "Ejemplo: '2025-2026' & '2026-2026'")]
        public string SchoolPeriod { get; set; } = String.Empty;
        [Required] [Display(Name = "Semestre")]
        [Column("semestre")]
        [Range(1, 6, ErrorMessage = "Seleccione un semestre válido")]
        public int Semester { get; set; }
        [Display(Name = "Activo")]
        [Column("activo")]
        public bool IsActive { get; set; } = true;
        [Column("nss")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El NSS debe tener 11 caracteres.")]
        public string? NSS { get; set; }
        [Display(Name = "Tipo de Sangre")]
        [Column("tipoSangre")]
        [RegularExpression(@"^(?:A|B|AB|O)[+-]$", ErrorMessage = "Tipo de sangre no válido. Ejemplo: 'A+'")]
        public string? BloodType { get; set; }
        [Display(Name = "Tutor")]
        [Column("tutor")]
        public string? TutorName { get; set; }
        [Display(Name = "Teléfono Tutor")]
        [Column("telefonoTutor")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Ejemplo: 945-845-3245")]
        public string? TutorPhone { get; set; }
        [Display(Name = "Foto de perfil")]
        [Column("imagen")]
        public string? ProfilePictureUrl { get; set; }
    }
}
