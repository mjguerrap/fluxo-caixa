using FluxoCaixa.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.AppExtentions;

public static class AddDbContextConfiguration
{
    public static IServiceCollection AddCustomDbContext(this WebApplicationBuilder builder)
    {
        AppDomain.CurrentDomain.SetData("DataDirectory",@"../Database");
        var connectionString = builder.Configuration.GetConnectionString("FluxoCaixaConnection") ?? "Data Source=|DataDirectory|FluxoCaixa.db";
        return builder.Services.AddDbContext<FluxoCaixaContext>(options => options.UseSqlite(connectionString));
    }
}