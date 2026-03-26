using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    [Table("CiclosEscolares")]
    public class SchoolYear
    {
        [Required]
        [Column("ID_PeriodoEscolar")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        [Display(Name = "Ciclo Escolar")]
        public string Name { get; set; } = String.Empty;
        [Column("fechaInicio")]
        public DateTime StartDate { get; set; }
        [Column("fechaFin")]
        public DateTime EndDate { get; set; }
        [Column("activo")]
        public bool IsActive { get; set; }
    }
}