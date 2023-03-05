# fluxo-caixa

## Projeto teste técnico (SOLUÇÃO)

## [Sobre o Teste :mega:] | [Como rodar 🔨](/Documentacao/md/ComoRodar.md) | [Desenho 📚](/Documentacao/md/DesenhoSolucao.md)

### Sobre o teste

Criar uma solução para controlar o fluxo de caixa diário com os lançamentos (débitos e créditos)

Esta solução foi desenvolvida utilizando:

- VS Code
- .Net Core 7
- Minimal Api
- Log (Instrumentação de código)
- C#
- EF 7
- SQLite
- Swagger
- XUnit (Testes)
- Coverlet (Code Coverage)
- Report Coverage (Framework)
- Docker para rodar a aplicação

**Obs:** _**Esta solução é apenas Backend**_, a solução com o Frontend pode ser desenvolvida futuramente, a idéia é desenvolver o front em diversos frameworks e linguagens, como Angular, React, Vue e também Mobile (Nativo ou não);

### Algumas observações importantes :information_source

1) Problema a ser resolvido;
   1) Controlar o fluxo de caixa diário;
      - Débitos;
      - Créditos;
   2) Ter a visão do saldo diário consolidado;
2) Pontos que ficaram em dúvida e que devem em um cenário real ser perguntado para o cliente;
   1) Como é que esse controle é realizado atualmente?
   2) Precisamos desenvolver ou podemos adquirir algo pronto do mercado?
      - Por ser um comerciante, o controle do fluxo de caixa não parece ser o Domínio do negócio do cliente;
   3) Não temos informações de como o cliente controla esse caixa, como por exemplo:
       - O cliente tem mais de uma Loja?
       - Existe mais de um único caixa por loja?
       - O cliente entende "Caixa" como o movimento total da loja?
       - Existe um "ator" com o papel de caixa?
       - O Consolidado pode ser visualizado por todos?
       - ...
       - Muitas outras perguntas poderiam ser feitas para que não precisemos refatorar/refazer a solução
3) Como não foi comentado qual tipo de acesso deveria ser implementado, a parte de Autenticação e Autorização ficou de fora dessa implementação

### Pontos importantes que ficaram de fora, mas são importantes para uma solução real

- Segurança;
  - Autenticação;
  - Autorização;
  - Perfis de acesso;
  - Dados como string de conexão, senhas, secrets, nomes de usuário e informações sensíveis não devem estar nos repositórios da solução;
- Payload;
  - Quantidade de usuários simultaneos;
  - Volume de transações programadas;
- Desempenho;
- Confiabilidade;
- e muitos outros pontos que, se não observados causarão dores de cabeça futuras;
