## Dotnet Api Base

Este repositório contém um modelo de uma API usando a arquitetura MVC, desenvolvido em [.NET Core](https://dotnet.microsoft.com/) e [Entity Framework](https://docs.microsoft.com/pt-br/ef/).

O projeto serve para inicialização de novas APIs baseadas em .Net Core, com alguns recursos de classes prontos. Esta estrutura é flexível e permite a alteração e inclusão de camadas quando necessário. 

Para fins de inicialização e demostração do projeto, foi adicionado uma classe de modelo de Usuário como exemplo de utilização da API. O contexto da aplicação utiliza um banco de dados em memória (*InMemoryDatabase*) para simular algumas das funcionalidades de um mecanismo de banco de dados tradicional. Ao reutilizar o projeto, modificar a fonte de dados usada no contexto para um mecanismo mais apropriado.

### Requisitos

- [Git](https://git-scm.com/) 
- [.Net Core](https://dotnet.microsoft.com/)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/), [Visual Studio Code](https://code.visualstudio.com/) ou outro editor com suporte ao C# e .Net Core. 

### Inicialização

Se estiver usando Windows com a IDE do Visual Studio, simplesmente importe o projeto através do arquivo de solução (*ApiBase.sln*). As propriedades de execução da solução presentes em *Properties -> launchSettings.json* serão carregadas e habilitadas automaticamente. Escolha um dos perfis e clique no ícone para excutar o projeto. 

Caso esteja usando o Linux ou prefira executar via linha de comando, digite: 

```bash
dotnet run
```

Para o modo *watch* (semelhante a um *live reload*), digite: 

```bash
dotnet watch run
```
No dois modos de execução uma instância do navegador padrão será aberta direcionada para a rota principal da API. Se tudo estiver funcionando, a seguinte mensagem deve aparecer: 

> API em Execução

Caso queira alterar esse comportamento, edite a propriedade *launchBrowser* do perfil específico de execução, dentro da seção *profiles*.  
