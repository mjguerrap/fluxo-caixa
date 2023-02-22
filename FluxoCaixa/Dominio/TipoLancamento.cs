using System.ComponentModel.DataAnnotations;

namespace FluxoCaixa.Dominio
{
    public enum TipoLancamento
    {
        [Display(Name = "Débito")]  
        Debito = 0,
        [Display(Name = "Crédito")]  
        Credito = 1
    }
}