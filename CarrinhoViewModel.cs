using System.Collections.Generic;

namespace PizzariaWeb.ViewModels
{
    public class CarrinhoViewModel
    {
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
        public decimal Subtotal => Itens.Sum(i => i.Subtotal);
        public decimal TaxaEntrega => 5.00m;
        public decimal Total => Subtotal + TaxaEntrega;
    }

    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string Tamanho { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        public decimal Subtotal => Preco * Quantidade;
    }
}
