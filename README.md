Mais um projeto pra treinar .NET 5 / C# / SQL


Sem dúvidas esse é o projeto mais denso que já comecei. O objetivo principal é construir uma livraria funcional, onde o administrador pode criar livros e os usuários podem
comprar ou pegar emprestado os livros.

Estrutura do projeto:

<b>API</b> Responsável por receber e direcionar as requisições HTTP. Ao receber uma requisição a API acessa a camada de Business. Obs: Em alguns casos a API acessa diretamente
a camada de Persistência, isso será alterado no futuro pra que não aconteça.

<b>Business</b> A camada de business é responsável por receber as chamadas da API, fazer validações se necessário, e faz as chamadas acessando a camada de persistência.

<b>Infraestrutura</b> Aqui é onde fica toda a lógica de acesso a base de dados, utilizando o repository pattern em alguns casos ou chamadas diretas em outros. Como é no caso
do cadastro de Livro. Um livro tem seu cadastro padrão na tabela Livro, insere também um registro na tabela Foto e por fim um registro na tabela LivroAutor, que é a responsável
por fazer o relacionamento entre Livro e Autor. Por enquanto as consultas são feitas de forma separada, porém em um futuro estarei aplicando todas essas consultas
em uma mesma transação, fazendo assim que se alguma consulta retorne erro não gera registros inconsistentes na base de dados.

<b>Domínio</b> Aqui é onde ficam nossas entidades da aplicação em relação a chamadas de Api ou Infraestrutura.

<b>WebApp</b> Interface da aplicação. Nosso WebApp funciona por meio de chamadas HTTP a nossa API. O WebApp é totalmente desacoplado das outras camadas, em nenhum momento
faz referência ao domínio, infraestrutura ou business. É uma camada separada.


<b>Servidor</b> Essa camada foi criada apenas para o processamento da imagem que o usuário fez upload no cadastro de livros. É basicamnete um serviço que salva a imagem 
em uma pasta

Aqui temos uma "ótima" imagem pra ilustrar a arquitetura da aplicação:
<img src="https://github.com/Gabriel-0216/Nascimento.Software.Livraria/blob/f8a9d2042be2b5a9ab0aae94429536f52a8bc522/ARQUITETURA.png"/>


Nesse primeiro commit: Temos o cadastro e listagem de Livros funcional, CRUD completo de Autor.

ToDo: CRUD completo de Livros Views de [Edit/Delete/Details], CRUD completo de Categorias [List/Insert/Delete/Edit/Details]
      
      Lógica de empréstimo de livros, lógica de compra de um livro.
      
     
English:
This is another project to practice .NET 5 / C# / SQL

Without doubt this is the most dense project that i already started. The main goal is to build a functional library, where the administrator can register books and the users 
can buy or loan them.

Project architecture:

<b>Api</b> Receives the HTTP Requests. When receives a request, the API access Business Layer. Obs: In some cases the API access directly the 'Persistencia' layer, this will be 
changed soon.

<b>Business</b> Business layer receives the API requests, validate the data if necessary and then calls 'Persistencia' layer
      
<b>Infra</b> This layer is responsible to all data base acess. Using repository pattern or in some cases calling directly the database. Like the 'Book create' case. A book have
a normal register in the database, but when inserting a book it's necessary to create a register in 'Foto' table, and 'LivroAutor' table too. For now i'm not using transactions, 
but soon i will implement transactions to prevent inconsistency in database.

<b>Domain</b> This is where we find our Application entitys 

<b>WebApp</b> Application interface. The WebApp works calling the API. It's totally separated from the other layers.


<b>Server</b> This layer was created just to process the images uploaded by the user on Book's register. It's basically a service that saves the image in a paste.


Look this "beautifull" image who ilustrates the application architecture:
<img src="https://github.com/Gabriel-0216/Nascimento.Software.Livraria/blob/f8a9d2042be2b5a9ab0aae94429536f52a8bc522/ARQUITETURA.png"/>

In this first commit: We have the book register, Author complete crud
ToDo: Book's CRUD/ Views[Edit/Delete/Details], Category CRUD [List/Insert/Delete/Edit/Details]
Book's sale and loan logic
