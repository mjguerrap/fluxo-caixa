using FluxoCaixa.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.AppExtentions;

public static class AddDbContextConfiguration
{
    public static IServiceCollection AddCustomDbContext(this WebApplicationBuilder builder)
    {
        var environment = builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
        var connectionString = builder.Configuration.GetConnectionString("FluxoCaixaConnection") ?? "Data Source=FluxoCaixa.db";
        return builder.Services.AddDbContext<FluxoCaixaContext>(options => options.UseSqlite(connectionString));
    }
}