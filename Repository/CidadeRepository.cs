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
    class CidadeRepository
    {
        public int Inserir(Cidade cidade)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"INSERT INTO cidades (id_estado, nome, numero_habitantes)
OUTPUT INSERTED.ID VALUES 
(@ID_ESTADO,@NOME, @NUMERO_HABITANTES)";
            command.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            command.Parameters.AddWithValue("@NOME", cidade.Nome);
            command.Parameters.AddWithValue("@NUMERO_HABITANTES", cidade.NumeroHabitantes);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public List<Cidade> ObterTodos()
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT id AS 'CidadeId',id_estado AS 'EstadoId', nome AS 'CidadeNome', numero_habitantes AS 'NumeroHabitantes' FROM cidades";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());

            List<Cidade> cidades = new List<Cidade>();

            foreach (DataRow linha in tabela.Rows)
            {
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(linha["CidadeId"]);
                cidade.IdEstado = Convert.ToInt32(linha["EstadoId"]);
                cidade.Nome = linha["CidadeNome"].ToString();
                cidade.NumeroHabitantes = Convert.ToInt32(linha["NumeroHabitantes"]);
                cidades.Add(cidade);
            }
            command.Connection.Close();
            return cidades;
        }
    }
}
