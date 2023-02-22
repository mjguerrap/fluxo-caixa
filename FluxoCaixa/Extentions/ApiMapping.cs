
using Asp.Versioning;
using FluxoCaixa.Model;
using FluxoCaixa.Servicos;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoCaixa.Extentions
{
    public static class ApiMapping
    {
        public static IApplicationBuilder APIMapping(this WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                    .HasApiVersion(new ApiVersion(1.0))
                    .ReportApiVersions()
                    .Build();

            app.MapGet("/", () => "Apresentação do Teste sobre Fluxo de Caixa!");    
            
            app.MapGet("/consolidado", async (IConsolidadoService cs, int mes, int ano) => await cs.ObtemConsolidado(mes, ano))
            .WithApiVersionSet(versionSet).MapToApiVersion(1.0);         
            
            app.MapGet("/lancamentos", async (ILancamentoService ls, int mes, int ano, int pagina) => {
            var result = await ls.Buscar(mes, ano, pagina);
            return result is null ? Results.NotFound("Lancamento não encontrado") : Results.Ok(result); 
            }).WithApiVersionSet(versionSet).MapToApiVersion(1.0)
            .WithMetadata(new SwaggerOperationAttribute("Buscar lançamentos", "Limitado a 1000 lancamentos por página. Página deve iniciar com 1(um)"));
            
            app.MapGet("/lancamentos/{id}", async (ILancamentoService ls, int id) => {
                var result = await ls.Buscar(id);
                return result is null ?  Results.NotFound("Lancamento não encontrado") : Results.Ok(result);
                }).WithApiVersionSet(versionSet).MapToApiVersion(1.0);
            
            app.MapPost("/lancamentos", async (ILancamentoService ls, LancamentoPayload lancamento) =>
            {
                var result = await ls.Novo(lancamento);
                return Results.Created($"/lancamentos/{result.Id}", result);
            }).WithApiVersionSet(versionSet).MapToApiVersion(1.0)
            .WithMetadata(new SwaggerOperationAttribute("Cadastrar novo lançamento", "Valores válidos para [tipoLancamento] são 0(zero) para Débito, 1(um) para Crédito."));
            
            app.MapPut("/lancamentos/{id}", async (ILancamentoService ls, LancamentoPayload atualizarLancamento, int id) =>
            {
                var result = await ls.Ajustar(id, atualizarLancamento);
                return result is null ?  Results.NotFound() : Results.NoContent();
            }).WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithMetadata(new SwaggerOperationAttribute("Alterar um lançamento", "Valores válidos para [tipoLancamento] são 0(zero) para Débito, 1(um) para Crédito."));
            
            app.MapDelete("/lancamentos/{id}", async (ILancamentoService ls, int id) =>
            {
                var lancamento = await ls.Remover(id);
                return lancamento is null ? Results.NotFound() : Results.Ok();
            }).WithApiVersionSet(versionSet).MapToApiVersion(1.0);

            return app;
        }
    }
}