CREATE TABLE estados(
id INT PRIMARY KEY IDENTITY (1,1),
nome VARCHAR (50),
sigla VARCHAR (2)
);

CREATE TABLE cidades (
id INT PRIMARY KEY IDENTITY(1,1),

id_estado INT,
FOREIGN KEY (id_estado) REFERENCES estado(id), 

nome VARCHAR(50),
numero_habitantes INT
);

CREATE TABLE clientes(
id INT PRIMARY KEY IDENTITY(1,1),

id_cidade INT,
FOREIGN KEY (id_cidade) REFERENCES cidade(id),

nome VARCHAR(50),
cpf VARCHAR(50),
data_nascimento DATETIME2(7),
numero INT,
complemento NCHAR(10),
logradouro NCHAR(10),
cep NCHAR(10),

);