using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzariaWeb.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ClienteNome { get; set; }

        [Required]
        [EmailAddress]
        public string ClienteEmail { get; set; }

        [Required]
        [Phone]
        public string ClienteTelefone { get; set; }

        [Required]
        public string EnderecoEntrega { get; set; }

        public string Observacoes { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TaxaEntrega { get; set; } = 5.00m;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        public string Status { get; set; } = "Pendente";

        public string FormaPagamento { get; set; }

        public bool Entregue { get; set; } = false;

        public ICollection<ItemPedido> Itens { get; set; }
    }
}
