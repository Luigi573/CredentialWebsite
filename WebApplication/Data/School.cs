using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    [Table("Centros")]
    public class School
    {
        [Key]
        [Column("ID_Centro")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required][Display(Name = "Nombre")]
        [Column("nombre")]
        public string Name { get; set; }
        [Required][Display(Name = "Clave")]
        [Column("clave")]
        public string Code { get; set; }
    }
}