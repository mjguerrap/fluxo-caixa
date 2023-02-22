using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluxoCaixa;
using FluxoCaixa.Dominio;
using FluxoCaixa.Model;
using FluxoCaixa.Tests.Fixture;
using Microsoft.AspNetCore.Mvc.Testing;

public class ApiTest
{
    [Fact]
    public async Task Dado_ChamadaParaRoot_QuandoOk_Entao_RenornaMensagemApresentacao()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetStringAsync("/");

        response.Should().Be("Apresentação do Teste sobre Fluxo de Caixa!");
    }

    [Fact]
    public async Task Dado_ChamadaHealthCheck_QuandoOk_Entao_RenornaMensagemHealty()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetStringAsync("/HealthCheck");

        response.Should().Be("Healthy");
    } //

    [Fact]
    public async Task Dado_ChamadaHealthCheck_QuandoNOK_Entao_RenornaMensagemUnhealth()
    {
        Environment.SetEnvironmentVariable("ConnectionStrings.FluxoCaixaConnection", "Data Source=FluxoCaixaInexistente.db");
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetStringAsync("/HealthCheck");

        response.Should().Be("Healthy");
    } 

    [Fact]
    public async Task Dado_ChamadaBuscarLancamentoPorId_Quando_IdExistente_Entao_StatusCodeOK()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.GetAsync("lancamentos/1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Dado_ChamadaBuscarLancamentoPorId_Quando_IdIncorreto_Entao_StatusCodeNotFound()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.GetAsync("lancamentos/2");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Dado_ChamadoIncluirNovoLancamento_Quando_PayloadConsistente_Entao_StatusCodeCreated()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("lancamentos", new LancamentoPayload
        {
            Descricao = "Novo Lancamento",
            Valor = 105.99m,
            Data = DateTime.Now,
            TipoLancamento = TipoLancamento.Credito
        });
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    [Fact]
    public async Task Dado_ChamadoIncluirNovoLancamento_Quando_PayloadInconsistente_Entao_StatusCodeInternalServerError()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("lancamentos", new LancamentoPayload
        {
            Descricao = "Novo Lancamento",
            Valor = -105.99m,
            Data = DateTime.Now,
            TipoLancamento = TipoLancamento.Credito
        });
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Dado_ChamadoAlterarLancamento_Quando_PayloadConsistente_Entao_StatusCodeNoContent()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.PutAsJsonAsync("lancamentos/1", new LancamentoPayload
        {
            Descricao = "Novo Lancamento",
            Valor = 105.99m,
            Data = DateTime.Now,
            TipoLancamento = TipoLancamento.Credito
        });
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    [Fact]
    public async Task Dado_ChamadoAlterarLancamento_Quando_PayloadInconsistente_Entao_StatusCodeInternalServerError()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.PutAsJsonAsync("lancamentos/1", new LancamentoPayload
        {
            Descricao = "Novo Lancamento",
            Valor = -105.99m,
            Data = DateTime.Now,
            TipoLancamento = TipoLancamento.Credito
        });
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
    [Fact]
    public async Task Dado_ChamadoAlterarLancamento_Quando_IdLancamentoNaoEncontrado_Entao_StatusNotFound()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.PutAsJsonAsync("lancamentos/2", new LancamentoPayload
        {
            Descricao = "Novo Lancamento",
            Valor = -105.99m,
            Data = DateTime.Now,
            TipoLancamento = TipoLancamento.Credito
        });
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task Dado_ChamadoDeletarLancamento_Quando_IdLancamentoEncontrado_Entao_StatusOK()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.DeleteAsync("lancamentos/1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Dado_ChamadoDeletarLancamento_Quando_IdLancamentoNaoEncontrado_Entao_StatusNotFound()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.DeleteAsync("lancamentos/2");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task Dado_ChamadoDeletarLancamento_Quando_ErroDesconhecido_Entao_StatusInternalServerError()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.DeleteAsync("lancamentos/10");
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }    

    [Fact]
    public async Task Dado_ChamadaBuscarLancamentoPorPeriodo_Quando_ExistirLancamentos_Entao_StatusCodeOK()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();
                                                                         
        HttpResponseMessage response = await client.GetAsync("lancamentos?mes=2&ano=2022&pagina=1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Dado_ChamadaBuscarLancamentoPorPeriodo_Quando_NaoExistirLancamentos_Entao_StatusCodeNotFound()
    {
        await using var application = new ApplicationLancamentoFixture();
        using var client = application.CreateClient();

        HttpResponseMessage response = await client.GetAsync("lancamentos?mes=2&ano=2023&pagina=1");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    } 

}

