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
    public class EstadoRepository
    {
        public int Inserir(Estado estado)
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = @"INSERT INTO estados (nome, sigla)
OUTPUT INSERTED.ID VALUES 
(@NOME, @SIGLA)";
            command.Parameters.AddWithValue("@NOME", estado.Nome);
            command.Parameters.AddWithValue("@SIGLA", estado.Sigla);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public List<Estado> ObterTodos()
        {
            SqlCommand command = Conexao.Conectar();
            command.CommandText = "SELECT id AS 'EstadoId', nome AS 'EstadoNome', sigla AS 'EstadoSigla' FROM estados";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());

            List<Estado> estados = new List<Estado>();

            foreach (DataRow linha in tabela.Rows)
            {
                Estado estado = new Estado();
                estado.Id = Convert.ToInt32(linha["EstadoId"]);
                estado.Nome = linha["EstadoNome"].ToString();
                estado.Sigla = linha["EstadoSigla"].ToString();
                estados.Add(estado);
            }
            command.Connection.Close();
            return estados;
        }

    }
}
