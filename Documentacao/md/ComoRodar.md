# Como baixar :arrow_down: e rodar a solução :running

## Baixar o projeto

Para baixar a solução, deve-se acessar este mesmo repositório e baixar a versão Master ou Develop.

[Repositório mjguerrap/fluxocaixa](https://github.com/mjguerrap/fluxo-caixa)

Opções:

1) Clonar;
2) Utilizar o GitHub Desktop
3) Baixar compactado;

![.](./Documentacao/../../Img/Baixar%20solucao.png)

_**Importante**_
> É necessário que, para compilar a solução o dotnet core 7 esteja devidamente instalado e a versão do Visual Studio seja compatível

### Podemos rodar o projeto de 3 maneiras distintas

Geral - Configuração

- Copiar o arquivo FluxoCaixa.db para uma pasta em seu computador
- Configurar appsettings.Development.json e/ou appseting.json com o caminho que pode ser **absoluto (Para rodar no Docker só pode ser absoluto)** ou relativo de onde o arquivo FluxoCaixa.db foi colocado;
- Colocar FluxoCaixa.csproj como startup project

1) Através do debug do Visual Studio
   - Executar a configuração descrita acima
   - Rodar a aplicação pelo Visual Studio Debug
   - Observar a porta que a Api deu start, a porta que foi configurada no projeto é a 50965 e ajustar o link abaixo:
  
```uri
   http://localhost:[PORTA DO START]/swagger/index.html
```

2) Através da linha de comando
   - Executar a configuração descrita acima
   - Navegar para a pasta onde encontra-se o projeto FluxoCaixa
   - Executar a linha de comando abaixo

```cmd
    dotnet run
```

3) Rodando através de container
   - Copiar o arquivo do banco FluxoCaixa.db para uma pasta em seu computador
   - Rodar o comando abaixo substituindo [PATH ABSOLUTO] em "src" pelo caminho que o seu arquivo foi colocado no seu computador e [PORTA] em "-dp" por uma porta disponível em seu computador
  
```cmd
docker run --mount type=bind,src=[PATH ABSOLUTO],target=/Database -dp [PORTA]:50965  mjguerrap/fluxocaixateste --name fluxocaixa
```

