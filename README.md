# 6SOAT-TechChallenge-FIAP
O principal objetivo deste projeto foi desenvolver uma solucao para gerenciar o cadastro, pedidos, produtos e fila de entrega, sendo um sistema de controle de pedidos utilizando .NET Core, Clean Architecture e Kubernetes.

## Arquitetura do projeto
Arquivo "TechChallenge.drawio" na raiz do projeto para download caso necessário.
![image](https://github.com/user-attachments/assets/c410c3d7-856d-4145-a549-49b4571d2cd8)

## Iniciar o Projeto
Para iniciar o Projeto, é necessário clonar o projeto do GitHub num diretório de sua preferência:

```shell
cd "diretorio de sua preferencia"
git clone https://github.com/andersonssilveira96/Software-Architecture-FIAP
```
#### Opções de execução do projeto
   * Docker compose: Com o Daemon do Docker em execução, acesse o diretório em que o projeto foi clonado e execute o comando "docker compose up", irá subir o banco de dados, a API irá esperar o Healthcheck do banco para subir e assim que subir executará o Migration para construção do banco com os dados iniciais.
   * Visual Studio: Com o Visual Studio, basta abrir a solution, adicionar o "docker-compose" como projeto de inicialização e iniciar, conforme descrito na opção acima, irá subir toda a infraestrutura e executar o cadastro dos dados iniciais.
     
## Versao

2.0.0
 
Obrigado pela visita e bom codigo!
