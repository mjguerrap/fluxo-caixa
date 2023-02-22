using Microsoft.EntityFrameworkCore;
using FluxoCaixa.Dominio;

namespace FluxoCaixa.DB;
public class FluxoCaixaContext : DbContext
{
    public FluxoCaixaContext(){}
    public FluxoCaixaContext(DbContextOptions options) : base(options) { }
    public virtual DbSet<Lancamento> Lancamentos { get; set; } = null!;

}
