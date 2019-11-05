#language: pt-br


Funcionalidade: Dados sou um usuario da API
	Quero testar o acesso API Usuario

Cenario: Obter usuario por identificador
	Dados que a url do endpoint é http://localhost:50003/api/Usuario
	E o metodo HTTP é 'GET'
	Quando chamar o servico
	Entao Statuscod da resposta deverá ser 'OK'
