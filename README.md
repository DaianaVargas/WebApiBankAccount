# API Contas Bancárias

Web API para criar e listar contas bancárias, além de permitir operações de saque e depósito nessas contas.

## Iniciando ...

O projeto foi desenvolvido no Visual Studio Community 2017. Para utilizar a aplicação, a mesma deve ser compilada local e deve ser utilizado o [Postman](https://www.getpostman.com/apps) para realizar a execução dos endpoints.

### Utilizando a API ...

As requisições a serem executadas na API Contas Bancárias são as seguintes:

* POST que cria uma conta bancária a partir da informação do CPF do cliente:
```
http://localhost:53126/api/BankAccounts/{números do CPF}
```

* GET que retorna todas as contas bancárias cadastradas no banco de dados:
```
http://localhost:53126/BankAccounts
```

* GET que retorna as informações da conta bancária solicitada (pesquisada pelo número informado):
```
http://localhost:53126/BankAccounts/{número da conta}
```

* POST que cria saques ou depósitos nas contas bancárias informadas na requisição; deve ser informado o valor que deve ser inserido no banco de dados e a operação ("sum" para depósitos e "discount" para saques):
```
http://localhost:53126/api/BankAccountBalances/{número da conta}/{valor}/{operação}
```

* GET que retorna todas as operações de depósitos e saques de todas as contas bancárias cadastradas no banco de dados:
```
http://localhost:53126/api/BankAccountsBalances
```

* GET que retorna as informações relativas a saques e depósitos na conta bancária informada pelo usuário. Se o tipo da requisição for "extract", a API retorna todas as operações pela data de criação; se o tipo da requisição for "currentBalance", a API retorna o valor do saldo atual na conta do usuário:
```
http://localhost:53126/api/BankAccountBalances/{número da conta}/{tipo}
```

## Rodando os testes unitários ...

Os testes unitários foram criados com o intuito de testar se os métodos estão conseguindo conectar corretamente com o banco de dados e fazer as devidas inserções de contas bancárias, operações e consultas.

## Autor

* **Daiana Vargas** - *Primeiro projeto no GitHub* 

## Licença

O projeto utiliza a licença MIT.

