# TesteMarqAPI
API restful sistema de Leilão de Carro.


1. Planejamento e Estrutura do Projeto

estruturar o projeto:

    Modelos (Models): Representação dos dados, como Usuários, Carros e Lances.
    Controladores (Controllers): rotas para manipular os dados.
    Serviços (Services): Lógica de negócios, como integração com o ViaCEP, envio de e-mails, etc.
    Data (Repositories): Para acesso e manipulação dos dados no banco de dados.

2. Criando os Modelos

modelos principais:

    User: com propriedades como Id, Nome, Email, Senha, , CEP.
    Address: com propriedades Id e todas as informações vindas do ViaCEP.
    Car: com propriedades como Id, Marca, Modelo, Ano, PrecoMinimo.
    Bid: com propriedades como Id, UsuarioId, CarroId, Valor, DataHora.

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
	PUT  /api/usuarios/{id} - Editar Usuario.
	DELETE /api/usuarios/{id} - Deletar Usuario

    Carro
        GET /api/carros - Listar todos os carros.
        POST /api/carros - Criar um novo carro.
	PUT /api/carros/{id} - Editar carro.
        DELETE /api/carros/{id} - Deletar um carro.

    Lance
        POST /api/bids/{BidId} - Dar um lance em um carro.
	GET /api/bids/{BidId} - Consultar um lance em um carro.
	PUT /api/bids/{BidId} - Editar um lance em um carro.
	DELETE /api/bids/{BidId} - Deletar um lance em um carro.
