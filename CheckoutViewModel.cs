using System.ComponentModel.DataAnnotations;

namespace PizzariaWeb.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório")]
        [Display(Name = "Endereço de entrega")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Forma de pagamento é obrigatória")]
        [Display(Name = "Forma de pagamento")]
        public string FormaPagamento { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
