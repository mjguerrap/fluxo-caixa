namespace FluxoCaixa.Dominio
{
    public class Consolidado
    {
        public DateTime Data { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
        public decimal Saldo { get { return Credito - Debito; } }
    }
}