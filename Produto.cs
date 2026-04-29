using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzariaWeb.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoPequena { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoMedia { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoGrande { get; set; }

        [Required]
        public string Categoria { get; set; }

        public string Ingredientes { get; set; }

        public string ImagemUrl { get; set; }

        public bool Destaque { get; set; } = false;

        public bool Disponivel { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
