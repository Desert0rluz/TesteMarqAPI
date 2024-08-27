# TesteMarqAPI
API restful sistema de Leilão de Carro.


1. Planejamento e Estrutura do Projeto

estruturar o projeto:

    Modelos (Models): Representação dos dados, como Usuários, Carros e Lances.
    Controladores (Controllers): Onde vamos implementar as rotas para manipular os dados.
    Serviços (Services): Lógica de negócios, como integração com o ViaCEP, envio de e-mails, etc.
    Repositórios (Repositories): Para acesso e manipulação dos dados no banco de dados.
    Agendamento de Tarefas (Tasks): Para enviar os e-mails de notificação diariamente às 08:00.

2. Criando os Modelos

modelos principais:

    Usuário: com propriedades como Id, Nome, Email, Senha, Endereco, CEP.
    Carro: com propriedades como Id, Marca, Modelo, Ano, PrecoMinimo.
    Lance: com propriedades como Id, UsuarioId, CarroId, Valor, DataHora.

3. Criação do Cadastro de Usuário

    Cadastro de Usuário: Ao cadastrar um usuário, ao inserir o CEP, faremos uma chamada à API do ViaCEP para preencher automaticamente o endereço.

4. Manipulação de Carros

    Visualizar Carros: Um endpoint para listar todos os carros disponíveis.
    Criar/Deletar Carros: Endpoints para adicionar novos carros ou remover carros do sistema.

5. Sistema de Lances

    Dar Lance em um Carro: Endpoint para usuários darem lances em carros específicos.

6. Notificação de Vencedor

    Notificação de Vencedor: Quando um usuário ganha o leilão, ele é notificado por e-mail.

7. Tarefa Agendada

    Envio de E-mails: Uma rotina diária que envia e-mails às 08:00 para os ganhadores dos leilões.

8. Detalhamento dos Endpoints

Aqui está uma visão geral dos endpoints que você pode precisar implementar:

    Usuário
        POST /api/usuarios - Criar um novo usuário.
        GET /api/usuarios/{id} - Obter detalhes de um usuário.

    Carro
        GET /api/carros - Listar todos os carros.
        POST /api/carros - Criar um novo carro.
        DELETE /api/carros/{id} - Deletar um carro.

    Lance
        POST /api/carros/{carroId}/lances - Dar um lance em um carro.

9. Pontos de Atenção

    Validação e Autenticação: Lembre-se de incluir autenticação para proteger as rotas e garantir que apenas usuários autorizados possam criar, deletar carros ou dar lances.
    Integração com ViaCEP: Ao cadastrar o usuário, ao inserir o CEP, vamos fazer uma chamada para a API ViaCEP para preencher automaticamente o endereço.
    Envio de E-mails: Para o envio de e-mails, você pode utilizar o serviço SmtpClient do .NET, ou um serviço externo como SendGrid.

10. Tarefas Agendadas

Você pode usar o Hangfire ou outro serviço de agendamento de tarefas para garantir que os e-mails sejam enviados diariamente às 08:00.