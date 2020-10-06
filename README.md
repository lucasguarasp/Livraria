# CRUD Livro

Front ->
CRUD usando ASP.NET CORE MVC 3.1, NHIBERNATE e POSTGRESQL
Back -> 
CRUD usando Angular version 9.0.3 e Rest

-> Ao Rodar o projeto o banco de dados é criado automaticamente, assim como as tabelas. 
Necessário ter o PostgreSQL instalado na máquina.


# Para rodar o Front:
npm install,
ng serve

obs: necessario rodar o back junto,
navegar por http://localhost:4200/


# Para rodar Back
em 'appsettings.json', verificar a string de conexão 'DefaultConnection',
obs: trocar senha do banco local, não é necessário criar ou alterar o nome do banco,
já tem uma configuração no startup para criar o banco e add a tabelas.


