# Desenho da solução

## [Sobre o teste :mega:](https://github.com/mjguerrap/fluxo-caixa) | [Como rodar 🔨](/Documentacao/md/ComoRodar.md) | [Desenho 📚]

## 1 Visão contexto

### Objetivo do contexto

> O objetivo da visão geral é dar aos stakeholders um panorama bastante sucinto das principais iterações e sistemas envolvidos, nesta visão, são abstraídas toda e qualquer referência à componentes e/ou código, é uma visão do sistema e suas iterações.

### Descrição do contexto

> Projeto **MVP** que permite ao gestor do estabelecimento comercial realizar o controle de seu fluxo de caixa (financeiro), com ele, será possível lancar as despesas e receitas, além de permitir visualizar os lançamentos de ntrada e saída e um consolidade diário de um determinado mês de referência.

![Contexto do sistema - Microserviço](/Documentacao/Img/C4-SistemaFluxoCaixaContext.png "Contexto do Sistema - Microserviço")</br>
*Imagem 01: Visão Geral do Sistema*

## 1 Visão do container (Composição do Sistema)

### Objetivo do container

> O objetivo é colocar uma aproximação de como o sistema é interligado, abstraindo sistemas externos e focando em como implementar corretamente e de forma objetiva todos os sistemas que serão fabricados, abstraindo os componentes internos e de terceiros e/ou frameworks.

### Descrição do container

> A implementação será consistida em duas UIs, uma Web e outra Mobile, a Web será responsíva e SPA (podendo ter diversos Micro-frontends), e a mobile poderá rodar em Andróide, OSx e Windows, teremos um sistema de autenticação único e um controle de autorização, já os Microserviços serão controlados por versões e poderão ter versões específicas para Mobile e Web, o banco de dados será o relacional.

![Contexto do sistema - Microserviço](/Documentacao/Img/C4-SistemaFluxoCaixaContainer.png "Contexto do Sistema - Microserviço")</br>
*Imagem 02: Visão intermediária do sistema*

## 2 Visão do componente (Componentização do Sistema)

### Objetivo do componente

> O objetivo da visão compoente é dar um panorama das camadas que iremos trabalhar, das convenções acordadas pelo time de Arquitetura referente ao Stack Tech e padrões. Nesta visão, focamos numa leiturá rápida e fácil dos principais componentes que devem ser compulsóriamente implementados.

### Descrição do componente

> Como podemos observar, teremos várias camadas que deverão ter uma única responsabilidade e deverão ser especialistas nesta responsabilidade, é importante ressaltar que não se deve misturar tais responsabilidades para que consigamos garantir a correção, adição de novas funcionalidades e principalmente, a manutenibilidade e agilidade na intervenção no código, tais responsabilidades devem seguir os padrões de mercado e estarão devidamente documentadas em um Wiki interno.

![Contexto do sistema - Microserviço](/Documentacao/Img/C4-SistemaFluxoCaixaComponent.png "Contexto do Sistema - Microserviço")</br>
*Imagem 03: Visão componente do sistema*

### **IMPORTANTE**

> A Documentação C4 possui 4 visões (Contexto, Container, Componente e Código), no meu ponto de vista, a visão Código somente deve ser construída em raríssimos casos onde a implementação é extremamente complexa e merece de um detalhamento visual bem definido para a compreensão do que está lá. Como podemos observar, não é o caso dessa nossa pequena solução.
