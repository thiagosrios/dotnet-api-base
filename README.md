## Api Base

Este repositório contém um modelo de WebAPI usando a arquitetura MVC, desenvolvido em 
[.NET Core](https://dotnet.microsoft.com/) e 
[Entity Framework](https://docs.microsoft.com/pt-br/ef/).

O projeto serve para inicialização de novas APIs baseadas em .Net Core, com alguns recursos de classes prontos. 
Esta estrutura é flexível e permite a alteração e inclusão de camadas quando necessário. 

Para fins de inicialização e demostração do projeto, foi adicionado uma classe de modelo de Usuário como exemplo
de utilização da API. O contexto da aplicação utiliza SQLite para simular algumas das funcionalidades de um banco
tradicional. Ao reutilizar o projeto, modificar a fonte de dados usada no contexto para um mecanismo mais apropriado.

### Requisitos

- [Git](https://git-scm.com/) 
- [.Net Core](https://dotnet.microsoft.com/)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/) ou [Visual Studio Code](https://code.visualstudio.com/)