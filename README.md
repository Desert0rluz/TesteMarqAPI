Visão Geral

Este sistema de gerenciamento de Empresas, Funcionarios e Pontos foi desenvolvido em C# utilizando .NET 8 e Entity Framework Core com SQLServer como banco de dados. O objetivo principal é fornecer uma interface para gerenciar empresas e funcionários, além de registrar e relatar pontos.

Requisitos e Implementação
1. Configuração do Banco de Dados

    O sistema foi configurado para utilizar o SQLServer como banco de dados, garantindo simplicidade e portabilidade. O Entity Framework Core foi utilizado para facilitar a interação com o banco de dados.

2. Implementação do CRUD para Company, Employee e Pontos
2.1 - Cadastro Único de Documentos

   Ao adicionar uma nova empresa ou funcionário, é feita a verificação para garantir que o documento não esteja em uso. Essa validação é realizada nas classes CompanyService e EmployeeService.

2.2 - Limitação de Caracteres

Os campos de nome em ambas as entidades (Company e Employee) foram limitados a 100 caracteres. Isso é validado durante o cadastro e atualização.

2.3 - Exclusões Lógicas

Implementou-se o conceito de soft delete, onde as entidades não são removidas fisicamente do banco de dados, mas marcadas como excluídas através de uma propriedade IsDeleted. Isso é realizado nos métodos DeleteAsync de EmployeeService e CompanyService.

2.4 - Propriedade PIN

A entidade Employee foi enriquecida com uma nova propriedade chamada PIN, que é única e possui um limite de 4 caracteres. O cadastro do funcionário exige a inclusão do PIN e a verificação da unicidade é realizada no método AddEmployeeAsync, o PIN é utlizado posteriormente para o registro do ponto.

3. Registro de Ponto
3.1 - Endpoint para Registro de Ponto

    Foi criado um endpoint que permite ao usuário registrar ponto informando o PIN do funcionário. O serviço RegistroPontoService trata da validação do PIN e do registro no banco de dados.

4. Relatório de Ponto
4.1 - Endpoint de Relatório

    Um endpoint foi desenvolvido para gerar relatórios de pontos, permitindo filtrar por Funcionario e Empresa data de inicio e data final. O retorno inclui informações como: Data, Nome do Funcionário, Documento, Quantidade de Pontos no Dia e Empresa.

Estrutura do Código

O código está dividido em serviços que implementam as interfaces correspondentes, proporcionando uma arquitetura limpa e separando a lógica de negócios da lógica de acesso a dados. As principais classes incluem:

EmployeeService: Gerencia operações relacionadas a funcionários, como adição, atualização e exclusão lógica.
CompanyService: Gerencia operações relacionadas a empresas, incluindo a validação de documentos.
RegistroPontoService: Responsável por registrar pontos e recuperar informações de registro.

Conclusão

A solução atende a todos os requisitos obrigatórios propostos para o teste, garantindo uma estrutura robusta para gerenciamento de tarefas, com foco em práticas de desenvolvimento limpas e eficientes.

Para executar a aplicação, é necessário configurar uma string de conexão para o banco de dados SQL no arquivo appsettings.json. Após a configuração, crie as migrações e atualize o banco de dados utilizando o Entity Framework. Ao iniciar a aplicação, o sistema abrirá automaticamente o navegador de sua escolha, exibindo os endpoints disponíveis através do Swagger.
