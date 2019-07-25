using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClienteRepository
    {
        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientes
(id_cidade, nome, cpf, data_nascimento, numero, complemento, logradouro, cep)
OUTPUT INSERTED.ID VALUES
(@ID_CIDADE, @NOME, @CPF, @DATA_NASCIMENTO, @NUMERO, @COMPLEMENTO, @LOGRADOURO, @CEP)";
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public List<Cliente> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT cidades.id_cidade AS 'IdCidade',
nome AS 'Nome',
cpf AS 'CPF',
data_nascimento AS 'Data de Nascimento',
numero AS 'Numero',
complemento AS 'Complemento'
logradouro AS 'Logradouro'
cep AS 'CEP'
FROM clientes
INNER JOIN cidades ON (cidades.id_cidade = cidade.id)
;";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Cliente> clientes = new List<Cliente>();
            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                cliente.IdCidade = Convert.ToInt32(linha["CidadeId"]);
                cliente.Nome = linha["Nome"].ToString();
                cliente.Cpf = linha["CPF"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["Data de Nascimento"]);
                cliente.Numero = Convert.ToInt32(linha["Número"]);
                cliente.Complemento = linha["Complemento"].ToString();
                cliente.Logradouro = linha["Logradouro"].ToString();
                cliente.Cep = linha["CEP"].ToString();


                cliente.Cidade = new Cidade();
                cliente.Cidade.Nome = linha["CidadeNome"].ToString();
            }
            return clientes;
        }
    }
}
