# Felli - Grupo 04

## Autoria

[André Pedro](https://github.com/andre-pedro) - a21701115  
[Diogo Maia](https://github.com/Issa-Maia) - a21901308


## Repartição de Tarefas


André Pedro:

* Criação das classes principais
* Movimento das peças
* Condição de Vitória
* Criação da UI
* Comentários XML
* Documentação (README e UML)

Diogo Maia:

* Seleção das Peças
* Verificação das Direções
* Movimento das peças
* Remoção de Warnings
* Comentários XML
* Doxygen


## Repositório

O repositório do projeto está disponível no seguinte 
[_link_](https://github.com/andre-pedro/2ProjectoLP2).

## Arquitetura da Solução

### Descrição da Solução

Começámos por criar o _Game Manager_, criando dois jogadores (cada um com a sua 
respectiva cor, ID e quantidade de peças), e pedimos no inicio do jogo para cada 
_player_ escolher a sua cor de peças. Desenhámos o tabuleiro do jogo, fazendo 
com que certos espaços fossem jogáveis e outros não para criar um tabuleiro com 
a forma desejada. Colocámos seis peças de cada lado, com as suas respetivas cores 
para serem controladas por cada jogador.
Começámos a implementear a lógica do jogo, fazendo com que o primeiro jogador 
escolhesse a primeira peça, fosse feita uma verificação para saber tanto se a 
peça escolhida como a direção escolhida são válidas, e fazer o respetivo movimento. 
É feita a troca de turnos e o processo é repetido até um dos jogadores ficar sem 
peças ou todas as suas peças ficarem sem possíveis movimentos. Esse jogador perde 
e a vitória é atribuida ao outro jogador. 

### Diagrama UML

![](./UML.png)

## Referências

* [C# .NET API](https://docs.microsoft.com/en-us/dotnet/api/?view=netcore-2.2)
* [The C# Player's Guide](http://starboundsoftware.com/books/c-sharp/CSharpPlayersGuide-Sample.pdf)
* [C# Programming Yellow Book](https://static1.squarespace.com/static/5019271be4b0807297e8f404/t/5824ad58f7e0ab31fc216843/1478798685347/CSharp+Book+2016+Rob+Miles+8.2.pdf)
* [StackOverflow](https://stackoverflow.com/)